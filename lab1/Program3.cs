using System;
using System.Collections.Generic;
using System.IO;
/* -------------------------------------------------------------------------- */
/*                                  ZADANIE 3                                 */
/* -------------------------------------------------------------------------- */
class Program3
{
    static void Main23(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine("To few arguments");
            return;
        }

        StreamReader sr = new StreamReader(args[0]);
        String to_find = args[1];

        int line_number = 0;

        while (!sr.EndOfStream)
        {
            line_number++;
            string line = sr.ReadLine();
            int position = line.IndexOf(to_find);
            if (position != -1)
            {
                Console.WriteLine($"linijka: {line_number}, pozycja: {position}");
            }
            
        }
        sr.Close();

    }
}

