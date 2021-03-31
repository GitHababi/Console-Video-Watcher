using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CVW.Ascii
{
    static class VideoCreator
    {
        public static readonly char[] HighQuality = ".,:;iljkOXSG#&%@".ToCharArray();
        public static readonly char[] MedQuality = ".,:i1LG@".ToCharArray();
        public static readonly char[] LowQuality = ".iG@".ToCharArray();




        /// <summary>
        /// Given a video object, it will play the video into the console.
        /// </summary>
        /// <param name="video1"></param>

        public static void PlayVideo(Video video1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            VideoPlayback videoWindow = new VideoPlayback(video1.frames[0].x, video1.frames[0].y, video1.fps);
            videoWindow.PlayVideo(video1);
            stopwatch.Stop();
            Console.Clear();
            Console.WriteLine("Playback info: ");
            Console.WriteLine("Expected FPS: " + video1.fps + " Actual FPS: " + video1.frames.Count / (stopwatch.ElapsedMilliseconds / 1000));
            Console.WriteLine("DIMX: " + video1.frames[0].x + " DIMY: " + video1.frames[0].y);
        }

        /// <summary>
        /// Given a file path, and a video object, it will save to the specified file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="video"></param>
        /// <returns></returns>

        public static void Save(string filePath, Video video)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            VidObj vidObj = new VidObj(video);
            bf.Serialize(fileStream, vidObj);


            fileStream.Close();
        }

        /// <summary>
        /// Loads an ascii video from a file path, returns a Video structure.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static Video Load(string filePath)
        {
            VidObj vidObj = new VidObj(new Video());
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            vidObj = (VidObj)bf.Deserialize(fileStream);

            fileStream.Close();
            return vidObj.video;
        }

        /// <summary>
        /// Given a file path of an mp4 encoded video, it will return a Video structure object.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static Video RenderFrom(string filePath, int quality)
        {
            VideoExtractor video = new VideoExtractor(filePath);
            List<Frame> frameList = new List<Frame>();
            int x = Console.CursorLeft;
            int y = Console.CursorTop + 1;

            // This section here is dedicated to estimating the amount of time it will take to render.
            // I don't really care if it is that inaccurate, since when has it ever?
            
            decimal qualityFactor = 1;
            switch (quality)
            {
                case 0:
                    qualityFactor = 0.89M;
                    break;
                case 2:
                    qualityFactor = 1.123M;
                    break;
            }
            Console.Write("Estimated time: " + Math.Round((decimal)(video.frameList[0].Width * video.frameList[0].Height * video.frameList.Count) / 198300 * qualityFactor, 2) + " seconds.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (Bitmap bitmap in video.frameList)
            {
                frameList.Add(new Frame(bitmap, quality));
                Console.SetCursorPosition(x, y);
                Console.WriteLine(Convert.ToInt32(Math.Floor((decimal)frameList.Count / video.frameList.Count * 100)) + "% Done Rendering. Frames Rendered: " + frameList.Count);
            }

            stopwatch.Stop();
            Console.WriteLine("Elapased time: " + stopwatch.ElapsedMilliseconds + "ms");

            Video video1 = new Video(frameList, video.fps);
            return video1;
        }
    }
}
