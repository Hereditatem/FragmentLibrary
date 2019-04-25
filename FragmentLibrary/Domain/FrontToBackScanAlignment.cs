using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentLibrary.Domain
{
    public class FrontToBackScanAlignment
    {
        public FrontToBackScanAlignment(int left, int right, double scale, double angle)
        {
            Angle = angle;
            Scale = scale;
            Left = left;
            Right = right;
        }

        public int Left { get; private set; }

        public int Right { get; private set; }

        public double Scale { get; private set; }

        public double Angle { get; private set; }
    }
}
