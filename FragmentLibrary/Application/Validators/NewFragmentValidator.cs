using FragmentLibrary.Application.Dto;
using FragmentLibrary.Application.Exceptions;
using FragmentLibrary.Domain;
using System.Collections.Generic;

namespace FragmentLibrary.Application.Validators
{
    public class NewFragmentValidator
    {
        public const int MaxNameLength = 150;

        public static void ThrowIfNotValid(NewFragmentDto newFragment)
        {
            var validationErrors = new List<InvalidFieldMessage>();
            // Name
            if (string.IsNullOrEmpty(newFragment.Name))
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(newFragment.Name),
                        Message = $"Name is required and has max length of ${MaxNameLength}"
                    });
            }
            else if (newFragment.Name.Length >= MaxNameLength)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.Name),
                        Message = $"Name exceeds max length of ${MaxNameLength}"
                    });
            }

            // Front Scan
            if (newFragment.FrontScan == null
                || newFragment.FrontScan.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScan),
                        Message = $"Front Scan image is required"
                    });
            }
            if (string.IsNullOrEmpty(newFragment.FrontScanContentType)
                 || ImageFormat.FromMimeType(newFragment.FrontScanContentType) == null)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScan),
                        Message = $"Front Scan image must be png or jpeg formats"
                    });
            }

            // Back Scan
            if (newFragment.BackScan == null
                || newFragment.BackScan.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScan),
                        Message = $"Back Scan image is required"
                    });
            }
            if (string.IsNullOrEmpty(newFragment.BackScanContentType)
                 || ImageFormat.FromMimeType(newFragment.BackScanContentType) == null)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScan),
                        Message = $"Back Scan image must be png or jpeg formats"
                    });
            }

            // Front Scan Without background
            if (newFragment.FrontScanWithoutBackground == null
                || newFragment.FrontScanWithoutBackground.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScanWithoutBackground),
                        Message = $"Back Scan image is required"
                    });
            }
            if (string.IsNullOrEmpty(newFragment.FrontScanWithoutBackgroundContentType)
                || newFragment.FrontScanWithoutBackgroundContentType != ImageFormat.Png.MimeType)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScanWithoutBackground),
                        Message = $"Back Scan image must be png"
                    });
            }

            // Back Scan Without background
            if (newFragment.BackScanWithoutBackground == null
                || newFragment.BackScanWithoutBackground.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScanWithoutBackground),
                        Message = $"Back Scan image is required"
                    });
            }
            if (newFragment.BackScanWithoutBackgroundContentType != ImageFormat.Png.MimeType)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScanWithoutBackground),
                        Message = $"Back Scan image must be png"
                    });
            }

            if (validationErrors.Count > 0)
            {
                throw new ValidationException(validationErrors);
            }
        }
    }
}
