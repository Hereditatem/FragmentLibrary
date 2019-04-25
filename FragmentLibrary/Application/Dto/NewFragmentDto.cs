using System;
using Microsoft.AspNetCore.Http;

namespace FragmentLibrary.Application.Dto
{

    public class NewFragmentDto
    {
        public NewFragmentDto()
        {
        }

        public string Name { get; set; }

        public IFormFile FrontScan { get; set; }

        public IFormFile BackScan { get; set; }

        public IFormFile FrontScanWithoutBackground { get; set; }

        public IFormFile BackScanWithoutBackground { get; set; }
    }
}
