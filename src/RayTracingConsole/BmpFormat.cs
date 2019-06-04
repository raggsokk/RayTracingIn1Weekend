using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Buffers;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend;
using RayTracingIn1Weekend.Week1;

namespace RayTracingConsole
{
    public static class BmpFormatExtensions
    {
        /// <summary>
        /// BITMAPFILEHEADER
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BmpFileHeader 
        {
            public ushort Signature;
            public int FileSize;
            private int Reserved;
            public int DataOffset;
        }

        /// <summary>
        /// BITMAPINFOHEADER
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BmpInfoHeader
        {
            public int SizeOfStruct;
            public int Width;
            public int Height;
            public short Planes;
            public short BitsPerPixel;
            public int Compression;
            public int ImageSizeIfCompressed;
            public int XPixelsPerM;
            public int YPixelsPerM;
            public int ColorsUsed;
            public int ImportantColors;

        }

        public static bool WriteBmp(this RayImage img, Stream output)
        {
            try
            {
                var sizeFileHeader = Marshal.SizeOf<BmpFileHeader>();
                //const int sizeFileHeader = 14;
                var sizeInfoHeader = Marshal.SizeOf<BmpInfoHeader>();
                //const int sizeInfoHeader = 40;
                var sizePixels = img.Width * img.Height * 3;

                var buffer = new byte[sizeFileHeader + sizeInfoHeader + sizePixels];
                var span = new Span<byte>(buffer);

                // write file header.
                ref BmpFileHeader fileHeader = ref MemoryMarshal.Cast<byte, BmpFileHeader>(span)[0];
                fileHeader.Signature = (int)('B') + ((int)('M') << 8);
                fileHeader.FileSize = buffer.Length;
                fileHeader.DataOffset = sizeFileHeader + sizeInfoHeader;

                // write info header.
                ref BmpInfoHeader infoHeader = ref MemoryMarshal.Cast<byte, BmpInfoHeader>(span.Slice(sizeFileHeader, sizeInfoHeader))[0];
                infoHeader.SizeOfStruct = sizeInfoHeader;
                infoHeader.Width = img.Width;
                infoHeader.Height = img.Height;
                infoHeader.Planes = 1;
                infoHeader.BitsPerPixel = 24;
                infoHeader.Compression = 0; // no compression.
                infoHeader.ImageSizeIfCompressed = 0; // no compression;
                infoHeader.ImportantColors = 0; // all colors important.

                // convert rgb3f to bgr8
                var slicePixels = span.Slice(sizeFileHeader + sizeInfoHeader, sizePixels);
                FormatUtils.ConvertBGR8(img, ref slicePixels);

                output.Write(span);

                output.Flush();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
