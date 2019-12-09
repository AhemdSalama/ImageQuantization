using System;

namespace ImageQuantization
{
    public struct RgbPixel
    {
        public byte red, green, blue;
        public RgbPixel(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public static RgbPixel IntToRGB(int value)
        {
            var red = (value >> 0) & 255;
            var green = (value >> 8) & 255;
            var blue = (value >> 16) & 255;
            return new RgbPixel((byte) red, (byte) green, (byte) blue);

        }

        public int RGBToInt()
        {
            return (red << 0) | (green << 8) | (blue << 16);
        }

        public static double EuclideanDistance(RgbPixel p1, RgbPixel p2)
        {
            int redDistance = p1.red - p2.red;
            int greenDistance = p1.green - p2.green;
            int blueDistance = p1.blue - p2.blue;

            double squareDistance = redDistance * redDistance + greenDistance * greenDistance + blueDistance * blueDistance;
            return Math.Sqrt(squareDistance);

        }

        public static RgbPixel ConvertToRgbPixel(RGBPixel pixel)
        {
            RgbPixel p = new RgbPixel(pixel.red, pixel.green, pixel.blue);
            return p;
        }
    }
}
