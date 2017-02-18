using System;
using System.Collections.Generic;

namespace PrototypeTextInvaders.Modules
{    
    //Class to generate consistent, though not perfect, random integer numbers.
    class RNGenerator
    {
        static Random _r = new Random();

        public static int GiveRandom()
        {
            int n = _r.Next();
            return n;
        }

        public static int GiveRandom(int maxVal)
        {
            int n = _r.Next(maxVal);
            return n;
        }

        public static int GiveRandom(int minVal, int maxVal)
        {
            int n = _r.Next(minVal, maxVal);
            return n;
        }
    }
}
