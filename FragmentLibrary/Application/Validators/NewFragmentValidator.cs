using System;
using System.Collections.Generic;
using FragmentLibrary.Application.Dto;
using FragmentLibrary.Application.Exceptions;
using FragmentLibrary.Domain;

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
            if (newFragment.FrontScan.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScan),
                        Message = $"Front Scan image is required"
                    });
            }
            if (newFragment.FrontScan.ContentType == null
                 || ImageFormat.FromMimeType(newFragment.FrontScan.ContentType) == null)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScan),
                        Message = $"Front Scan image must be png or jpeg formats"
                    });
            }

            // Back Scan
            if (newFragment.BackScan.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScan),
                        Message = $"Back Scan image is required"
                    });
            }
            if (newFragment.BackScan.ContentType == null
                 || ImageFormat.FromMimeType(newFragment.BackScan.ContentType) == null)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScan),
                        Message = $"Back Scan image must be png or jpeg formats"
                    });
            }

            // Front Scan Without background
            if (newFragment.FrontScanWithoutBackground.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScanWithoutBackground),
                        Message = $"Back Scan image is required"
                    });
            }
            if (newFragment.FrontScanWithoutBackground.ContentType != ImageFormat.Png.MimeType)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.FrontScanWithoutBackground),
                        Message = $"Back Scan image must be png"
                    });
            }

            // Back Scan Without background
            if (newFragment.BackScanWithoutBackground.Length == 0)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScanWithoutBackground),
                        Message = $"Back Scan image is required"
                    });
            }
            if (newFragment.BackScanWithoutBackground.ContentType != ImageFormat.Png.MimeType)
            {
                validationErrors.Add(
                    new InvalidFieldMessage
                    {
                        FieldName = nameof(NewFragmentDto.BackScanWithoutBackground),
                        Message = $"Back Scan image must be png"
                    });
            }

            if (validationErrors.Count > 0)
                throw new ValidationException(validationErrors);
        }
    }
}
