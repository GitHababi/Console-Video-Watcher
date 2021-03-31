using System;
using System.Drawing;
namespace CVW.Ascii
{
    [Serializable]
    public struct Frame
    {
        private int dimX; //Starts from 0
        private int dimY; // Starts from 0
        private int qualityIndex;

        private string frameString; //the frame data

        public Frame(Bitmap bitmap, int qDex)
        {
            frameString = "";
            dimX = bitmap.Width;
            dimY = bitmap.Height;
            qualityIndex = qDex;
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
            if (qualityIndex == 0) return Convert.ToInt32(Math.Floor(brightness * 3)); //4 Brightness levels
            else if(qualityIndex == 1) return Convert.ToInt32(Math.Floor(brightness * 7)); //8 brightness levels
            else if(qualityIndex == 2) return Convert.ToInt32(Math.Floor(brightness * 15)); //16 brightness levels
            else return Convert.ToInt32(Math.Floor(brightness * 7));
        }
        private char GetPixelChar(int level)
        {
            switch (qualityIndex)
            {
                case 0:
                    return VideoCreator.LowQuality[level];
                case 1:
                    return VideoCreator.MedQuality[level];
                case 2:
                    return VideoCreator.HighQuality[level];
            }
            return ' ';
        }
    }
}
