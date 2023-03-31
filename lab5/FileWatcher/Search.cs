using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileWatcher
{

    public class Search
    {

        string dirPath;
        string searchStr;

        public Search(string path, string word)
        {
            this.dirPath = path;
            this.searchStr = word;
        }


        public void Start()
        {

            Thread searchThread = new Thread(() => SearchFiles(dirPath, searchStr));
            searchThread.Start();

            Console.WriteLine("Search in progress...");
            Console.ReadLine(); // wait for user to press Enter key before exiting
        }

        public void SearchFiles(string dirPath, string searchStr)
        {
            try
            {
                foreach (string filePath in Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories))
                {
                    if (Path.GetFileName(filePath).Contains(searchStr))
                    {
                        Console.WriteLine(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}