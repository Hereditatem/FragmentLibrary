using FragmentLibrary.Application;
using FragmentLibrary.Application.Dto;
using FragmentLibrary.Controllers.Viewmodels.Fragment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FragmentLibrary.Controllers
{
    [Route("api/[controller]")]
    public class FragmentController : Controller
    {
        private FragmetService _fragmentService;

        public FragmentController(FragmetService fragmentService)
        {
            _fragmentService = fragmentService ?? throw new ArgumentNullException(nameof(fragmentService));
        }

        [HttpGet]
        public async Task<ICollection<FragmentDetailsDto>> Index()
        {
            return await _fragmentService.GetAll();
        }

        [HttpGet]
        public async Task<FragmentDetailsDto> Index(long fragmentId)
        {
            return await _fragmentService.Find(fragmentId);
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewFragmentViewModel newFragment)
        {
            await _fragmentService.CreateFragment(await MapViewmodelToDto(newFragment));
            return Ok(/*new { count = files.Count, size, filePath }*/);
        }

        public async Task<FileStreamResult> FetchImage(string imageId)
        {
            var image = await _fragmentService.FindImage(imageId);
            return new FileStreamResult(new MemoryStream(image.Data), image.ContentType);
        }

        private async Task<NewFragmentDto> MapViewmodelToDto(NewFragmentViewModel viewmodel)
        {
            byte[] frontScanData = null,
                   backScanData = null,
                   frontScanWithoutBackgroumdData = null,
                   backScanWithoutBackgroumdData = null;

            if (viewmodel.FrontScan.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewmodel.FrontScan.CopyToAsync(memoryStream);
                    frontScanData = memoryStream.ToArray();
                }
            }


            if (viewmodel.BackScan.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewmodel.BackScan.CopyToAsync(memoryStream);
                    backScanData = memoryStream.ToArray();
                }
            }


            if (viewmodel.FrontScanWithoutBackground.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewmodel.FrontScanWithoutBackground.CopyToAsync(memoryStream);
                    frontScanWithoutBackgroumdData = memoryStream.ToArray();
                }
            }


            if (viewmodel.BackScanWithoutBackground.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewmodel.BackScanWithoutBackground.CopyToAsync(memoryStream);
                    backScanWithoutBackgroumdData = memoryStream.ToArray();
                }
            }

            return new NewFragmentDto()
            {
                Name = viewmodel.Name,
                FrontScan = frontScanData,
                FrontScanContentType = viewmodel.FrontScan.ContentType,
                BackScan = backScanData,
                BackScanContentType = viewmodel.BackScan.ContentType,
                FrontScanWithoutBackground = frontScanWithoutBackgroumdData,
                FrontScanWithoutBackgroundContentType = viewmodel.FrontScanWithoutBackground.ContentType,
                BackScanWithoutBackground = backScanWithoutBackgroumdData,
                BackScanWithoutBackgroundContentType = viewmodel.BackScanWithoutBackground.ContentType
            };

        }

    }
}
