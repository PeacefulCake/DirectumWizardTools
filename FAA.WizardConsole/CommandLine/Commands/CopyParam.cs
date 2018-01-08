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
            name = "copy";
            commandFormat = "<original_param> <new_param>";
            help = "Справка";
            helpDetails = "\t<command_name> - Имя команды, для отображения справки по конкретной команде";
        }

        public override void Execute(string[] commandLine)
        {
            // TODO : Проверка на загруженность мастера

            string originalParamName = commandLine[1];
            string newParamName = commandLine[2];

            WizardInstanceManager.GetWizard.Params.CopyParam(originalParamName, newParamName);
        }
    }
}
