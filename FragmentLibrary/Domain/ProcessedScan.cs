using System;
namespace FragmentLibrary.Domain
{
    public class ProcessedScan : Scan
    {
        public ProcessedScan(string originalImageId) 
        : base(originalImageId)
        {

        }

        public string LargeImageId { get; set; }

        public string MediumImageId { get; set; }

        public string SmallImageId { get; set; }
    }
}
