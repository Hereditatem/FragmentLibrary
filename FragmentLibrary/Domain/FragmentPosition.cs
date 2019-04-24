using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentLibrary.Domain
{
    public class FragmentPosition
    {
        protected FragmentPosition()
        {

        }

        public FragmentPosition(Fragment fragment)
        {
            Fragment = fragment;
        }

        public Fragment Fragment { get; private set; }

        public double Angle { get; private set; }

        public double Scale { get; private set; }

        public int Left { get; private set; }

        public int Right { get; private set; }
    }
}
