using System;
using System.Collections.Generic;
using System.IO;
using ConsoleExtender;
using System.Diagnostics;
namespace CVW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console Video Watcher v1: by Heerod Sahraei\nCopyright (C) Hababisoft Corporation, All rights reserved.");
            Console.WriteLine("\nWould you like to make a new video, or load an existing one?(Render/Load):");
            Video video = new Video(new List<Frame>(), 0);
            switch (ConsoleHelper.Prompt().ToLower())
            {
                case "render":
                    Console.WriteLine("NOTE: Please use upto 144p resolution and 30 fps videos\nLarger videos may take more time, and are not guaranteed to able to be played in the program.\nInput the directory of the video you wish to render:");
                    string input = ConsoleHelper.Prompt();
                    if (File.Exists(input))
                    {
                        video = VideoCreator.RenderFrom(input);
                        Console.WriteLine("Render Successful. Input filename to save .asciivid file into");
                        input = ConsoleHelper.Prompt();
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
                    string input1 = ConsoleHelper.Prompt();
                    if (File.Exists(input1)) { video = VideoCreator.Load(input1); PlayVideo(video); }
                    break;
                default:
                    Console.WriteLine("Invalid Command. Press any key to exit the program.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine("Press any key to exit the program.");
            Console.ReadLine(); //Final Line in the program.
        }
        static void PlayVideo(Video video1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            VideoPlayback videoWindow = new VideoPlayback(video1.frames[0].x, video1.frames[0].y, video1.fps);
            videoWindow.PlayVideo(video1);
            stopwatch.Stop();
            Console.WriteLine("Expected FPS: " + video1.fps + " Actual FPS: " + video1.frames.Count / (stopwatch.ElapsedMilliseconds / 1000));
            Console.WriteLine("DIMX: " + video1.frames[0].x + " DIMY: " + video1.frames[0].y);
        }
    }
}
