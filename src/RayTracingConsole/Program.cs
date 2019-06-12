using System;
using System.IO;
using System.Diagnostics;

using RayTracingIn1Weekend;
using RayTracingIn1Weekend.Week1;



namespace RayTracingConsole
{
    //Class handles rendering options, output format, etc.
    class Program
    {
        static void Main(string[] args)
        {            
            // REALLY sofisticated argument parser ...
            var parser = new ArgParser();
            var options = new Options();

            //parser.AddArgument("width", (w) => options.Width = int.Parse(w));
            //parser.AddArgument("height", (h) => options.Height = int.Parse(h));
            //TODO: Make overload which handles value parsing.
            parser.AddArgument("width", (w) => {
                if(int.TryParse(w, out int i))
                    options.Width = i;
            }, 'w', true, "The width of the rendered image.");
            parser.AddArgument("height", (h) => {
                if(int.TryParse(h, out int i))
                    options.Height = i;
            }, 'h', true, "The height of the rendered image.");
            parser.AddArgument("samples", (s) => {
                if(int.TryParse(s, out int i))
                    options.Samples = i;
            }, 's', false, "The number of samples for each pixel.");
            parser.AddArgument("output", (o) => {
                options.Output = o;
            }, 'o', false, "The file to write rendered image to. The file extension determines which format its saved as.");

            if(!parser.Parse(args))
            {
                //Console.WriteLine("ERROR: Failed to parse arguments ... exitting.");
                // assume parser writes its own error messages.
                Environment.Exit(1);
            }

            //Console.WriteLine($"W={options.Width}, H={options.Height}, S={options.Samples}, O={options.Output}");
            //Environment.Exit(0);

            Run(options);
            
        }

        private static void Run(Options options)
        {
            //var extOutput = 

            Func<RayImage, Stream, bool> encoder = null;
            string filename = null;

            // if output not set, use default output filename.
            if(string.IsNullOrWhiteSpace(options.Output))
                filename = "image.bmp";
            else
                filename = options.Output;

            // TODO: implement encoders as a proper interface with factory methods instead of this switch.            
            //var ext = string.IsNullOrWhiteSpace(options.Output) ? string.Empty:
            var ext = Path.GetExtension(filename).ToUpperInvariant();
            switch(ext)
            {
                case ".TGA":
                    encoder = (r, s) => r.WriteTga(s);
                    break;
                case ".PPM":
                    encoder = (r, s) => r.WritePpm(s);
                    break;
                case ".BMP":
                    encoder = (r, s) => r.WriteBmp(s);
                    break;
                default:
                    encoder = (r, s) => r.WriteBmp(s);
                    filename += ".bmp";
                    break;
            }

            // code.
            var watch = new Stopwatch();
            watch.Start();

            // render.
            var img = RayTrace1.Render(options.Width, options.Height);

            watch.Stop();
            
            // remember if the file exists before opening it.
            var flagExists = File.Exists(filename);

            // save.
            using(var f = File.OpenWrite(filename))
            {
                encoder(img, f);

                // in case new output is smaller than the last rendered image, trunc the rest.
                if(flagExists)
                    f.SetLength(f.Position);
            }

            // done message.
            Console.WriteLine($"Image '{filename}' rendered in {watch.ElapsedMilliseconds}ms.");
        }
    }
}
