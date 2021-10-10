using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace textTycoonWorker
{
    static class save
    {
        public static void newFile()
        {
            Dictionary<string, object> tempDict = new Dictionary<string, object>();

            Console.WriteLine("please enter your name (MUST NOT BE OVER 20 CHARS):");

            string name = Console.ReadLine();
            while (name.Length > 20)
            {
                Console.WriteLine("name is over 20 chars! please re-enter another name:");
                name = Console.ReadLine();
            }

            Console.WriteLine("initializing values...");

            tempDict["Username"] = name;
            tempDict["Money"] = 0;
            tempDict["LuckChance"] = 0;

            Console.WriteLine("creating file...");
            int i = 0;
            string[] dictPH = new string[tempDict.Keys.Count];
            foreach (string key in tempDict.Keys)
            {
                dictPH[i] = $"{key},{tempDict[key]}";
                i++;
            }

            File.WriteAllLines(globals.path, dictPH);

            Console.WriteLine("saving to globals...");

            globals.statList = tempDict;

            Console.WriteLine($"\ncompleted! welcome, {globals.statList["Username"]}...");
        }
        public static void saveFile()
        {
            int i = 0;
            string[] dictPH = new string[globals.statList.Keys.Count];
            foreach (string key in globals.statList.Keys)
            {
                dictPH[i] = $"{key},{globals.statList[key]}";
                i++;
            }

            try
            {
                File.WriteAllLines(globals.path, dictPH);
            }
            catch
            {
                Console.WriteLine("failed to write to player file! [make sure the program has sufficient perissions, and/or that another program doesn't have the player file opened!]");
            }
        }
    }
}
