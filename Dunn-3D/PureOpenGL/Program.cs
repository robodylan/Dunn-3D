using System;
using System.Drawing;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PureOpenGL
{
    class Program
    {
        static void Main()
        {
            using(var game = new GameWindow()) 
            {
                game.Load += (sender, e) =>
                {
                    game.VSync = VSyncMode.On;
                };

                game.UpdateFrame += (sender, e) =>
                {
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }
                };

                game.RenderFrame += (sender, e) =>
                {
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                    GL.Begin(PrimitiveType.Triangles);

                    GL.Vertex2(-1.0f, 1.0f);
                    GL.Vertex2(0.0f, -1.0f);
                    GL.Vertex2(1.0, 1.0);

                    GL.End();

                    game.SwapBuffers();
                };

                game.Run(60.0);
            }
        } 
    }
}
