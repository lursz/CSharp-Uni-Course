using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using lab5;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Ex 1:");
        Ex1 ex1 = new Ex1(4,5);
        ex1.Start();
    }
}