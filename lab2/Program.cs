using System;
using System.Collections.Generic;
using System.IO;


/* -------------------------------------------------------------------------- */
/*                                      1                                     */
/* -------------------------------------------------------------------------- */
class Program1
{
    static public void Exec()
    {
        // Zapis do pliku
        string nazwaPliku = "plik.txt";
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
/*                                MAIN PROGRAM                                */
/* -------------------------------------------------------------------------- */
class Program
{
    static void Main(string[] args)
    {
        Program1.Exec();
        // Program2.Exec(args);
        // Program3.Exec(args);
        // Program4.Exec(args);
    }
}