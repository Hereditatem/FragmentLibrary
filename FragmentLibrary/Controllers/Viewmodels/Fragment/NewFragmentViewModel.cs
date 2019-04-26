using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentLibrary.Controllers.Viewmodels.Fragment
{
    public class NewFragmentViewModel
    {
        public NewFragmentViewModel()
        {
        }

        public string Name { get; set; }

        public IFormFile FrontScan { get; set; }

        public IFormFile BackScan { get; set; }

        public IFormFile FrontScanWithoutBackground { get; set; }

        public IFormFile BackScanWithoutBackground { get; set; }
    }
}
