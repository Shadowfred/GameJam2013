#region Using Statements
using System;
using Microsoft.Xna.Framework;
using FredLib.Rand;
#endregion

namespace GameJam2013.Particles
{
    public static class ParticleHelpers
    {
        //public static readonly Random Random = new Random();

        public static float RandomBetween(float min, float max)
        {
            return min + (float)FredRandom.random.NextDouble() * (max - min);
        }
    }
}