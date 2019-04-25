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

        public long Id { get; private set; }

        public string Name { get; private set; }

        public int? PanelId { get; private set; }

        public bool PanelIsKnown => PanelId.HasValue;

        public Scan FrontScan { get; private set; }

        public Scan BacktScan { get; private set; }

        public FrontToBackScanAlignment FrontToBackScanAlignment { get; private set; }

        public ProcessedScan FrontScanWithoutBackground { get; private set; }

        public ProcessedScan BackScanWithoutBackground { get; private set; }

        public FrontToBackScanAlignment FrontToBackWithoutBackgroundScanAlignment { get; private set; }

        public void SetFrontToBackScanAlignment(FrontToBackScanAlignment frontToBackScanAlignment)
        {
            FrontToBackScanAlignment = frontToBackScanAlignment;
        }

        public void SetFrontToBackWithoutBackgroundScanAlignment(FrontToBackScanAlignment frontToBackScanAlignment)
        {
            FrontToBackWithoutBackgroundScanAlignment = frontToBackScanAlignment;
        }

        public static Fragment BuildTransient(
            string name,
            Scan frontScan,
            Scan backScan,
            ProcessedScan frontWithoutBgScan,
            ProcessedScan backWithoutBgScan)
        {
            return new Fragment()
            {
                Name = name ?? throw new ArgumentNullException(nameof(name)),
                FrontScan = frontScan ?? throw new ArgumentNullException(nameof(frontScan)),
                BacktScan = backScan ?? throw new ArgumentNullException(nameof(backScan)),
                FrontScanWithoutBackground = frontWithoutBgScan ?? throw new ArgumentNullException(nameof(frontWithoutBgScan)),
                BackScanWithoutBackground = backWithoutBgScan ?? throw new ArgumentNullException(nameof(backWithoutBgScan)),
            };
        }

        public static Fragment BuildTransientWithKnownPanel(
            string name,
           Scan frontScan,
           Scan backScan,
           ProcessedScan frontWithoutBgScan,
           ProcessedScan backWithoutBgScan,
           int panelId)
        {
            var transient = BuildTransient(name, frontScan, backScan, frontWithoutBgScan, backWithoutBgScan);
            transient.PanelId = panelId;
            return transient;
        }

        public static Fragment BuildFromRepository(
            long id,
            string name,
            Scan frontScan,
            Scan backScan,
            ProcessedScan frontWithoutBgScan,
            ProcessedScan backWithoutBgScan,
            FrontToBackScanAlignment frontToBackScanAlignment,
            FrontToBackScanAlignment frontToBackWithoutBackgroundScanAlignment,
            int? panelId)

        {
            return new Fragment()
            {
                Name = name ?? throw new ArgumentNullException(nameof(name)),
                FrontScan = frontScan ?? throw new ArgumentNullException(nameof(frontScan)),
                BacktScan = backScan ?? throw new ArgumentNullException(nameof(backScan)),
                FrontScanWithoutBackground = frontWithoutBgScan ?? throw new ArgumentNullException(nameof(frontWithoutBgScan)),
                BackScanWithoutBackground = backWithoutBgScan ?? throw new ArgumentNullException(nameof(backWithoutBgScan)),
                FrontToBackScanAlignment = frontToBackScanAlignment,
                FrontToBackWithoutBackgroundScanAlignment = frontToBackWithoutBackgroundScanAlignment,
                PanelId = panelId
            };
        }
    }
}
