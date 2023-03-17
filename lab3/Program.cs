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
        Data data = new Data();
        data.readFromJson("favorite-tweets.jsonl");
        data.saveToXML("favorite-tweets.xml");
        data.readFromXML("favorite-tweets.xml");

        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict = data.frequencyOfWords();
        Dictionary <string,int> dict_top10 = new Dictionary<string, int>(); 
        
        dict_top10 = data.Top10Words();
        foreach (var i in dict_top10)
        {
            System.Console.WriteLine(i.Key + " " + i.Value);
        }

        System.Console.WriteLine("-----------");
        System.Console.WriteLine(data.findNewestTweet().CreatedAt);
        System.Console.WriteLine(data.findOldestTweet().CreatedAt);

        System.Console.WriteLine("-----------");
        System.Console.WriteLine(data.Top10IDF());
        

        
        




    }
}