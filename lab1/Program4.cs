using System;
using System.Collections.Generic;
using System.IO;
/* -------------------------------------------------------------------------- */
/*                                  ZADANIE 4                                 */
/* -------------------------------------------------------------------------- */
class Program4
{
    static void Main4(string[] args)
    {
        if (args.Length <= 5)
        {
            Console.WriteLine("To few arguments");
            return;
        }

        string filename = args[0];
        int n = int.Parse(args[1]);

        int seed = int.Parse(args[4]);
        bool is_integer = bool.Parse(args[5]);

        System.Console.WriteLine(filename);
        System.Console.WriteLine(n);
        // System.Console.WriteLine(min_value);
        // System.Console.WriteLine(max_value);
        System.Console.WriteLine(seed);
        System.Console.WriteLine(is_integer);

        Random random = new Random(seed);
        StreamWriter sw = new StreamWriter(filename);


        if (is_integer)
        {
            int min_value = int.Parse(args[2]);
            int max_value = int.Parse(args[3]);

            for (int i = 0; i < n; i++)
            {

                int number = random.Next(min_value, max_value);
                sw.WriteLine(number);
            }

        }
        else
        {
            float min_value = float.Parse(args[2]);
            float max_value = float.Parse(args[3]);

            for (int i = 0; i < n; i++)
            {
                float number = (float)random.NextDouble() * (max_value - min_value) + min_value;
                sw.WriteLine(number);
            }

        }
    sw.Close();
    }
}

