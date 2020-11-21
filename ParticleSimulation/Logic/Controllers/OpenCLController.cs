using Ara3D;
using Cloo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParticleSimulation.Logic.Controllers
{
    public class OpenCLController
    {
        static string CLProgram
        {
            get
            {
                return @"
                    
                        __kernel void calculateSquareDistance(__global float *resultX,
                                               __global float *resultY,
                                               __global float *listX,
                                               __global float *listY)
                        {
                            int id = get_global_id(0);
                            resultX[id] = sqrt(listX[id]);
                            resultY[id] = sqrt(listY[id]);
                        }
                ";
            }
        }

        private ComputeProgram program;
        private ComputeKernel kernel;
        private ComputeContext context;
        private ComputeDevice device;

        public OpenCLController()
        {
            var platform = ComputePlatform.Platforms[0];
            context = new ComputeContext(ComputeDeviceTypes.All, new ComputeContextPropertyList(platform), null, IntPtr.Zero);
            device = context.Devices[0];

            program = new ComputeProgram(context, CLProgram);

            try
            {
                program.Build(null, null, null, IntPtr.Zero);
            }
            catch (Exception e)
            {
                Console.WriteLine("Build log: " + program.GetBuildLog(device));
                throw e;
            }

            kernel = program.CreateKernel("calculateSquareDistance");
        }

        public List<Vector2> CalculateSquareDistance(List<Vector2> positions)
        {
            // Create data
            var xResultData = new float[positions.Count];
            var yResultData = new float[positions.Count];

            var xData = positions.Select(x => x.X).ToArray();
            var yData = positions.Select(x => x.Y).ToArray();

            // Put data on buffers
            var xResultBuffer = new ComputeBuffer<float>(context, ComputeMemoryFlags.WriteOnly | ComputeMemoryFlags.CopyHostPointer, xResultData);
            var yResultBuffer = new ComputeBuffer<float>(context, ComputeMemoryFlags.WriteOnly | ComputeMemoryFlags.CopyHostPointer, yResultData);

            var xBuffer = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, xData);
            var yBuffer = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, yData);

            // Set memory arguments to kernel
            kernel.SetMemoryArgument(0, xResultBuffer);
            kernel.SetMemoryArgument(1, yResultBuffer);
            kernel.SetMemoryArgument(2, xBuffer);
            kernel.SetMemoryArgument(3, yBuffer);

            // Create queue
            var queue = new ComputeCommandQueue(context, device, ComputeCommandQueueFlags.None);
            queue.Execute(kernel, null, new long[] { positions.Count }, null, null);

            // Read data
            queue.ReadFromBuffer(xResultBuffer, ref xResultData, true, null);
            queue.ReadFromBuffer(yResultBuffer, ref yResultData, true, null);

            var result = new List<Vector2>();
            for (int i = 0; i < xResultData.Length; i++)
            {
                result.Add(new Vector2(xResultData[i], xResultData[i]));
            }

            return result;
        }
    }
}
