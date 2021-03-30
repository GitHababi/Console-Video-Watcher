using CVW.UI;
using CVW.Ascii;
using System;
using System.IO;

/// Basic map of this program's source code:
/// The CVW.Ascii namespace contains all you'll need to create, play, and save videos.
/// The way you interact with these video classes is quite simple.
/// Use the VideoCreator static class methods to play, extract, save, and load videos.
/// Play - PlayVideo(Video object), Load (returns Video object)- Load(File path), 
/// Save - Save(Video object, File path), Render/Extract (returns Video object)- RenderFrom(File path)

namespace CVW
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("Console Video Watcher: by Heerod Sahraei\nCopyright (C) Hababisoft Corporation, All rights reserved.\nVersion 1.4A");
                    break;
                case 1: //What to do when file is opened with this program.
                    if (args[0].Contains(".mp4")) //When an mp4 video is put in.
                    {
                        if (File.Exists(args[0]))
                        {
                            try
                            {
                                UserPrompt.vidMem = VideoCreator.RenderFrom(args[0]);
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
                    else //If it isn't an mp4 video, it will just try to load it directly.
                    {
                        if (File.Exists(args[0]))
                        {
                            try
                            {
                                UserPrompt.vidMem = VideoCreator.Load(args[0]);
                                try
                                {
                                    VideoCreator.PlayVideo(UserPrompt.vidMem);
                                }
                                catch
                                {
                                    Console.WriteLine("Playback Failed. The video you are trying to play may be corrupted.");
                                }
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
                    break;
            }
            UserPrompt.Ask();
        }
    }
}
