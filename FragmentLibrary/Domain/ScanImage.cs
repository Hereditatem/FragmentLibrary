using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FragmentLibrary.Domain
{
    public class ImageFormat
    {
        protected ImageFormat(string extension, string mimeType)
        {
            Extension = extension;
            MimeType = mimeType;
        }

        public string MimeType { get; private set; }

        public string Extension { get; private set; }

        public static readonly ImageFormat Png = new ImageFormat("png", "image/png");

        public static readonly ImageFormat Jpeg = new ImageFormat("jpeg", "image/jpeg");

        public static IEnumerable<ImageFormat> GetAll()
        {
            yield return ImageFormat.Png;
            yield return ImageFormat.Jpeg;
        }

        public static ImageFormat FromExtension(string extension)
        {
            return GetAll().FirstOrDefault(f => f.Extension == extension);
        }

        public static ImageFormat FromMimeType(string mimeType)
        {
            return GetAll().FirstOrDefault(f => f.MimeType == mimeType);
        }
    }

    public class ScanImage
    {
        protected ScanImage()
        {

        }

        public string Id { get; private set; }

        public string GenericFileName { get; private set; }

        public ImageFormat Format { get; private set; }

        public byte[] Data { get; private set; }

        public static ScanImage BuildFromData(byte[] data, ImageFormat format)
        {
            return new ScanImage()
            {
                Data = data ?? throw new ArgumentNullException(nameof(data)),
                Format = format ?? throw new ArgumentNullException(nameof(format))
            };
        }

        public static ScanImage BuidFromRepository(string id, byte[] data, ImageFormat format)
        {
            return new ScanImage()
            {
                Data = data ?? throw new ArgumentNullException(nameof(data)),
                Format = format ?? throw new ArgumentNullException(nameof(format)),
                Id = id,
            };
        }
    }
}
