using System;
using System.IO;
using System.Threading.Tasks;
using FragmentLibrary.Application.Dto;
using FragmentLibrary.Application.Validators;
using FragmentLibrary.Repository;

namespace FragmentLibrary.Application
{
    public class FragmetService
    {
        private FragmentRepository _fragmentRepository;
        private FragmentImageRepository _fragmentImageRepository;

        public FragmetService(FragmentRepository fragmentRepository, FragmentImageRepository fragmentImageRepository)
        {
            _fragmentRepository = fragmentRepository;
            _fragmentImageRepository = fragmentImageRepository;
        }

        public async Task CreateFragment(NewFragmentDto newFragment)
        {
            NewFragmentValidator.ThrowIfNotValid(newFragment);
            byte[] frontScanData, backScanData, frontScanWitoutBgData, backScanWithoutBgData;
            using (var stream = newFragment.FrontScan.OpenReadStream())
            {
               
            }
        }
    }
}
