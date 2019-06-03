using System;

namespace RayTracingIn1Weekend.Week1
{
    public class RayTrace1
    {

        public static RayImage Render(int width, int height)
        {
            // As simple as possible code for now.
            var img = new RayImage();
            img.Width = width;
            img.Height = height;
            img.Pixels = new Rgb3f[width * height];

            int cur = 0;

            for(int j = height-1; j>= 0; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    float r = (float)i / (float)width;
                    float g = (float)j / (float)height;
                    float b = 0.2f;
              
                    img.Pixels[cur++] = new Rgb3f() { R = r, G = g, B = b };
                }
            }

            return img;
        }
    }
}
