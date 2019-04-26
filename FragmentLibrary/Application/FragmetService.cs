using FragmentLibrary.Application.Dto;
using FragmentLibrary.Application.Validators;
using FragmentLibrary.Controllers.Viewmodels.Fragment;
using FragmentLibrary.Domain;
using FragmentLibrary.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FragmentLibrary.Application
{
    public class FragmetService
    {
        private readonly FragmentRepository _fragmentRepository;
        private readonly FragmentImageRepository _fragmentImageRepository;

        public FragmetService(FragmentRepository fragmentRepository, FragmentImageRepository fragmentImageRepository)
        {
            _fragmentRepository = fragmentRepository;
            _fragmentImageRepository = fragmentImageRepository;
        }

        public async Task<long> CreateFragment(NewFragmentDto newFragment)
        {
            NewFragmentValidator.ThrowIfNotValid(newFragment);
            var frontScan = ScanImage.BuildFromData(newFragment.FrontScan, ImageFormat.FromMimeType(newFragment.FrontScanContentType));
            var backScan = ScanImage.BuildFromData(newFragment.BackScan, ImageFormat.FromMimeType(newFragment.BackScanContentType));
            var frontScanWithoutBg = ScanImage.BuildFromData(newFragment.FrontScanWithoutBackground, ImageFormat.FromMimeType(newFragment.FrontScanWithoutBackgroundContentType));
            var backScanWithoutBg = ScanImage.BuildFromData(newFragment.BackScanWithoutBackground, ImageFormat.FromMimeType(newFragment.BackScanWithoutBackgroundContentType));
            //TODO Make atomic
            var frontScanImageId = await _fragmentImageRepository.Add(frontScan);
            var backScanImageId = await _fragmentImageRepository.Add(backScan);
            var frontScanWithoutBgImageId = await _fragmentImageRepository.Add(frontScanWithoutBg);
            var backScanWithoutBgImageId = await _fragmentImageRepository.Add(backScanWithoutBg);

            var fragment = await _fragmentRepository.Add(Fragment.BuildTransient(
                                    newFragment.Name,
                                    new Scan(frontScanImageId),
                                    new Scan(backScanImageId),
                                    // TODO - Process Images
                                    new ProcessedScan(frontScanWithoutBgImageId),
                                    new ProcessedScan(backScanWithoutBgImageId)));
            return fragment.Id;

        }

        internal async Task<ICollection<FragmentDetailsDto>> GetAll()
        {
            var result = new List<FragmentDetailsDto>();
            foreach (var fragment in await _fragmentRepository.List())
                result.Add(MapToDto(fragment));
            return result;
        }

        public async Task<FragmentDetailsDto> Find(long id)
        {
            var fragment = await _fragmentRepository.Find(id);
            return MapToDto(fragment);
        }

        private static FragmentDetailsDto MapToDto(Fragment fragment)
        {
            return new FragmentDetailsDto()
            {
                Id = fragment.Id,
                Name = fragment.Name,
                FrontScan = new ScanImageInfoDto()
                {
                    OriginalImageID = fragment.FrontScan.OriginalImageId
                },
                BackScan = new ScanImageInfoDto()
                {
                    OriginalImageID = fragment.BacktScan.OriginalImageId
                },
                FrontScanWithoutBackground = new ScanImageInfoDto()
                {
                    OriginalImageID = fragment.FrontScanWithoutBackground.OriginalImageId,
                    LargeImageID = fragment.FrontScanWithoutBackground.LargeImageId,
                    MediumImageID = fragment.FrontScanWithoutBackground.MediumImageId,
                    SmallImageID = fragment.FrontScanWithoutBackground.SmallImageId
                },
                BackScanWithoutBackground = new ScanImageInfoDto()
                {
                    OriginalImageID = fragment.BackScanWithoutBackground.OriginalImageId,
                    LargeImageID = fragment.BackScanWithoutBackground.LargeImageId,
                    MediumImageID = fragment.BackScanWithoutBackground.MediumImageId,
                    SmallImageID = fragment.BackScanWithoutBackground.SmallImageId
                },
                FrontToBackAlignment = new AlignmentDto()
                {
                    Left = fragment.FrontToBackWithoutBackgroundScanAlignment.Left,
                    Right = fragment.FrontToBackWithoutBackgroundScanAlignment.Right,
                    Scale = fragment.FrontToBackWithoutBackgroundScanAlignment.Scale,
                    Angle = fragment.FrontToBackWithoutBackgroundScanAlignment.Angle,
                }
            };
        }
    }
}
