using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Rendering.Controllers;
using SFML.Graphics;
using SFML.System;
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
        private View view;
        private Dictionary<int, CircleShape> shapes;

        private Font defaultFont;
        private SpaceConfig spaceConfig;

        private bool isGravityConfig;

        public Window(SpaceConfig spaceConfig)
        {
            this.spaceConfig = spaceConfig;

            isGravityConfig = spaceConfig is GravityConfig;

            defaultFont = new Font("arial.ttf");
            shapes = new Dictionary<int, CircleShape>();
        }

        public void Run()
        {
            var mode = new VideoMode(1600, 900);
            view = new View(new Vector2f(mode.Width / 2, mode.Height / 2), new Vector2f(mode.Width, mode.Height));

            window = new RenderWindow(mode, "Particle Simulation");
           
            window.SetView(view);
            window.KeyPressed += Window_KeyPressed;

            new DragDropController(view, window);
            new ZoomController(view, window);
        }

        public void Update(List<Particle> particles)
        {
            window.DispatchEvents();
            window.Clear();

            var circle = new CircleShape(3);

            foreach (var particle in particles)
            {
                if (isGravityConfig)
                    circle.Radius = particle.GetParticleData<GravityParticleData>()?.Radius ?? 3;
               
                circle.Position = new Vector2f(particle.Position.X, particle.Position.Y);
                circle.FillColor = particle.ParticleData.Color;
              
                circle.Origin = new Vector2f(circle.GetLocalBounds().Width / 2f, circle.GetLocalBounds().Height / 2f);
                window.Draw(circle);
            }

            window.Display();
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            var window = (SFML.Window.Window)sender;
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }
    }
}
