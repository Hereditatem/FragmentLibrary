using FragmentLibrary.Application.Dto;
using FragmentLibrary.Application.Validators;
using FragmentLibrary.Controllers.Viewmodels.Fragment;
using FragmentLibrary.Domain;
using FragmentLibrary.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System.IO;

namespace FragmentLibrary.Application
{

    public class FragmetService
    {
        public const int LARGE_IMAGE_SIZE = 1024;
        public const int MEDIUM_IMAGE_SIZE = 512;
        public const int SMALL_IMAGE_SIZE = 256;

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

            var originalFrontScan = ScanImage.BuildFromData(newFragment.FrontScan,
                ImageFormat.FromMimeType(newFragment.FrontScanContentType));
            var originalBackScan = ScanImage.BuildFromData(newFragment.BackScan,
                ImageFormat.FromMimeType(newFragment.BackScanContentType));

            var originalFrontScanWithoutBg = ScanImage.BuildFromData(newFragment.FrontScanWithoutBackground,
                ImageFormat.FromMimeType(newFragment.FrontScanWithoutBackgroundContentType));

            var largeFrontScanWithoutBg = ScanImage.BuildFromData(
                    await ResizeImage(newFragment.FrontScanWithoutBackground, LARGE_IMAGE_SIZE),
                    ImageFormat.Png
                );

            var mediumFrontScanWithoutBg = ScanImage.BuildFromData(
                    await ResizeImage(newFragment.FrontScanWithoutBackground, MEDIUM_IMAGE_SIZE),
                    ImageFormat.Png
                );

            var smallFrontScanWithoutBg = ScanImage.BuildFromData(
                    await ResizeImage(newFragment.FrontScanWithoutBackground, SMALL_IMAGE_SIZE),
                    ImageFormat.Png
                );

            var originalBackScanWithoutBg = ScanImage.BuildFromData(newFragment.BackScanWithoutBackground,
                ImageFormat.FromMimeType(newFragment.BackScanWithoutBackgroundContentType));

            var largeBackScanWithoutBg = ScanImage.BuildFromData(
                    await ResizeImage(newFragment.BackScanWithoutBackground, LARGE_IMAGE_SIZE),
                    ImageFormat.Png
                );

            var mediumBackScanWithoutBg = ScanImage.BuildFromData(
                    await ResizeImage(newFragment.BackScanWithoutBackground, MEDIUM_IMAGE_SIZE),
                    ImageFormat.Png
                );

            var smallBackScanWithoutBg = ScanImage.BuildFromData(
                    await ResizeImage(newFragment.BackScanWithoutBackground, SMALL_IMAGE_SIZE),
                    ImageFormat.Png
                );


            //TODO Make atomic
            var frontScanImageId = await _fragmentImageRepository.Add(originalFrontScan);
            var backScanImageId = await _fragmentImageRepository.Add(originalBackScan);
            var originalFrontScanWithoutBgImageId = await _fragmentImageRepository.Add(originalFrontScanWithoutBg);
            var largeFrontScanWithoutBgImageId = await _fragmentImageRepository.Add(largeFrontScanWithoutBg);
            var mediumFrontScanWithoutBgImageId = await _fragmentImageRepository.Add(mediumFrontScanWithoutBg);
            var smallFrontrontScanWithoutBgImageId = await _fragmentImageRepository.Add(smallFrontScanWithoutBg);
            var originalBackScanWithoutBgImageId = await _fragmentImageRepository.Add(originalBackScanWithoutBg);
            var largeBackScanWithoutBgImageId = await _fragmentImageRepository.Add(largeBackScanWithoutBg);
            var mediumBackScanWithoutBgImageId = await _fragmentImageRepository.Add(mediumBackScanWithoutBg);
            var smallBackScanWithoutBgImageId = await _fragmentImageRepository.Add(smallBackScanWithoutBg);

            var savedFragment = await _fragmentRepository.Add(Fragment.BuildTransient(
                                    newFragment.Name,
                                    new Scan(frontScanImageId),
                                    new Scan(backScanImageId),
                                    // TODO - Process Images
                                    new ProcessedScan(originalFrontScanWithoutBgImageId) { 
                                        LargeImageId = largeFrontScanWithoutBgImageId,
                                        MediumImageId = mediumFrontScanWithoutBgImageId,
                                        SmallImageId = smallFrontrontScanWithoutBgImageId
                                    },
                                    new ProcessedScan(originalBackScanWithoutBgImageId)
                                    {
                                        LargeImageId = largeBackScanWithoutBgImageId,
                                        MediumImageId = mediumBackScanWithoutBgImageId,
                                        SmallImageId = smallBackScanWithoutBgImageId
                                    }));

            return savedFragment.Id;
        }


        private async Task<byte[]> ResizeImage(byte[] originalImage, int size)
        {
            return await Task.Run(() =>
            {
                var decoder = new PngDecoder();
                using (var image = Image.Load(originalImage, new PngDecoder()))
                {
                    image.Mutate(c => c.Resize(new ResizeOptions()
                    {
                        Size = new Size(size),
                        Mode = ResizeMode.Max,
                        Position = AnchorPositionMode.Center
                    }));
                    using (var memoryStream = new MemoryStream())
                    {
                        image.SaveAsPng(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            });
        }

        public async Task<ICollection<FragmentDetailsDto>> GetAll()
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

        public async Task<ImageDto> FindImage(string imageId)
        {
            var image = await _fragmentImageRepository.Find(imageId);

            return new ImageDto()
            {
                Data = image.Data,
                ContentType = image.Format.MimeType
            };
        }
    }
}
