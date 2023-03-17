using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;

namespace lab3
{
    public class Data
    {
        public List<Tweet>? array_of_tweets { get; set; }
        string date_format = "MMMM d, yyyy 'at' hh:mmtt";


        /* ------------------------------------------------------------------------- */
        public Data()
        {
            array_of_tweets = new List<Tweet>();
        }

        /* ------------------------------ Reading data ------------------------------ */
        public void readFromJson(string json_name)
        {
            var file = File.ReadAllLines(json_name);
            foreach (var line in file)
            {
                var tweet = JsonSerializer.Deserialize<Tweet>(line);
                array_of_tweets.Add(tweet);
            }

        }

        public void convToXML(string xml_name)
        {
            StreamWriter writer = new StreamWriter(xml_name);
            foreach (var i in array_of_tweets)
            {
                var x = new System.Xml.Serialization.XmlSerializer(i.GetType());
                x.Serialize(writer, i);
            }
        }

        public void readFromXML(string xml_name)
        {
            StreamReader reader = new StreamReader(xml_name);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(Tweet));
            while (reader.Peek() != -1)
            {
                Tweet tweet = (Tweet)x.Deserialize(reader);
                array_of_tweets.Add(tweet);
            }

        }

        /* --------------------------------- Sorting -------------------------------- */
        public void sortByUserName()
        {
            array_of_tweets.Sort((x, y) => x.UserName.CompareTo(y.UserName));
        }
        public void sortByDate()
        {
            array_of_tweets.Sort((x, y) => DateTime.ParseExact(x.CreatedAt, date_format, null).CompareTo(DateTime.ParseExact(y.CreatedAt, date_format, null)));
        }
        public void sortByNameAndDate()
        {
            array_of_tweets.OrderBy(x => x.UserName).ThenBy(x => DateTime.ParseExact(x.CreatedAt, date_format, null));
        }

        public Tweet findOldestTweet()
        {
            var temp = array_of_tweets.OrderBy(x => DateTime.ParseExact(x.CreatedAt, date_format, null));
            return temp.First();
        }
        public Tweet findNewestTweet()
        {
            var temp = array_of_tweets.OrderBy(x => DateTime.ParseExact(x.CreatedAt, date_format, null));
            return temp.Last();
        }


        /* ------------------------------- Dictionary ------------------------------- */

        public Dictionary<string, List<Tweet>> dictionaryOfTweetsByUser()
        {
            Dictionary<string, List<Tweet>> dict = new Dictionary<string, List<Tweet>>();

            foreach (var i in array_of_tweets)
            {
                if (dict.ContainsKey(i.UserName))
                    dict[i.UserName].Add(i);
                else
                    dict.Add(i.UserName, new List<Tweet> { i });
            }
            return dict;
        }


        /* --------------------------- Frequency of words --------------------------- */
        public Dictionary<string, int> frequencyOfWords()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var i in array_of_tweets)
            {
                // string [] words = Tweet.reg.Split(i.Text);
                string[] words = i.Text.Split(' ', '.', ',', '!', '?', ':', ';', '-', '(', ')', '[', ']', '{', '}', '/', '\\', '"', '\'', '\t', '\n', '\r');
                foreach (var word in words)
                {
                    String word_low = word.ToLower();
                    if (dict.ContainsKey(word_low))
                        dict[word_low]++;
                    else
                        dict.Add(word_low, 1);
                }
            }
            dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            System.Console.WriteLine(dict);
            return dict;
        }

        public Dictionary<string, int> Top10Words()
        {
            Dictionary<string, int> dict = frequencyOfWords();
            Dictionary<string, int> result = new Dictionary<string, int>();
            int counter = 0;
            for (int i = 0; i < dict.Count; i++)
            {
                if (dict.ElementAt(i).Key.Length >= 5)
                {
                    result.Add(dict.ElementAt(i).Key, dict.ElementAt(i).Value);
                    counter++;
                }
                if (counter == 10)
                    break;
            }
            return result;
        }


        /* ----------------------------------- IDF ---------------------------------- */
        public void Top10IDF()
        {

        }

        public void TermFrequency()
        {



        }

        /* -------------------------------- Printing -------------------------------- */
        public void printAllTweets()
        {
            foreach (var i in array_of_tweets)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public void printAllUserNames()
        {
            foreach (var i in array_of_tweets)
            {
                Console.WriteLine(i.UserName);
            }
        }
        public void printAllDates()
        {
            foreach (var i in array_of_tweets)
            {
                Console.WriteLine(i.CreatedAt);
            }
        }

    }



}