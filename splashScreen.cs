using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;  

namespace textTycoonWorker
{
    static class splashScreen
    {
        private static readonly string[] asciiSplashScreen = new string[]
        {
            "\n\n                                            88                                  ",
            "    ,d                                      88                                  ",
            "    88                                      88                                  ",
            "  MM88MMM  8b      db      d8   ,adPPYba,   88   ,d8   ,adPPYYba,  8b       d8  ",
            @"    88     `8b    d88b    d8'  a8""     ""8a  88 ,a8""    """"     `Y8  `8b     d8'  ",
            "    88      `8b  d8'`8b  d8'   8b       d8  8888[      ,adPPPPP88   `8b   d8'   ",
            @"    88,      `8bd8'  `8bd8'    ""8a,   ,a8""  88`""Yba,   88,    ,88    `8b,d8'    ",
            @"    ""Y888      YP      YP       `""YbbdP""'   88   `Y8a  `""8bbdP""Y8      Y88'     ",
            "                                                                       d8'      ",
            "                                                                      d8'       ",
            "                                    projects!                                   "
        };
        
        private static void splashScreenAnim(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                foreach (string line in asciiSplashScreen)
                {
                    foreach (char chara in line)
                    {
                        Console.Write(chara);
                        Thread.Sleep(1);
                    }
                    Console.WriteLine();
                    Thread.Sleep(2);
                }
            }
        }
        public static void loadSplashScreen(int duration = 2000, int iterations = 3)
        {
            splashScreenAnim(iterations);

            Thread.Sleep(duration);

            Console.Clear();
        }
    }
}
