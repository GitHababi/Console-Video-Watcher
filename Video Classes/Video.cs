using System;
using System.Collections.Generic;

namespace CVW.Ascii
{
    [Serializable]
    public struct Video
    {
        public List<Frame> frames;
        public readonly int fps;
        public double latency;
        public Video(List<Frame> videoFrames, int frameRate)
        {
            latency = 0;
            fps = frameRate;
            frames = videoFrames;

            VideoPlayback video = new VideoPlayback(frames[0].x,frames[0].y,fps);
            latency = video.CalculateLatency(this); //Calculate latency
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
