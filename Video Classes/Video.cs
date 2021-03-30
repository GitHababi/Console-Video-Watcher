using System;
using System.Collections.Generic;

namespace CVW
{
    [Serializable]
    public struct Video
    {
        public List<Frame> frames;
        public readonly int fps;
        public Video(List<Frame> videoFrames, int frameRate)
        {
            fps = frameRate;
            frames = videoFrames;
        }
    }
    [Serializable]
    class VidObj
    {
        public Video video;
        public VidObj(Video video1)
        {
            video = video1;
        }
    }
}
