using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class CopyParam : BaseCommand
    {
        public CopyParam() : base ()
        {
            name = "paramcopy";
            commandFormat = "<original_param> <new_param>";
            help = "Скопировать параметр МД";
            helpDetails = "\t<original_param> - Имя параметра, который нужно скопировать\n\t<new_param> - Имя нового параметра";
        }

        public override void Execute(string[] commandLine)
        {
            if (!WizardInstanceManager.Loaded)
            {
                Console.WriteLine("МД не загружен");
                return;
            }

            if (commandLine.Length < 3)
            {
                Console.WriteLine("Неверный формат параметров команды");
                return;
            }

            string originalParamName = commandLine[1];
            string newParamName = commandLine[2];

            if (WizardInstanceManager.GetWizard.Params.CopyParam(originalParamName, newParamName))
            {
                Console.WriteLine("Параметр успешно скопирован.");
            }
            else
            {
                Console.WriteLine("Неудачное копирование. Исходный параметр не найден, или параметр с новым именем уже существует.");
            }
        }
    }
}
