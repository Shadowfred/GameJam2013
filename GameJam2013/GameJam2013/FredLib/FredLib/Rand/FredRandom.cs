using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FredLib.Rand
{
    public class FredRandom
    {
        public static Random random;

        public static void InitRand()
        {
            random = new Random();
        }
    }
}
