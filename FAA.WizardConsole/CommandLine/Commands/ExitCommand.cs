using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class ExitCommand : BaseCommand
    {
        public ExitCommand() : base()
        {
            name = "exit";
            commandFormat = "";
            help = "Выйти из приложения";
            helpDetails = "";
        }

        public override void Execute(string[] commandLine)
        {
            if (!WizardInstanceManager.ChangesSaved)
            {
                Console.Write("Изменения в МД не были сохранены. Выйти без сохранения изменений? (Д/Y - выйти, иначе остаться): ");
                var userResp = Console.ReadKey();
                if (userResp.KeyChar == 'Д' || userResp.KeyChar == 'д' || userResp.KeyChar == 'Y' || userResp.KeyChar == 'y')
                {
                    CommandsProcessor.exitFlag = true;
                }
                Console.WriteLine();
            } else CommandsProcessor.exitFlag = true;

        }
    }
}
