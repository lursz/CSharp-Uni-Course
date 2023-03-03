using System;
using System.Collections.Generic;
using System.IO;

/* -------------------------------------------------------------------------- */
/*                                  ZADANIE 2                                 */
/* -------------------------------------------------------------------------- */

class Program2
{
    static void Main2()
    {
        // Zapis do pliku
        string nazwaPliku = "plik.txt";
        StreamWriter sw;
        if (File.Exists(nazwaPliku))
            sw = new StreamWriter(nazwaPliku, append: true);
        else
            sw = new StreamWriter(nazwaPliku);


        String input = "";
        Console.WriteLine("Wprowadź tekst do pliku, aby zakończyć wprowadzenie napisz 'koniec!'");
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

