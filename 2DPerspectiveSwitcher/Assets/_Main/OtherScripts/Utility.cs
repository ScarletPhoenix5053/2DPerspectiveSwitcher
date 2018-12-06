using UnityEngine;
using System;

static class Utility
{
    public static float FramesToSeconds(int frames, int framesPerSecond = 60)
    {
        return Convert.ToSingle(frames) / Convert.ToSingle(framesPerSecond);
    }
}