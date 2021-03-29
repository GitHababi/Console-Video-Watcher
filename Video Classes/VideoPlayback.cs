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
                Console.SetWindowSize(screenX, screenY);
            } catch{}
            }

        /// <summary>
        /// Draws the frame specified into the console. 
        /// </summary>
        /// <param name="frame">The Frame to be drawn. Must have same resolution as the specified screen dimensions.</param>

            public void DrawFrame(Frame frame)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(frame.data);
                Thread.Sleep(1000 / (frameRate + 10));
            }
        }
    }
