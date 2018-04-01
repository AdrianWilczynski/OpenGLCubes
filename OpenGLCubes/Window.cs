using OpenGLCubes.Enums;
using OpenGLCubes.Shapes;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace OpenGLCubes
{
    public class Window : GameWindow
    {
        private double viewingAngle = 30;

        private Mode mode = Mode.RotatingBigCube;

        private int numberOfCubes = 3;

        private double smallCubeSizeUnit = 0.10;
        private double smallCubeAngle = 0;
        private double SmallCubeAngle
        {
            get => smallCubeAngle;
            set
            {
                if (value > 360)
                {
                    smallCubeAngle = value - 360;
                }
                else
                {
                    smallCubeAngle = value;
                }
            }
        }
        private double smallCubeRotationSpeed = 1;

        private double bigCubeAngle = 30;
        private double BigCubeAngle
        {
            get => bigCubeAngle;
            set
            {
                if (value > 360)
                {
                    bigCubeAngle = value - 360;
                }
                else
                {
                    bigCubeAngle = value;
                }
            }
        }
        private double bigCubeRotationSpeed = 1;

        private double distanceBetweenCubes = 0.4;
        private GrowthDirection growthDirection = GrowthDirection.Grow;
        private double maxDistanceBetweenCubes = 0.45;
        private double minDistanceBetweenCubes = 0.3;
        private double distanceChangeSpeed = 0.005;

        private ColorChangeDirection colorChangeDirection = ColorChangeDirection.Desaturate;
        private int colorFactor = 0;
        private int ColorFactor
        {
            get => colorFactor;
            set
            {
                if (value > 255)
                {
                    colorFactor = 255;
                }
                else if (value < 0)
                {
                    colorFactor = 0;
                }
                else
                {
                    colorFactor = value;
                }
            }
        }
        private int colorChangeSpeed = 1;

        public Window() : base(600, 500, new GraphicsMode(32, 24, 0, 4), "Sześciany") { }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Lighting);
            GL.Light(LightName.Light0, LightParameter.Ambient, new[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new[] { 0.8f, 0.8f, 0.8f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1.0f, 0.0f, -1.0f });
            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.ColorMaterial);

            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            if (Width > Height)
            {
                GL.Viewport((Width - Height) / 2, 0, Height, Height);
            }
            else
            {
                GL.Viewport(0, (Height - Width) / 2, Width, Width);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            switch (mode)
            {
                case Mode.RotatingBigCube:
                    BigCubeAngle += bigCubeRotationSpeed;
                    break;

                case Mode.RotatingSmallCubes:
                    SmallCubeAngle += smallCubeRotationSpeed;
                    break;

                case Mode.GrowingCube:
                    if (distanceBetweenCubes >= maxDistanceBetweenCubes)
                    {
                        growthDirection = GrowthDirection.Shrink;
                    }
                    else if (distanceBetweenCubes <= minDistanceBetweenCubes)
                    {
                        growthDirection = GrowthDirection.Grow;
                    }

                    if (growthDirection == GrowthDirection.Grow)
                    {
                        distanceBetweenCubes += distanceChangeSpeed;
                    }
                    else if (growthDirection == GrowthDirection.Shrink)
                    {
                        distanceBetweenCubes -= distanceChangeSpeed;
                    }
                    break;

                case Mode.ChangingColors:
                    if (ColorFactor == 0)
                    {
                        colorChangeDirection = ColorChangeDirection.Saturate;
                    }
                    else if (ColorFactor == 255)
                    {
                        colorChangeDirection = ColorChangeDirection.Desaturate;
                    }

                    if (colorChangeDirection == ColorChangeDirection.Saturate)
                    {
                        ColorFactor += colorChangeSpeed;
                    }
                    else if (colorChangeDirection == ColorChangeDirection.Desaturate)
                    {
                        ColorFactor -= colorChangeSpeed;
                    }
                    break;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();

            GL.Rotate(viewingAngle, -1, 0, 0);

            GL.Rotate(BigCubeAngle, 0, 1, 0);

            GL.Translate(-distanceBetweenCubes, -distanceBetweenCubes, -distanceBetweenCubes);

            for (int x = 0; x < numberOfCubes; x++)
            {
                GL.PushMatrix();
                GL.Translate(x * distanceBetweenCubes, 0, 0);

                for (int y = 0; y < numberOfCubes; y++)
                {
                    GL.PushMatrix();
                    GL.Translate(0, y * distanceBetweenCubes, 0);

                    for (int z = 0; z < numberOfCubes; z++)
                    {
                        GL.PushMatrix();
                        GL.Translate(0, 0, z * distanceBetweenCubes);

                        DecideColor(x, y, z);

                        Cube.DrawCube(smallCubeSizeUnit, SmallCubeAngle);

                        GL.PopMatrix();
                    }
                    GL.PopMatrix();
                }
                GL.PopMatrix();
            }

            SwapBuffers();
        }

        private void DecideColor(int x, int y, int z)
        {
            var numberOfLastCube = numberOfCubes - 1;
            var numberOfCenterCube = numberOfCubes / 2;

            var isFrontTopCornerCube = x == 0 && y == numberOfLastCube && z == 0;
            var isBackTopCornerCube = x == numberOfLastCube && y == numberOfLastCube && z == numberOfLastCube;
            var isTopCenterCube = x == numberOfCenterCube && y == numberOfLastCube && z == numberOfCenterCube;

            if (isFrontTopCornerCube)
            {
                var frontTopCornerCubeColor = Color.FromArgb(0, 255 - ColorFactor, ColorFactor);
                GL.Color3(frontTopCornerCubeColor);
            }
            else if (isBackTopCornerCube)
            {
                var backTopCornerCubeColor = Color.FromArgb(255 - ColorFactor, 255 - ColorFactor, ColorFactor);
                GL.Color3(backTopCornerCubeColor);
            }
            else if (isTopCenterCube)
            {
                var topCenterCubeColor = Color.FromArgb(255 - ColorFactor, 0, ColorFactor);
                GL.Color3(topCenterCubeColor);
            }
            else
            {
                var primaryCubeColor = Color.Blue;
                GL.Color3(primaryCubeColor);
            }
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Number1:
                    mode = Mode.RotatingBigCube;
                    break;
                case Key.Number2:
                    mode = Mode.RotatingSmallCubes;
                    break;
                case Key.Number3:
                    mode = Mode.GrowingCube;
                    break;
                case Key.Number4:
                    mode = Mode.ChangingColors;
                    break;
                case Key.Escape:
                    Exit();
                    break;
            }
        }
    }
}

