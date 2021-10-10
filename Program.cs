using System;
using System.IO;
using System.Threading;

namespace textTycoonWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            int selected;
            splashScreen.loadSplashScreen(iterations: 1);
            loadProcess.load();

            Thread.Sleep(1000);
            Console.Clear();

            selected = mainMenu.initMainMenu(globals.statFileFound);

            gameMenu.loadGameMenu(selected);

        }
    }
}
