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
        data.convToXML("favorite-tweets.xml");

        data.sortByDate();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(data.array_of_tweets[i].CreatedAt);
        }
        System.Console.WriteLine("-----------");
        System.Console.WriteLine(data.findNewestTweet().CreatedAt);
        System.Console.WriteLine(data.findOldestTweet().CreatedAt);

        System.Console.WriteLine(data.Top10Words());

        
        




    }
}