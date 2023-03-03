
using System;
using System.Collections.Generic;
using System.IO;
/* -------------------------------------------------------------------------- */
/*                                  ZADANIE 5                                 */
/* -------------------------------------------------------------------------- */

class Program5
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("To few arguments");
            return;
        }
        StreamReader sr = new StreamReader(args[0]);

        int lines_count = 0;
        int signs_count = 0;
        float max_number = 0;
        float min_number = 0;
        float sum = 0;
        float average = 0;

        while(!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            lines_count++;
            signs_count += line.Length;

            float number = float.Parse(line);
            if (number > max_number)
                max_number = number;
            if (number < min_number)
                min_number = number;
            sum += number;
            
        }

        sr.Close();

        System.Console.WriteLine($"Liczba linijek: {lines_count}");
        System.Console.WriteLine($"Liczba znaków: {signs_count}");
        System.Console.WriteLine($"Maksymalna liczba: {max_number}");   
        System.Console.WriteLine($"Minimalna liczba: {min_number}");
        System.Console.WriteLine($"Suma: {sum}");
        System.Console.WriteLine($"Średnia: {sum/lines_count}");
    }
}

