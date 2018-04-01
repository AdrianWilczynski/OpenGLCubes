using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLCubes.Shapes
{
    public static class Cube
    {
        public static void DrawCube(double sizeUnit, double angle)
        {
            GL.PushMatrix();
            GL.Rotate(angle, 0, 1, 0);

            // Front face
            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(sizeUnit, -sizeUnit, -sizeUnit); 
            GL.Vertex3(sizeUnit, sizeUnit, -sizeUnit);  
            GL.Vertex3(-sizeUnit, sizeUnit, -sizeUnit); 
            GL.Vertex3(-sizeUnit, -sizeUnit, -sizeUnit);
            GL.End();

            // Back face
            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(sizeUnit, -sizeUnit, sizeUnit);
            GL.Vertex3(sizeUnit, sizeUnit, sizeUnit);
            GL.Vertex3(-sizeUnit, sizeUnit, sizeUnit);
            GL.Vertex3(-sizeUnit, -sizeUnit, sizeUnit);
            GL.End();

            // Right face
            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(sizeUnit, -sizeUnit, -sizeUnit);
            GL.Vertex3(sizeUnit, sizeUnit, -sizeUnit);
            GL.Vertex3(sizeUnit, sizeUnit, sizeUnit);
            GL.Vertex3(sizeUnit, -sizeUnit, sizeUnit);
            GL.End();

            // Left face
            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Vertex3(-sizeUnit, -sizeUnit, sizeUnit);
            GL.Vertex3(-sizeUnit, sizeUnit, sizeUnit);
            GL.Vertex3(-sizeUnit, sizeUnit, -sizeUnit);
            GL.Vertex3(-sizeUnit, -sizeUnit, -sizeUnit);
            GL.End();

            // Top face
            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(sizeUnit, sizeUnit, sizeUnit);
            GL.Vertex3(sizeUnit, sizeUnit, -sizeUnit);
            GL.Vertex3(-sizeUnit, sizeUnit, -sizeUnit);
            GL.Vertex3(-sizeUnit, sizeUnit, sizeUnit);
            GL.End();

            // Bottom face
            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Vertex3(sizeUnit, -sizeUnit, -sizeUnit);
            GL.Vertex3(sizeUnit, -sizeUnit, sizeUnit);
            GL.Vertex3(-sizeUnit, -sizeUnit, sizeUnit);
            GL.Vertex3(-sizeUnit, -sizeUnit, -sizeUnit);
            GL.End();

            GL.PopMatrix();
        }
    }
}
