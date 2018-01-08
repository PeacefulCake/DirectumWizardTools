using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class HelpCommand : BaseCommand
    {
        public HelpCommand() : base()
        {
            name = "help";
            commandFormat = "<command_name>";
            help = "Справка";
            helpDetails = "\t<command_name> - Имя команды, для отображения справки по конкретной команде";
        }

        public override void Execute(string[] commandLine)
        {
            if (commandLine.Length > 1)
            {
                string commandName = commandLine[1];
                BaseCommand command = CommandsProcessor.CommandList.Where(c => string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (command != null)
                {
                    Console.WriteLine(command.GetDetailedHelp());
                }
                else
                {
                    Console.WriteLine(string.Format("Команда \"{0}\" не найдена", commandName));
                }
            }
            else
            {
                Console.WriteLine(GetDetailedHelp());
                foreach (var item in CommandsProcessor.CommandList)
                {
                    if(item.Name != name)
                        Console.WriteLine(item.GetSimpleHelp());
                }
            }
            
        }
    }
}
