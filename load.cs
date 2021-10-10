using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace textTycoonWorker
{
    static class loadProcess
    {
        private static bool fileCheck = File.Exists(globals.path);
        private static Dictionary<string, object> items;
        private static bool loadFile()
        {
            try
            {
                string[] allLines = File.ReadAllLines(globals.path);
                items = new Dictionary<string, object>();

                foreach (string lines in allLines)
                {
                    string[] split = lines.Split(',');

                    items[split[0].ToString()] = split[1];
                }

                if (items.Keys.Count == globals.fileKeyLength)
                {
                    globals.statList = items;
                    return true;
                }                   
            }
            catch (Exception ex)
            {
                Console.WriteLine("unable to load file. exception: " + ex);
            }
            return false;
        }
        public static void load()
        {
            if (fileCheck && loadFile())
            {
                Console.WriteLine("player file successfully loaded!");
                globals.statFileFound = true;
                //mainMenu.initMainMenu(true);
            }
            else if (!fileCheck)
            {
                Console.WriteLine("player file not found.");
                globals.statFileFound = false;
            }
            else
            {
                Console.WriteLine("unable to load player file. press 1 to retry, or press 2 to exit.");
                switch(Convert.ToInt32(Console.ReadKey().KeyChar.ToString()))
                {
                    case 1:
                        Console.Clear();
                        Thread.Sleep(500);
                        load();
                        break;
                    case 2:
                        Environment.Exit(-1);
                        break;
                }
            }
        }
    }
}
