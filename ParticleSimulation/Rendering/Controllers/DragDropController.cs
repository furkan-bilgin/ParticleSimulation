using SFML.Graphics;
using SFML.Window;
using System;

namespace ParticleSimulation.Rendering
{
    public class DragDropController
    {
        private const float SENSIVITY = 5;

        private View view;
        private RenderWindow renderWindow;

        public DragDropController(View view, RenderWindow renderWindow)
        {
            this.view = view;
            this.renderWindow = renderWindow;

            renderWindow.MouseMoved += Window_MouseMoved;
            renderWindow.MouseButtonPressed += Window_MouseButtonPressed;
            renderWindow.MouseButtonReleased += Window_MouseButtonReleased;
        }

        private float previousMouseX;
        private float previousMouseY;
        private bool clickedMouse;

        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
                clickedMouse = false;
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                clickedMouse = true;
                UpdatePreviousMousePosition(e.X, e.Y);
            }
        }

        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            if (clickedMouse)
            {
                var deltaX = e.X - previousMouseX;
                var deltaY = e.Y - previousMouseY;

                view.Move(-new SFML.System.Vector2f(deltaX, deltaY) * SENSIVITY);
                renderWindow.SetView(view);

                UpdatePreviousMousePosition(e.X, e.Y);

            }
        }

        private void UpdatePreviousMousePosition(float x, float y)
        {
            previousMouseX = x;
            previousMouseY = y;
        }
    }
}
