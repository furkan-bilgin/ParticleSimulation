using SFML.Graphics;
using System;

namespace ParticleSimulation.Rendering.Controllers
{
    public class ZoomController
    {
        const float SENSIVITY = 0.1f;

        private View view;
        private RenderWindow renderWindow;
        private float zoomValue;

        public ZoomController(View view, RenderWindow renderWindow)
        {
            this.view = view;
            this.renderWindow = renderWindow;

            renderWindow.MouseWheelScrolled += RenderWindow_MouseWheelScrolled;
        }

        private void RenderWindow_MouseWheelScrolled(object sender, SFML.Window.MouseWheelScrollEventArgs e)
        {
            if (zoomValue <= 0 && e.Delta <= 0)
            {
                return;
            }
            
            if (e.Delta > 0)
            {
                view.Zoom(1f + SENSIVITY);
                zoomValue += SENSIVITY;
            } 
            else if (e.Delta < 0)
            {
                view.Zoom(1f - SENSIVITY);
                zoomValue -= SENSIVITY;
            }

            renderWindow.SetView(view);
        }
    }
}
