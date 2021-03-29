using System;
using System.Drawing;
namespace CVW
{
    [Serializable]
    struct Frame
    {
        private int dimX; //Starts from 0
        private int dimY; // Starts from 0

        private string frameString; //the frame data

        public Frame(Bitmap bitmap)
        {
            frameString = "";
            dimX = bitmap.Width;
            dimY = bitmap.Height;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    frameString += GetPixelChar(GetPixelBrightness(bitmap.GetPixel(x, y)));
                    frameString += x == bitmap.Width - 1 ? '\n' : ' ';
                }
            }
        }
        public string data { get { return frameString; } set { } }
        public int x { get { return dimX; } set { } }
        public int y { get { return dimY; } set { } }
        private int GetPixelBrightness(Color color)
        {
            float brightness = color.GetBrightness();
            return Convert.ToInt32(Math.Floor(brightness * 7));
        }
        private char GetPixelChar(int level)
        {
            switch (level)
            {
                case 0:
                    return '.';
                case 1:
                    return ',';
                case 2:
                    return ':';
                case 3:
                    return 'i';
                case 4:
                    return '1';
                case 5:
                    return 'L';
                case 6:
                    return 'G';
                case 7:
                    return '@';
                default:
                    return ' ';
            }
        }
    }
}
