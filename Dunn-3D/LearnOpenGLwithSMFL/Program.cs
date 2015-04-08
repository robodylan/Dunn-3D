using System;
using System.Runtime.InteropServices;
using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using OpenTK;
using OpenTK.Graphics;
namespace LearnOpenGLwithSMFL
{
    class Program
    {
        static void Main()
        {
            ContextSettings contextSettings = new ContextSettings();
            contextSettings.DepthBits = 32;

            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML graphics with OpenGL", Styles.Default, contextSettings);
            window.SetVerticalSyncEnabled(true);

            window.SetActive();

            Toolkit.Init();
            GraphicsContext context = new GraphicsContext(new ContextHandle(IntPtr.Zero), null);

            window.Closed += new EventHandler(OnClosed);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.ClearDepth(1);

            GL.Disable(EnableCap.Lighting);

            GL.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float ratio = (float)(window.Size.X) / window.Size.Y;
            GL.Frustum(-ratio, ratio, -1, 1, 1, 500);

            GL.Enable(EnableCap.Texture2D);

            float[] cube = new float[]
            {
                // positions    // texture coordinates
                -20, -20, -20,  0, 0,
                -20,  20, -20,  1, 0,
                -20, -20,  20,  0, 1,
                -20, -20,  20,  0, 1,
                -20,  20, -20,  1, 0,
                -20,  20,  20,  1, 1,

                 20, -20, -20,  0, 0,
                 20,  20, -20,  1, 0,
                 20, -20,  20,  0, 1,
                 20, -20,  20,  0, 1,
                 20,  20, -20,  1, 0,
                 20,  20,  20,  1, 1,

                -20, -20, -20,  0, 0,
                 20, -20, -20,  1, 0,
                -20, -20,  20,  0, 1,
                -20, -20,  20,  0, 1,
                 20, -20, -20,  1, 0,
                 20, -20,  20,  1, 1,

                -20,  20, -20,  0, 0,
                 20,  20, -20,  1, 0,
                -20,  20,  20,  0, 1,
                -20,  20,  20,  0, 1,
                 20,  20, -20,  1, 0,
                 20,  20,  20,  1, 1,

                -20, -20, -20,  0, 0,
                 20, -20, -20,  1, 0,
                -20,  20, -20,  0, 1,
                -20,  20, -20,  0, 1,
                 20, -20, -20,  1, 0,
                 20,  20, -20,  1, 1,

                -20, -20,  20,  0, 0,
                 20, -20,  20,  1, 0,
                -20,  20,  20,  0, 1,
                -20,  20,  20,  0, 1,
                 20, -20,  20,  1, 0,
                 20,  20,  20,  1, 1
            };

            // Enable position and texture coordinates vertex components
            GL.EnableClientState(EnableCap.VertexArray);
            GL.EnableClientState(EnableCap.TextureCoordArray);
            GL.VertexPointer(3, VertexPointerType.Float, 5 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 5 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3));

            // Disable normal and color vertex components
            GL.DisableClientState(EnableCap.NormalArray);
            GL.DisableClientState(EnableCap.ColorArray);

            // Start game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Clear the window
                window.Clear();

                // Draw background
                window.PushGLStates();
                window.PopGLStates();

                // Clear the depth buffer
                GL.Clear(ClearBufferMask.DepthBufferBit);

                // Apply some transformations
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Translate(0, 0, -100f);

                // Draw the cube
                GL.DrawArrays(BeginMode.Triangles, 0, 36);

                // Draw some text on top of our OpenGL object
                window.PushGLStates();
                window.PopGLStates();

                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();
        }

        /// <summary>
        /// Function called when the window is resized
        /// </summary>
        static void OnResized(object sender, SizeEventArgs e)
        {
            GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
        }
    }
}
