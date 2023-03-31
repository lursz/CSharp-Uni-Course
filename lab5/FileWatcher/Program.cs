using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using FileWatcher;



class Program
{
    private static void Main(string[] args)
    {
        // MonitorFolder fileWatcher = new MonitorFolder(@"/mnt/e/test");
        // fileWatcher.Start();

        Search search = new Search(@"/mnt/e/test", "test");
        search.Start();
        
    }
}