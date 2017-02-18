using System;
using System.Collections.Generic;

namespace monoOPPAI_Engine.Utilities
{    
    //Class to generate consistent, though not perfect, random integer/float numbers.
    class RNGenerator
    {
        static Random _r = new Random();

        public static int GiveRandomInteger()
        {
            int n = _r.Next();
            return n;
        }

        public static int GiveRandomInteger(int maxVal)
        {
            int n = _r.Next(maxVal);
            return n;
        }

        public static int GiveRandomInteger(int minVal, int maxVal)
        {
            int n = _r.Next(minVal, maxVal);
            return n;
        }

        public static float GiveRandomFloat()
        {
            var buffer = new byte[4];
            _r.NextBytes(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static float GiveRandomFloat(float maxVal)
        {
            double range = (double)maxVal - (double)float.MinValue;
            double sample = _r.NextDouble();
            double scaled = (sample * range) + float.MinValue;
            return (float)scaled;
        }

        public static float GiveRandomFloat(float minVal, float maxVal)
        {
            double range = (double)maxVal - (double)minVal;
            double sample = _r.NextDouble();
            double scaled = (sample * range) + minVal;
            return (float)scaled;
        }
    }
}
