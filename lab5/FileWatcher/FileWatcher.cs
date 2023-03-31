using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class FileWatcherObject
    {
        string path;
        Thread thread;

        public FileWatcherObject(string path)
        {
            this.path = path;
        }

        public void Start()
        {
            Console.WriteLine($"Monitoring directory: {path}");

            var watcher = new FileSystemWatcher(path);
            watcher.IncludeSubdirectories = false;

            watcher.Created += (sender, e) => Console.WriteLine($"Created: {e.Name}");
            watcher.Deleted += (sender, e) => Console.WriteLine($"Deleted: {e.Name}");
            watcher.Changed += (sender, e) => Console.WriteLine($"Changed: {e.Name}");
            watcher.Renamed += (sender, e) => Console.WriteLine($"Renamed: {e.OldName} -> {e.Name}");
            watcher.EnableRaisingEvents = true;

            while (true)
            {
                System.Console.WriteLine("Press 'q' to quit");
                var input = Console.ReadKey();

                if (input.KeyChar == 'q')
                {
                    watcher.EnableRaisingEvents = false;
                    break;
                }
            }
            Console.WriteLine("Stopped monitoring directory.");

        }
    }
}