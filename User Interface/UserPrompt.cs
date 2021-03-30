using ConsoleExtender;
using System;
using System.IO;
using CVW.Ascii;

namespace CVW.UI
{
    public static class UserPrompt
    {
        public static Video vidMem;
        private static readonly Video undefinedVid;

        public static void Ask()
        {
            Answer(ConsoleHelper.Prompt("").ToLower());
        }
        private static void Answer(string input)
        {
            switch (input)
            {
                default:
                    break;
                case "render":
                    Render();
                    break;
                case "load":
                    Load();
                    break;
                case "play":
                    Play();
                    break;
                case "save":
                    Save();
                    break;
                case "exit":
                    Exit();
                    break;
                case "help":
                    Help();
                    break;
            }
            Ask();
        }
        private static void Help()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("Load - load created asciivideos from saved files");
            Console.WriteLine("Save - save loaded asciivideos");
            Console.WriteLine("Render - render and load asciivideos from mp4 videos");
            Console.WriteLine("Play - play loaded asciivideos");
            Console.WriteLine("Exit - exit the program");
            Console.WriteLine("Help - show this text menu");
        }
        private static void Exit()
        {
            if (!vidMem.Equals(undefinedVid))
            {
                Console.WriteLine("Are you sure you want to exit? (Y/N)");
                switch (ConsoleHelper.Prompt("exit"))
                {
                    case "Y":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }
        private static void Save()
        {
            if (!vidMem.Equals(undefinedVid))
            {
                Console.WriteLine("Input the directory of the folder you wish to save to:");
                string input = ConsoleHelper.Prompt("save>directory");
                if (Directory.Exists(input))
                {
                    Console.WriteLine("Input the name which you wish to name the file. It will be given the .asciivid file extension.");
                    string input2 = ConsoleHelper.Prompt("save>filename>");
                    try
                    {
                        VideoCreator.Save(Path.Combine(input,input2 + ".asciivid"), vidMem);
                        Console.WriteLine("Saving Successful");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Saving Failed. " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Saving Failed. The directory you wish to save into does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Saving Failed. No video is rendered/loaded to be saved.");
            }
        }
        private static void Play()
        {
            if (!vidMem.Equals(undefinedVid))
            {
                try
                {
                    VideoCreator.PlayVideo(vidMem);
                }
                catch
                {
                    Console.WriteLine("Playback Failed. The video you are trying to play may be corrupted.");
                }
            }
            else
            {
                Console.WriteLine("Playback Failed. No video is rendered/loaded.");
            }
        }
        private static void Render()
        {
            Console.WriteLine("Recommended video sizes upto 144p and at a recommended 30fps, as playback speeds may vary.\nInput the directory of the video you wish to render from:");
            string input = ConsoleHelper.Prompt("render");
            if (File.Exists(input))
            {
                try
                {
                    vidMem = VideoCreator.RenderFrom(input);
                    Console.WriteLine("Render Successful.");
                }
                catch (Exception e)
                {
                    if (e is ArgumentException || e is OverflowException) Console.WriteLine("Render Failed. The video you attempted to render is too large.");
                    if (e is ArgumentOutOfRangeException) Console.WriteLine("Render Failed. Ensure that you are specifying an mp4 video file.");
                }
            }
            else
            {
                Console.WriteLine("Render Failed. Cannot render from specified directory.");
            }
        }
        private static void Load()
        {
            Console.WriteLine("This is used to load rendered files by this program. This cannot load video files.\nInput the directory of the video you wish to load from:");
            string input1 = ConsoleHelper.Prompt("load");
            if (File.Exists(input1))
            {
                try
                {
                    vidMem = VideoCreator.Load(input1);
                    Console.WriteLine("Loading Successful");
                }
                catch (Exception e)
                {
                    if (e is System.Runtime.Serialization.SerializationException) Console.WriteLine("Loading Failed. Ensure that you are specifying a file rendered by this program.");
                    else Console.WriteLine("Loading Failed. " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Loading Failed. Cannot load from specified directory");
            }
        }
    }
}
