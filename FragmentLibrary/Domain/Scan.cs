using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentLibrary.Domain
{
    public class Scan
    {
        protected Scan()
        {

        }

        public Scan(string originalImageId)
        {
            OriginalImageId = originalImageId;
        }

        public string OriginalImageId { get; private set; }
    }
}
