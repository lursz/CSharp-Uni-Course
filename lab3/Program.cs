using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


using lab3;


class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Reading from JSON, saving to XML, reading from XML\n");
        Data data = new Data();
        data.readFromJson("favorite-tweets.jsonl");
        data.saveToXML("favorite-tweets.xml");
        data.readFromXML("favorite-tweets.xml");

        System.Console.WriteLine("\nSorting by user name");
        data.sortByUserName();
        for (int i = 0; i < 10; i++)
        {
            System.Console.WriteLine(data.array_of_tweets[i].UserName);
        }

        System.Console.WriteLine("\nSorting by date");
        data.sortByDate();
        for (int i = 0; i < 10; i++)
        {
            System.Console.WriteLine(data.array_of_tweets[i].CreatedAt);
        }

        System.Console.WriteLine("\nOldest tweet: " + data.findOldestTweet().CreatedAt);
        System.Console.WriteLine("Newest tweet: " + data.findNewestTweet().CreatedAt);



        System.Console.WriteLine("\nFrequency of words:");
        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict = data.frequencyOfWords();
        for (int i = 0; i < 10; i++)
        {
            System.Console.WriteLine(dict.ElementAt(i).Key + " " + dict.ElementAt(i).Value);
        }

        System.Console.WriteLine("\nTop 10 words >= 5 letters:");
        Dictionary<string, int> dict_top10 = new Dictionary<string, int>();
        dict_top10 = data.Top10Words();
        foreach (var i in dict_top10)
        {
            System.Console.WriteLine(i.Key + " " + i.Value);
        }



        System.Console.WriteLine("\nTop 10 IDF:");
        Dictionary<string, double> dict_idf = new Dictionary<string, double>();
        dict_idf = data.Top10IDF();
        foreach (var i in dict_idf)
        {
            System.Console.WriteLine(i.Key + " " + i.Value);
        }








    }
}