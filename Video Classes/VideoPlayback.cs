using ConsoleExtender;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CVW.Ascii
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
        }

        /// <summary>
        /// Calculates the latency of the playing of the video.
        /// </summary>
        /// <param name="video"></param>
        /// <returns>Latency between frames in Ticks</returns>

        public double CalculateLatency(Video video)
        {
            int testFrames = Convert.ToInt32(video.frames.Count / 10);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int x = 0; x < testFrames; x++)
            {
                DrawFrame(video.frames[x]);
            }
            stopwatch.Stop();
            Console.WriteLine("Frames tested: " + testFrames + " Calculated Latency: " + (stopwatch.ElapsedTicks / (testFrames)));
            return stopwatch.ElapsedTicks / (testFrames);
        }
        
        /// <summary>
        /// Plays the specified video into the console.
        /// </summary>
        /// <param name="video"></param>
        /// <returns>Amount of time in seconds it took to play the video.</returns>
        
        public async Task<double> PlayVideo(Video video)
        {
            Console.Clear();

            TimeSpan timeSpan = (video.latency * 1.3 < 1000000/frameRate) ? new TimeSpan((long)(10000000 / frameRate - video.latency * 1.3)) : new TimeSpan(10000000 / frameRate - (long)video.latency); 
            //Setting frame delay to an amount that accounts for latency. Isn't perfect, but does much better job than before.

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            for (int x = 0;x < video.frames.Count; x++)
            {
                DrawFrame(video.frames[x]);
                await Task.Delay(timeSpan);
            }
            stopwatch.Stop();
            return stopwatch.ElapsedTicks / 1000;
        }
    }
}
