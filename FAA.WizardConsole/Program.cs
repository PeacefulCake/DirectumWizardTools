﻿using FAA.WizardConsole.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в WizardToolsConsole!");
            Console.WriteLine("Для вывода справки по командам - введи help");
            string userLine;
            do
            {
                Console.Write(@"WT>");
                userLine = Console.ReadLine();
                CommandsProcessor.ExecuteLine(userLine);

            } while (!CommandsProcessor.exitFlag);
            Console.WriteLine("Выход из приложения. Всего доброго!");

            //Console.WriteLine(WizardInstanceManager.GetWizard.ExportToString());

            //Console.ReadKey();
        }
    }
}
