using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentLibrary.Controllers.Viewmodels.Fragment
{

    public class FragmentDetailsDto
    {
        public long Id{ get; set; }

        public string Name { get; set; }
        
        public ScanImageInfoDto FrontScan { get; set; }

        public ScanImageInfoDto BackScan { get; set; }

        public ScanImageInfoDto FrontScanWithoutBackground { get; set; }

        public ScanImageInfoDto BackScanWithoutBackground { get; set; }

        public AlignmentDto FrontToBackAlignment { get; set; }
    }

    public class ScanImageInfoDto
    {
        public string OriginalImageID { get; set; }

        public string LargeImageID { get; set; }

        public string MediumImageID { get; set; }

        public string SmallImageID { get; set; }

    }

    public class AlignmentDto
    {
        public int Left { get; set; }

        public int Right { get; set; }

        public double Scale { get; set; }

        public double Angle { get; set; }
    }
}
