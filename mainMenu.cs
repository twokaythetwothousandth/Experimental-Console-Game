using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace textTycoonWorker
{
    static class mainMenu
    {
        private static string[] formatIfSelectedArr = new string[3] {"     ", "     ", "     "};
        private static int selected;
        public static bool ifSelected = false;
        private static void displayMainMenu()
        {
            Console.WriteLine("\n|                              |");
            Console.WriteLine("|   TWOKAY'S TEST TEXT GAME!   |");
            Console.WriteLine("|                              |");
               
            Console.WriteLine($"\n{formatIfSelectedArr[0]}1: NEW GAME");
            Console.WriteLine($"\n{formatIfSelectedArr[1]}2: CONTINUE FROM SAVE FILE");
            Console.WriteLine($"\n{formatIfSelectedArr[2]}3: QUIT EXEC\n");
        }
        public static int initMainMenu(bool ifLoaded)
        {
            displayMainMenu();

            while (!ifSelected)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                ConsoleKey keyPH = key.Key;
                switch (keyPH.ToString())
                {
                    case "D1":
                    case "D2":
                    case "D3":
                        selected = Convert.ToInt32(Convert.ToInt32(key.KeyChar.ToString()));

                        formatIfSelectedArr = new string[3] {"     ", "     ", "     "};
                        formatIfSelectedArr[selected - 1] = " [X] ";

                        Console.Clear();
                        displayMainMenu();
                        break;
                    case "Enter":
                        if (selected >= 0)
                        {
                            if (selected == 2 && globals.statFileFound)
                            {
                                ifSelected = true;
                                //transitionLoad.loadAnim(selected);
                            }
                            else if (selected == 2 && !globals.statFileFound)
                            {
                                Console.WriteLine("\nplr file not loaded!");

                                Thread.Sleep(1000);

                                Console.Clear();
                                displayMainMenu();
                            }
                            else if (selected != 2)
                            {
                                if (selected == 1 && globals.statFileFound)
                                {
                                    Console.WriteLine("it seems that there is a player file already present. are you sure you want to continue? Y = 1/N = 0");

                                    string result = Console.ReadKey().Key.ToString();

                                    switch (result)
                                    {
                                        case "D1":
                                            Console.Clear();

                                            const string confirm = "I wish to proceed.";
                                            Console.WriteLine("are you ABSOLUTELY sure? THIS WILL RESET YOUR DATA.");
                                            Console.WriteLine($"if sure, type the phrase \"{confirm}\"");

                                            switch(Console.ReadLine())
                                            {
                                                case confirm:
                                                    ifSelected = true;
                                                    break;
                                                default:
                                                    Console.WriteLine("check failed! aborting...");
                                                    Thread.Sleep(1000);

                                                    Console.Clear();
                                                    displayMainMenu();
                                                    break;
                                            }
                                            break;
                                        case "D2":
                                            Console.Clear();
                                            displayMainMenu();
                                            break;
                                        default:
                                            Console.WriteLine("check failed! aborting...");
                                            Thread.Sleep(1000);

                                            Console.Clear();
                                            displayMainMenu();
                                            break;
                                    }
                                }
                                else
                                    ifSelected = true;
                                //transitionLoad.loadAnim(selected);
                            }
                            
                        }
                        break;
                    default:
                        Console.Clear();
                        displayMainMenu();
                        break;
                }
            }
            return selected;
        }
    }
}
