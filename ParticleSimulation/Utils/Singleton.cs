using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Utils
{
    public class Singleton<T>
    {
        private static object m_Lock = new object();
        private static T m_Instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        throw new Exception("Instance is null");
                    }

                    return m_Instance;
                }
            }
        }

        protected void InitSingleton(T instance)
        {
            m_Instance = instance;
        }
    }
}
