using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class LoadFromBuffer : BaseCommand
    {
        public LoadFromBuffer() : base()
        {
            name = "paste";
            commandFormat = "";
            help = "Загрузить МД из буфера";
            helpDetails = "";
        }

        public override void Execute(string[] commandLine)
        {
            if (Clipboard.ContainsText())
            {
                if (WizardInstanceManager.LoadFromString(Clipboard.GetText()))
                {
                    Console.WriteLine("МД успешно загружен из буфера.");
                }
                else
                    Console.WriteLine("Содержимое буфера ну совсем не похоже на МД =(");

            }
            else Console.WriteLine("Содержимое буфера или пусто или не является текстом");
        }
    }
}
