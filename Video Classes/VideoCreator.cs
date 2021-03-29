using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace CVW
{
    static class VideoCreator
    {

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
            
            try
            {
                vidObj = (VidObj)bf.Deserialize(fileStream);
            }
            catch { }
            fileStream.Close();
            return vidObj.video;
        }

        /// <summary>
        /// Given a file path of an mp4 encoded video, it will return a Video structure object.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static Video RenderFrom(string filePath)
        {
            VideoExtractor video = new VideoExtractor(filePath);
            List<Frame> frameList = new List<Frame>();
            int x = Console.CursorLeft;
            int y = Console.CursorTop + 1;

            Console.Write("Estimated time: " + Math.Round((decimal)(video.frameList[0].Width * video.frameList[0].Height * video.frameList.Count)/198300, 2) + " seconds.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (Bitmap bitmap in video.frameList)
            {
                frameList.Add(new Frame(bitmap));
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
