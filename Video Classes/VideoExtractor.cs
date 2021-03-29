using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace CVW
{
    class VideoExtractor
    {
        public List<Bitmap> frameList = new List<Bitmap>(); //This is the list of all the bitmaps extracted from the frame extractor.
        public int fps;
        public VideoExtractor(string videoFile)
        {
            using (var vFReader = new VideoFileReader())
            {
                
                vFReader.Open(videoFile);
                for (int i = 0; i < vFReader.FrameCount; i++)
                {
                    frameList.Add(vFReader.ReadVideoFrame());
                }
                fps = Convert.ToInt32(vFReader.FrameRate.ToDouble());
                vFReader.Close();
            }
            Console.WriteLine(frameList.Count() + " frames to render.");
        }
    }
}
