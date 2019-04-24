using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentLibrary.Domain
{
    public class Fragment
    {
        protected Fragment()
        {

        }

        public Fragment(
            Scan frontScan,
            Scan backScan,
            Scan frontWithoutBgScan,
            Scan backWithoutBgScan
            ) : this()
        {
            FrontScan = frontScan ?? throw new ArgumentNullException(nameof(frontScan));
            BacktScan = backScan ?? throw new ArgumentNullException(nameof(backScan));
            FrontScanWithoutBackground = frontWithoutBgScan ?? throw new ArgumentNullException(nameof(frontWithoutBgScan));
            BackScanWithoutBackground = backWithoutBgScan ?? throw new ArgumentNullException(nameof(backWithoutBgScan));
        }

        public long Id { get; private set; }

        public Scan FrontScan { get; private set; }

        public Scan BacktScan { get; private set; }

        public FrontToBackScanAlignment FrontToBackScanAlignment { get; private set; }

        public Scan FrontScanWithoutBackground { get; private set; }

        public Scan BackScanWithoutBackground { get; private set; }

        public FrontToBackScanAlignment FrontToBackWithoutBackgroundScanAlignment { get; private set; }

        public void SetFrontToBackScanAlignment(FrontToBackScanAlignment frontToBackScanAlignment)
        {
            FrontToBackScanAlignment = frontToBackScanAlignment;
        }

        public void SetFrontToBackWithoutBackgroundScanAlignment(FrontToBackScanAlignment frontToBackScanAlignment)
        {
            FrontToBackWithoutBackgroundScanAlignment = frontToBackScanAlignment;
        }
    }
}
