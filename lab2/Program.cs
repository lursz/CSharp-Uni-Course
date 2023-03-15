using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;

using lab2;

// ?? operator
// x ?? y (x & y are of the same type)
// if (x == null) return y
// else return x


class Program
{
    static public void Main(string[] args)
    {
        OsobaFizyczna osoba1 = new OsobaFizyczna("Ja", "Ty", "Oni", "11111111111", "BV123543523");
        OsobaFizyczna osoba2 = new OsobaFizyczna("A", "B", "C", "22222222222", "AW1233432432");
        OsobaFizyczna osoba3 = new OsobaFizyczna("D", "E", "F", "33333333333", "AI123543523");
        OsobaFizyczna osoba4 = new OsobaFizyczna("G", "H", "I", "44444444444", "AL123543523");

        RachunekBankowy r1 = new RachunekBankowy("1234567890", 1000, true, new List<PosiadaczRachunku> { osoba1, osoba2 });
        RachunekBankowy r2 = new RachunekBankowy("0987654321", 100.70m, false, new List<PosiadaczRachunku> { osoba3, osoba4 });
        RachunekBankowy r3 = new RachunekBankowy("1234567890", 3000, true, new List<PosiadaczRachunku> { osoba1, osoba2, osoba3, osoba4 });
        RachunekBankowy.DokonajTransakcji(r2, r1, 0.2m, "Przelew");
        RachunekBankowy.DokonajTransakcji(r1, r2, 5000m, "Przelew");

        System.Console.WriteLine(r1);
        System.Console.WriteLine(r2);
        System.Console.WriteLine(r3);
        
        r1 -= osoba1;
        r1 -= osoba1;

    }
}
