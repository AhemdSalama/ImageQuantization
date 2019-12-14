using System;

namespace ImageQuantization
{
    public struct RgbPixel
    {
        public byte red, green, blue;  // Exact(1)
        public RgbPixel(byte red, byte green, byte blue)   // Exact(1)
        {
            this.red = red;  // Exact(1)
            this.green = green;   // Exact(1)
            this.blue = blue;   // Exact(1)
        }
        public static RgbPixel IntToRGB(int value)  // Exact(1)
        {
            var red = (value >> 0) & 255;  // Exact(1)
            var green = (value >> 8) & 255;  // Exact(1)
            var blue = (value >> 16) & 255;   // Exact(1)
            return new RgbPixel((byte) red, (byte) green, (byte) blue);  // Exact(1)
        }
        public int RGBToInt()    
        {
            return (red << 0) | (green << 8) | (blue << 16);    // Exact(1)
        }
        public static double EuclideanDistance(RgbPixel p1, RgbPixel p2)   // Exact(1)
        {
            int redDistance = p1.red - p2.red;    // Exact(1)
            int greenDistance = p1.green - p2.green;   // Exact(1)
            int blueDistance = p1.blue - p2.blue;   // Exact(1)
            // Exact(1)
            double squareDistance = redDistance * redDistance + greenDistance * greenDistance + blueDistance * blueDistance;
            return Math.Sqrt(squareDistance);   // Exact(1)
        }
        public static RgbPixel ConvertToRgbPixel(RGBPixel pixel)    // Exact(1)
        {
            RgbPixel p = new RgbPixel(pixel.red, pixel.green, pixel.blue);   // Exact(1)
            return p;    // Exact(1)
        }
        public static RgbPixel operator +(RgbPixel p1, RgbPixel p2)    // Exact(1)
        {
            RgbPixel pxl = new RgbPixel();   // Exact(1)

            pxl.red = (byte)(p1.red + p2.red);   // Exact(1)
            pxl.blue = (byte)(p1.blue + p2.blue);   // Exact(1)
            pxl.green = (byte)(p1.green + p2.green);     // Exact(1)
            return pxl;   // // Exact(1)
        }

    }
}
