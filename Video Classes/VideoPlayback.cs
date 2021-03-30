using ConsoleExtender;
using System;
using System.Threading;
namespace CVW
{
    class VideoPlayback
    {
        private int screenX;
        private int screenY;
        private int frameRate;


        public VideoPlayback(int dimX, int dimY, int fps)
        {
            frameRate = fps;
            screenX = dimX * 2;
            screenY = dimY;
            try
            {
                ConsoleHelper.SetCurrentFont("Consolas", Convert.ToInt16(Math.Floor((decimal)360 / dimY)));
                if (Console.LargestWindowHeight >= screenY + 10) Console.WindowHeight = screenY + 10;
                if (Console.LargestWindowWidth >= screenX + 10) Console.WindowWidth = screenX + 10;
            }
            catch { }
        }

        /// <summary>
        /// Draws the frame specified into the console. 
        /// </summary>
        /// <param name="frame">The Frame to be drawn. Must have same resolution as the specified screen dimensions.</param>

        private void DrawFrame(Frame frame)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(frame.data);
            Thread.Sleep(Convert.ToInt32(Math.Floor(1000 / (frameRate + frameRate * 0.3)))); //TODO: Fine tune this so that fps stays more consistent.
        }

        /// <summary>
        /// Plays the specified video into the console.
        /// </summary>
        /// <param name="video"></param>

        public void PlayVideo(Video video)
        {
            Console.Clear();
            foreach (Frame frame in video.frames)
            {
                DrawFrame(frame);
            }
        }
    }
}
