using System;
using System.Collections.Generic;
using System.IO;


/* -------------------------------------------------------------------------- */
/*                                      2                                     */
/* -------------------------------------------------------------------------- */
class Program2
{
    static public void Exec()
    {
        // Zapis do pliku
        string nazwaPliku = "plik.txt";
        StreamWriter sw;
        if (File.Exists(nazwaPliku))
            sw = new StreamWriter(nazwaPliku, append: true);
        else
            sw = new StreamWriter(nazwaPliku);

        String input = "";
        Console.WriteLine("Input your text, then type 'koniec!' to exit insert mode");
        input = Console.ReadLine();
        while (input != "koniec!")
        {
            sw.WriteLine(input);
            input = Console.ReadLine();
        }
        //close stream
        sw.Close();



    }

}

/* -------------------------------------------------------------------------- */
/*                                      3                                     */
/* -------------------------------------------------------------------------- */
class Program3
{
    static public void Exec(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine("Too few arguments");
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
                Console.WriteLine($"line: {line_number}, position: {position}");
            }
        }
        sr.Close();

    }
}


/* -------------------------------------------------------------------------- */
/*                                      4                                     */
/* -------------------------------------------------------------------------- */
class Program4
{
    static public void Exec(string[] args)
    {
        if (args.Length <= 5)
        {
            Console.WriteLine("Too few arguments");
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



/* -------------------------------------------------------------------------- */
/*                                      5                                     */
/* -------------------------------------------------------------------------- */
class Program5
{
    static public void Exec(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Too few arguments");
            return;
        }
        StreamReader sr = new StreamReader(args[0]);

        int lines_count = 0;
        int signs_count = 0;
        float max_number = 0;
        float min_number = 0;
        float sum = 0;

        while (!sr.EndOfStream)
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
        System.Console.WriteLine($"Średnia: {sum / lines_count}");
    }
}

/* -------------------------------------------------------------------------- */
/*                                MAIN PROGRAM                                */
/* -------------------------------------------------------------------------- */
class Program
{
    static void Main(string[] args)
    {
        Program2.Exec();
        // Program3.Exec(args);
        // Program4.Exec(args);
        // Program5.Exec(args);
    }
}