using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class ExportToBuffer : BaseCommand
    {
        public ExportToBuffer() : base()
        {
            name = "copy";
            commandFormat = "";
            help = "Скопировать МД в буфер";
            helpDetails = "";
        }

        public override void Execute(string[] commandLine)
        {
            if (!WizardInstanceManager.Loaded)
            {
                Console.WriteLine("МД не загружен");
                return;
            }

            Clipboard.SetText(WizardInstanceManager.GetWizard.ExportToString());
            Console.WriteLine("МД успешно скопирован в текст буфера");
        }
    }
}
