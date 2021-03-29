using System;
using System.Collections.Generic;
using System.IO;
namespace CVW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console Video Watcher v1: by Heerod Sahraei\nCopyright (C) Hababisoft Corporation, All rights reserved.");
            Console.WriteLine("\nWould you like to make a new video, or load an existing one?(Render/Load):");
            Video video = new Video(new List<Frame>(), 0);
            switch (Console.ReadLine().ToLower())
            {
                case "render":
                    Console.WriteLine("NOTE: Don't use videos that have too large of a resolution(144p max/mp4 format),\nand longer videos will take more memory and time.\nInput the directory of the video you wish to render:");
                    string input = Console.ReadLine();
                    if (File.Exists(input))
                    {
                        video = VideoCreator.RenderFrom(input);
                        Console.WriteLine("Render Successful. Input filename to save .asciivid file into");
                        input = Console.ReadLine();
                        try
                        {
                            VideoCreator.Save(input, video);
                            Console.WriteLine("Successfully saved video:");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Unable to save file: " + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot render from directory. Press any key to exit the program.");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    break;
                case "load":
                    Console.WriteLine("NOTE: This is used to load rendered files by this program. This cannot load video files.");
                    string input1 = Console.ReadLine();
                    if (File.Exists(input1)) { video = VideoCreator.Load(input1); PlayVideo(video); }
                    break;
                default:
                    Console.WriteLine("Invalid Command. Press any key to exit the program.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine("Press any key to exit the program.");
            Console.ReadKey(); //Final Line in the program.
        }
        static void PlayVideo(Video video1)
        {
            VideoPlayback videoWindow = new VideoPlayback(video1.frames[0].x, video1.frames[0].y, video1.fps);
            foreach (Frame frame in video1.frames)
            {
                videoWindow.DrawFrame(frame);
            }
        }
    }
}
