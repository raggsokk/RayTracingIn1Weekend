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
    public static class TgaFormatExtensions
    {
        /*
         * IMPORTENT: 
         *  Use pack = 1 to prevent padding added by compiler for more efficient access.
         *  This is really needed when mapping to file access.
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TgaHeader
        {
            public byte idLenght;
            public byte ColorMapType;
            public byte DataTypeCode;
            public short ColorMapOrigin;
            public short ColorMapLength;
            public byte ColorMapDepth;
            public short XOrigin;
            public short YOrigin;
            public short Width;
            public short Height;
            public byte BitsPerPixel;
            public byte ImageDescriptor;
        }

        public static bool WriteTga(this RayImage img, Stream output)
        {
            try
            {
                var sizeHeader = Marshal.SizeOf<TgaHeader>();
                //const int sizeHeader = 18;
                var sizePixels = img.Width * img.Height * 3;
                
                var buffer = new byte[sizeHeader + sizePixels];
                var span = new Span<byte>(buffer);

                // write header.
                // IMPORTENT TO USE REF HERE.
                ref TgaHeader header = ref MemoryMarshal.Cast<byte, TgaHeader>(span)[0];
                //var header = MemoryMarshal.Cast<byte, TgaHeader>(span)[0];
                //var header = MemoryMarshal.AsRef<TgaHeader>(span.Slice(0, sizeHeader));                
                header.DataTypeCode = 2;
                header.Width = (short)img.Width;
                header.Height = (short)img.Height;
                header.BitsPerPixel = 24;


                // convert rgb3f to bgr8
                var slicePixels = span.Slice(sizeHeader, sizePixels);
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
