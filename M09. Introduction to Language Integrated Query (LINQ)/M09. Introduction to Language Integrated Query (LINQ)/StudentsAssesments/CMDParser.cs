using System;
using System.Collections.Generic;
using NDesk.Options;

namespace StudentsAssesments
{
    public class CMDParser
    {
        public Dictionary<string, string> UserOptions { get; set; }
        public bool Interrupt { get; set; }

        public CMDParser()
        {
            Interrupt = false;
            UserOptions = new Dictionary<string, string>();
        }

        public Dictionary<string, string> ParseOptions(string options)
        {
            var showHelp = false;
            Interrupt = false;
            UserOptions.Clear();
            var p = new OptionSet()
                {
                    {
                        "name=", "the {NAME} of student.", v => UserOptions.Add("name", v)
                    },

                    {
                        "minmark=", "the {MIN} mark to retrieve.", v => UserOptions.Add("minmark", v)
                    },

                    {
                        "maxmark=", "the {MAX} mark to retrieve.", v => UserOptions.Add("maxmark", v)
                    },

                    {
                        "datefrom=", "{DATE FROM} tests.", v => UserOptions.Add("datefrom", v)
                    },

                    {
                        "dateto=", "{DATE TO} tests.", v => UserOptions.Add("dateto", v)
                    },

                    {
                        "test=", "{TEST} name.", v => UserOptions.Add("test", v)
                    },

                    {
                        "sort=", "{SORT} the retrieved list.", v => UserOptions.Add("sort", v)
                    },

                    {
                        "<>", v => { if (UserOptions.ContainsKey("sort")) UserOptions.Add("order", v);}
                    },

                    { "h|help",  "show this message and exit",
                      v => showHelp = v != null
                    },
            };

            List<string> extra;

            try
            {
                extra = p.Parse(options.Split());
            }
            catch (OptionException e)
            {
                Console.Write("error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help' for more information.\n");
                Interrupt = true;
            }

            if (showHelp)
            {
                ShowHelp(p);
                Interrupt = true;
            }

            ValidateOptions(UserOptions);

            return UserOptions;
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: [OPTIONS]+ filter criteria");
            Console.WriteLine("Retrieve a list of student's marks filtered by user.");
            Console.WriteLine("If no criteria is specified, complete list of marks retrieved.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        private void ValidateOptions(Dictionary<string, string> options)
        {
            if (options.ContainsKey("minmark"))
            {
                if (!int.TryParse(options["minmark"], out _))
                {
                    Console.WriteLine("Wrong argument minmark \"{0}\"!", options["minmark"]);
                    Interrupt = true;
                }
            }

            if (options.ContainsKey("maxmark"))
            {
                if (!int.TryParse(options["maxmark"], out _))
                {
                    Console.WriteLine("Wrong argument maxmark \"{0}\"!", options["maxmark"]);
                    Interrupt = true;
                };
            }

            if (options.ContainsKey("datefrom"))
            {
                if (!DateTime.TryParse(options["datefrom"], out _))
                {
                    Console.WriteLine("Wrong argument datefrom \"{0}\"!", options["datefrom"]);
                    Interrupt = true;
                };
            }

            if (options.ContainsKey("dateto"))
            {
                if (!DateTime.TryParse(options["dateto"], out _))
                {
                    Console.WriteLine("Wrong argument dateto \"{0}\"!", options["dateto"]);
                    Interrupt = true;
                };
            }

            List<string> validSortOptions = new() { "name", "mark", "date", "test" };

            if (options.ContainsKey("sort"))
            {
                if (!validSortOptions.Contains(options["sort"]))
                {
                    Console.WriteLine("Wrong sort parameter \"{0}\"!", options["sort"]);
                    Interrupt = true;
                };
            }

            if (options.ContainsKey("order"))
            {
                if (!options["order"].Contains("asc") && !options["order"].Contains("desc"))
                {
                    Console.WriteLine("Wrong order parameter \"{0}\"!", options["order"]);
                    Interrupt = true;
                };
            }
        }
    }
}
