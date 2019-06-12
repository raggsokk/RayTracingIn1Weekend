using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace RayTracingIn1Weekend
{
    // Yet another arg parser.
    // sorry, didnt find one a liked.
    // and didnt have time to make a better one either.
    // WHY on earth did I decide to make my own. Havent saved any time on it either.
    public class ArgParser
    {
        // list of arguments added.
        internal List<ArgumentDefinition> pArguments;

        // lookup tables.
        internal SortedDictionary<string, ArgumentDefinition> pLongNames;
        internal SortedDictionary<char, ArgumentDefinition> pShortNames;

        public ArgParser()
        {            
            //TODO: lazy init these?
            pArguments = new List<ArgumentDefinition>();
            pLongNames = new SortedDictionary<string, ArgumentDefinition>();
            pShortNames = new SortedDictionary<char, ArgumentDefinition>();
        }

        public void AddArgument(string longName, Action<string> action, char? shortName = null, bool? Required = false, string HelpMessage = null)
        {
            var a = new ArgumentDefinition(){
                LongName = longName.ToLowerInvariant(),
                ShortName = shortName,
                Required = Required,
                HelpMessage = HelpMessage,
                UserAction = action,
            };

            // by default, arguments are case insensitive for now.
            if(!string.IsNullOrWhiteSpace(longName))
                pLongNames.Add(a.LongName, a);
                //pLongNames.Add(a.LongName.ToLowerInvariant(), a);
            else
                if(!shortName.HasValue)
                    throw new ArgumentException("Either longName or shortName is required ...");

            // shortName handling: defuault to use first longName if not in use.
            /*
            if(!a.ShortName.HasValue)
            {
                var sn = char.ToLowerInvariant(longName[0]);

                if(!pShortNames.ContainsKey(sn))
                    pShortNames.Add(sn, a);
            }
            */

            if(a.ShortName.HasValue)
                pShortNames.Add(a.ShortName.Value, a);

            pArguments.Add(a);
        }

        private void PrintHelpText(ArgumentDefinition ad, StringBuilder reuse)
        {
            /*
                Example:
                    -h,  --help            Prints this help message.
                */
            const int COLUMNSTART=30;
            const int LINELENGTH=48;
            reuse.Length = 0;

            reuse.Append("  ");

            if(ad.ShortName.HasValue)
                reuse.Append($"-{ad.ShortName.Value}");
            else
                reuse.Append("  ");

            if(!string.IsNullOrWhiteSpace(ad.LongName))
            {
                if(ad.ShortName.HasValue)
                    reuse.Append(", ");
                else
                    reuse.Append("  ");

                reuse.Append("--");
                reuse.Append(ad.LongName);
            }
            //var len = 20 - reuse.Length;
            reuse.Append(' ', COLUMNSTART - reuse.Length); // pad to colom size.

            if(ad.HelpMessage.Length < (LINELENGTH + 2))
            {
                reuse.Append(ad.HelpMessage);
                reuse.Append(Environment.NewLine);
            }
            else
            {
                // print first message and then loop print the rest.
                bool flagInline = false;
                int curpos = 0;
                do
                {
                    if(flagInline)
                        reuse.Append(' ', COLUMNSTART);
                        /*
                    if(!flagInline)
                        reuse.Append("  "); // this is set above.
                    else
                        reuse.Append(' ', COLUMNSTART + 2);
                         */

                    if((curpos + LINELENGTH + 1) >= ad.HelpMessage.Length)
                    {
                        // end of line.
                        reuse.Append(ad.HelpMessage, curpos, ad.HelpMessage.Length - curpos);
                        reuse.Append(Environment.NewLine);
                        break;
                    }

                    // we need to break up the line. find nearest space within x chars.
                    int posSpace = ad.HelpMessage.LastIndexOf(' ', curpos + LINELENGTH, 6);

                    if(posSpace == -1)
                    {
                        // no space found, split a word in 2 with a separator.
                        reuse.Append(ad.HelpMessage, curpos, LINELENGTH);
                        reuse.Append("-");
                        posSpace += LINELENGTH;
                        // newline appended at bottom of loop.
                    }
                    else
                    {
                        // write up to nearest space from end and loop.
                        reuse.Append(ad.HelpMessage, curpos, posSpace - curpos);
                        curpos += posSpace;
                        // newline appended at bottom of loop.
                    }

                    // add newline since we need to write more...
                    reuse.Append(Environment.NewLine);
                    // indicate that the next lines should be indentet? (probably should rename inline to indentet...)
                    flagInline = true;
                } while(curpos < ad.HelpMessage.Length);
            }

            // write out what we have found.
            Console.Write(reuse.ToString());
        }
        public bool Parse(string[] args)
        {
            //TODO: Use Environment.CommandLine instead to handle quotings better aka ' " etc.

            /*
                This code should eventually handle:
                    --argument=value
                    -a=value
                    -avalue

                Flags and Csv are not supported yet...                    
             */
            if(args.Length == 0 || args[0].ToLowerInvariant() == "--help")
            {
                var sb = new StringBuilder(); // reuse this

                // add help text as option.
                this.AddArgument("help", null, null, false, "This Help Message");

                Console.WriteLine($"Usage: <exe> [OPTION] ");
                Console.WriteLine("Renders an image using raytracing.");
                Console.WriteLine("");
                Console.WriteLine("Valid options to use are:");

                foreach(var a in pArguments)
                {
                    PrintHelpText(a, sb);
                }

                Console.WriteLine("");
                Console.WriteLine("Example:");
                Console.WriteLine("  <exe> --width=200 -h=100 --samples=10 -o=image.bmp");

                Environment.Exit(0);
            }

            foreach(var argstring in args)
            {
                ArgumentDefinition argdef = null;
                string value = null;

                if(argstring.Length >= 2)
                {
                    var eqpos = argstring.IndexOf('=', 2);

                    if(argstring.StartsWith("--") && argstring.Length >= 4)
                    {
                        string ln = string.Empty;

                        if(eqpos == -1)
                            ln = argstring.Substring(2).ToLowerInvariant();
                        else
                        {
                            ln = argstring.Substring(2, eqpos - 2).ToLowerInvariant();
                        }

                        if(pLongNames.TryGetValue(ln, out argdef))
                            value = argstring.Substring(eqpos + 1);
                    }
                    else if(argstring.StartsWith("-") || argstring.StartsWith("/"))
                    {
                        var sn = argstring[1];

                        if(pShortNames.TryGetValue(sn, out argdef))
                        {
                            if(argstring.Length > 2)
                            {
                                // ok try to parse value if exists.
                                if(eqpos == -1)
                                    value = argstring.Substring(2);
                                else
                                    value = argstring.Substring(eqpos + 1);
                            }
                        }
                    }
                }

                if(argdef == null)
                {
                    // no argument found.
                    Console.WriteLine($"Unknown argument '{argstring}'.Exiting...");
                    return false;
                }

                try{
                    argdef.UserAction(value);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Failed to call useraction on '{argstring}'. Exiting...");
                    Console.WriteLine("Exception: {0}", e.Message);
                    return false;
                }                
            }

            return true;
        }

        internal class ArgumentDefinition
        {
            public string LongName {get;set;}
            public char? ShortName {get;set;}
            public bool? Required {get;set;}
            public string HelpMessage {get;set;}
            public Action<string> UserAction {get;set;}
        }
    }
}