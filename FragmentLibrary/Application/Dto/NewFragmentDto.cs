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

        public byte[] FrontScan { get; set; }

        public string FrontScanContentType { get; set; }

        public byte[] BackScan { get; set; }

        public string BackScanContentType { get; set; }

        public byte[] FrontScanWithoutBackground { get; set; }

        public string FrontScanWithoutBackgroundContentType { get; set; }

        public byte[] BackScanWithoutBackground { get; set; }

        public string BackScanWithoutBackgroundContentType { get; set; }
    }
}
