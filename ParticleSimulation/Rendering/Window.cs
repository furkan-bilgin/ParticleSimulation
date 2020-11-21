using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.ParticleData;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Rendering
{
    public class Window
    {
        private RenderWindow window;
        private Dictionary<int, CircleShape> shapes;

        public Window()
        {
            shapes = new Dictionary<int, CircleShape>();
        }

        public void Run()
        {
            var mode = new VideoMode(1600, 900);
            
            window = new RenderWindow(mode, "Particle Simulation");
            window.KeyPressed += Window_KeyPressed;
        }
        /*
        public void InitShapes(List<Particle> particles)
        {
            foreach (var particle in particles)
            {
                shapes.Add(particle.Id, new CircleShape(3)
                {
                    FillColor = particle.GetParticleData<ParticleLifeParticleData>().ParticleType.Color
                });
            }
        }

        public void UpdateShapePositions(List<Particle> particles)
        {
            foreach (var particle in particles)
            {
                var position = shapes[particle.Id].Position;
                position.X = particle.Position.X;
                position.Y = particle.Position.Y;

                //shapes[particle.Id].Position = new SFML.System.Vector2f(particle.Position.X, particle.Position.Y);
            }
        }*/

        public void Update(List<Particle> particles)
        {
            window.DispatchEvents();
            window.Clear();

            var circle = new CircleShape(3);

            foreach (var particle in particles)
            {
                circle.Position = new SFML.System.Vector2f(particle.Position.X, particle.Position.Y);
                circle.FillColor = particle.GetParticleData<ParticleLifeParticleData>().ParticleType.Color;
                
                window.Draw(circle);
            }
            /*
            foreach (var shape in shapes)
            {
                window.Draw(shape.Value);
            }*/


            // Finally, display the rendered frame on screen
            window.Display();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            var window = (SFML.Window.Window)sender;
            if (e.Code == SFML.Window.Keyboard.Key.Escape)
            {
                window.Close();
            }
        }
    }
}
