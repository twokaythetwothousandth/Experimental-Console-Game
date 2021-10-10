using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace textTycoonWorker
{
    static class gameMenu
    {
        private static void notifications()
        {

        }
        public static void loadGameMenu(int i)
        {
            Console.Clear();
            
            switch (i)
            {
                case 1:
                    save.newFile();
                    break;
                case 2:
                    Console.WriteLine($"welcome back, {globals.statList["Username"]}...");
                    notifications();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
            while (true)
            {
                string cmd = Console.ReadLine();
                string[] cmdSplit = cmd.Split(' ');

                 switch(cmds.cmd.ContainsKey(cmdSplit[0]))
                {
                    case true:
                        cmds.cmd[cmdSplit[0]](cmdSplit);
                        break;
                    case false:
                        Console.WriteLine("invalid command");
                        break;
                }
            }
        }
    }
}
