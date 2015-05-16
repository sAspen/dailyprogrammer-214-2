using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileOfPaper
{
    class Program
    {
        static void Main(string[] args)
        {
            PileOfPaper pile = null;

            try
            {
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    string line;

                    if ((line = sr.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(' ');

                        pile = new PileOfPaper(Int32.Parse(tokens[0]), 
                            Int32.Parse(tokens[1]));

                        while ((line = sr.ReadLine()) != null)
                        {
                            tokens = line.Split(' ');
                            pile.Add(new Rectangle(Int32.Parse(tokens[0]),
                                Int32.Parse(tokens[1]),
                                Int32.Parse(tokens[2]),
                                Int32.Parse(tokens[3]),
                                Int32.Parse(tokens[4])));
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

                throw;
            }

            if (pile != null)
            {
                pile.Construct();
                Console.WriteLine(pile);

                return;
            }
        }
    }
}
