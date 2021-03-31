using ConsoleExtender;
using System;
using System.Threading;
using System.Diagnostics;

namespace CVW.Ascii
{
    class VideoPlayback
    {
        private int screenX;
        private int screenY;
        private int frameRate; 
        public int threadDelay;

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
            Thread.Sleep(threadDelay);
        }       //Please someone i beg figure a better way of doing this, cause framerates are weird and flucuate due to system delays, so...

        /// <summary>
        /// Plays the specified video into the console.
        /// </summary>
        /// <param name="video"></param>

        public decimal PlayVideo(Video video)
        {
            Console.Clear();

            Stopwatch stopwatch = new Stopwatch(); //Determining video latency.          
            stopwatch.Start();
            DrawFrame(video.frames[0]);
            DrawFrame(video.frames[1]);
            stopwatch.Stop();
            decimal latency = stopwatch.ElapsedTicks / 10000; //Ticks are more accurate the elapsed milliseconds.
            threadDelay = Convert.ToInt32((1000 / frameRate) - latency); //Setting frame delay to an amount that accounts for latency.

            stopwatch.Restart();
            foreach (Frame frame in video.frames)
            {
                DrawFrame(frame);
            }
            stopwatch.Stop();
            Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds);
            return stopwatch.ElapsedTicks / 10000;
        }
    }
}
