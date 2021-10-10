using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

using invTim = System.Timers;

namespace textTycoonWorker
{
    static class cmds
    {
        public static readonly Dictionary<string, Action<string[]>> cmd = new Dictionary<string, Action<string[]>>();
        public static readonly Dictionary<string, string> cmdHelp = new Dictionary<string, string>();
        private static Dictionary<string, int> cooldowns = new Dictionary<string, int>();
        private static Dictionary<string, invTim.Timer> cooldownTimers = new Dictionary<string, invTim.Timer>();
        private static Dictionary<string, int> perks = new Dictionary<string, int>();
        private static Random rand = new Random();
        private static void extraArgs(string[] args, Action action)
        {
            if (args.Length > 1 && args[1].ToLower() == "help")
                Console.WriteLine(cmdHelp[args[0]]);
            else
                action();
        }
        private static void cooldownMethod(string name, int duration)
        {
            ++cooldowns[name];
            if (cooldowns[name] >= duration)
            {
                cooldowns[name] = 0;
                cooldownTimers[name].Stop();
            }
        }
        #region cmdMethods
        private static void print(string[] args)
        {
            void func()
            {
                Console.WriteLine(args[1]);
            }

            try
            {
                extraArgs(args, func);
            }
            catch
            {
                Console.WriteLine($"invalid arguments for cmd: {args[0]}");
            }         
        }
        private static void clear(string[] args)
        {
            void func()
            {
                Console.Clear();
            }

            extraArgs(args, func);
        }
        private static void listCmds(string[] args)
        {
            void func()
            {
                Console.WriteLine("displaying commands: \n");
                foreach (string key in cmd.Keys)
                    Console.WriteLine($"[{key}]: {cmdHelp[key]}");
            }

            extraArgs(args, func);
        }
        private static void beg(string[] args)
        {
            void func()
            {
                if (cooldowns["begCooldown"] <= 0)
                {
                    int chance = rand.Next(1, 100) + Convert.ToInt32(globals.statList["LuckChance"]);
                    cooldownTimers["begCooldown"].Start();
                    if (chance >= 50)
                    {
                        int profit = rand.Next(100, 1500);

                        globals.statList["Money"] = Convert.ToInt32(globals.statList["Money"]) + profit;
                        Console.WriteLine($"as you begged for money, one bypasser took pity on you & gave you ${profit}...");
                        save.saveFile();
                    }
                    else
                    {
                        Console.WriteLine("you begged for money, but no one batted an eye towards you. you earned $0...");
                    }
                }
                else
                    Console.WriteLine($"woah there, you're begging too fast! you have {20 - cooldowns["begCooldown"]} more seconds before you can beg again.");
            }

            extraArgs(args, func);
        }
        private static void exit(string[] args)
        {
            void func()
            {
                Console.WriteLine($"exiting. goodbye, {globals.statList["Username"]}...");

                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            extraArgs(args, func);
        }
        private static void stats(string[] args)
        {
            void func()
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"LV: 01");
                Console.WriteLine($"NAME: {globals.statList["Username"]}");
                Console.WriteLine($"$: {globals.statList["Money"]}");
                Console.WriteLine($"LUCK: {globals.statList["LuckChance"]}");
                Console.WriteLine($"STATUS: WANTED");
                Console.WriteLine($"ITEMS: 9999");
                Console.WriteLine($"COMPANY OWNER: TRUE");
                Console.WriteLine($"PETS: 4");
                Console.WriteLine($"PROPERTIES: 4");
                Console.WriteLine("-------------------------------");
            }

