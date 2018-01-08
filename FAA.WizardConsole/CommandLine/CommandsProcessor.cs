using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine
{
    public static class CommandsProcessor
    {
        public static bool exitFlag;

        private static char[] splitArray;

        static CommandsProcessor()
        {
            commandList = new List<BaseCommand>();
            exitFlag = false;
            splitArray = new char[] { ' ' };

            Type baseCommandtype = typeof(BaseCommand);
            IEnumerable<Type> list = Assembly.GetAssembly(baseCommandtype).GetTypes().Where(type => type.IsSubclassOf(baseCommandtype));

            foreach (Type itm in list)
            {
                Activator.CreateInstance(itm);
            }
        }

        private static List<BaseCommand> commandList;
        public static List<BaseCommand> CommandList
        {
            get { return commandList; }
        }

        public static void RegisterCommand(BaseCommand command)
        {
            commandList.Add(command);
        }

        public static void ExecuteLine(string line)
        {
            var parsedLine = line.Split(splitArray, StringSplitOptions.RemoveEmptyEntries);
            if (parsedLine.Length > 0)
            {
                string commandName = parsedLine[0];
                BaseCommand command = commandList.Where(c => string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (command != null)
                {
                    command.Execute(parsedLine);
                }
                else
                {
                    Console.WriteLine(string.Format("Команда \"{0}\" не найдена", commandName));
                }
            }
            
        }
    }
}