            extraArgs(args, func);
        }
        private static void hack(string[] args)
        {
            void func()
            {
                if (cooldowns["hackCooldown"] <= 0)
                {
                    string[] phrases = new string[]
                    {
                        "#include <iostream>",
                        "using namespace std;",
                        "cin << \"ssh root\";",
                        "cout << \"port 2222\";",
                        "while (true)", 
                        "int x = 90;",
                        "int main() { x++; }"
                    };
                    bool ifHacked = false;
                    Console.WriteLine("you opened up your computer, and started hacking...");
                    Thread.Sleep(1000);
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("VERITAS HACKING CONSOLE INITIATED");
                    Thread.Sleep(500);
                    Console.WriteLine("INITIALIZE ARGUMENTS FOR COMMANDS");
                    Thread.Sleep(500);
                    for (int i = 0; i < 6; i++)
                    {
                        string random = phrases[rand.Next(0, phrases.Length - 1)];
                        Console.WriteLine($"[ {random} ]");

                        Console.Write(">");
                        if (Console.ReadLine() == random)
                        {
                            Console.WriteLine($"{i + 1} / 6");

                            switch (i)
                            {
                                case 5:
                                    Console.WriteLine("APPLYING COMMANDS...");
                                    Thread.Sleep(1000);
                                    Console.WriteLine("ACCESSING...");
                                    Thread.Sleep(1000);
                                    Console.WriteLine("APPLYING PAYLOADS...");
                                    Thread.Sleep(rand.Next(1000, 5000));

                                    Console.WriteLine("SUCCESS!");
                                    ifHacked = true;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("INSUFFICIENT ARGUMENTS! ABORTING...");
                            Thread.Sleep(2000);
                        }
                    }
                    Console.WriteLine("-------------------------------");
                    switch (ifHacked)
                    {
                        case true:
                            int profit = rand.Next(5000, 50000);
                            Console.WriteLine($"you celebrated at the successful hack attack, which managed to earn you ${profit}!");

                            globals.statList["Money"] = Convert.ToInt32(globals.statList["Money"]) + profit;
                            save.saveFile();
                            break;
                        case false:
                            Console.WriteLine("the hack was unsuccessful! you earned $0...");
                            break;
                    }

                    cooldownTimers["hackCooldown"].Start();
                }
                else
                    Console.WriteLine($"you need to lay low, or else the fbi might detect you! you have {120 - cooldowns["hackCooldown"]} more seconds before you can hack again.");
            }

            extraArgs(args, func);
        }
        #endregion
        static cmds()
        {
            ///<summary>
            ///static constructor has each method referenced to an action, then has each action referenced to a specific placement in the cmd Dictionary.
            ///this is also where cmdHelp gets set up.
            ///cmdHelp usage: "{description} [usage]: {usage}"
            ///</summary>
            #region cmdInitialization
            cmd["cmds"] = listCmds;
            cmdHelp["cmds"] = $"displays a full list of commands available. [usage]: cmds";

            cmd["print"] = print;
            cmdHelp["print"] = $"writes a line of text to the console window. [usage]: print {{string}}";

            cmd["clr"] = clear;
            cmdHelp["clr"] = $"clears the console window. [usage]: clear";

            cmd["beg"] = beg;
            cmdHelp["beg"] = $"makes you simulate what it's like to be a bum. can potentially earn you money! [usage]: beg";

            cmd["stats"] = stats;
            cmdHelp["stats"] = $"displays your stats, including your net worth, and item count. [usage]: stats";

            cmd["exit"] = exit;
            cmdHelp["exit"] = $"exits out of the game. [usage]: exit";

            cmd["hack"] = hack;
            cmdHelp["hack"] = $"allows you to hack a company. LAPTOP REQUIRED! [usage]: hack";
            #endregion
            #region cooldowns
            cooldowns["begCooldown"] = 0;
            cooldownTimers["begCooldown"] = new invTim.Timer();
            cooldownTimers["begCooldown"].Interval = 1000;
            cooldownTimers["begCooldown"].Elapsed += (object sender, ElapsedEventArgs e) => cooldownMethod("begCooldown", 20);

            cooldowns["hackCooldown"] = 0;
            cooldownTimers["hackCooldown"] = new invTim.Timer();
            cooldownTimers["hackCooldown"].Interval = 1000;
            cooldownTimers["hackCooldown"].Elapsed += (object sender, ElapsedEventArgs e) => cooldownMethod("hackCooldown", 120);
            #endregion
            #region perks

            #endregion
        }
    }
}
