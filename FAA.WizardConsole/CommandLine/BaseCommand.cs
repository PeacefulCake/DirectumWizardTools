using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine
{
    public abstract class BaseCommand
    {
        public BaseCommand()
        {
            CommandsProcessor.RegisterCommand(this);
        }

        protected string name;
        public string Name
        {
            get { return name; }
        }

        protected string commandFormat;
        public string CommandFormat
        {
            get { return commandFormat;  }
        }
        protected string help;
        public string Help
        {
            get { return help; }
        }
        protected string helpDetails;
        public string HelpDetails
        {
            get { return helpDetails; }
        }

        public string GetSimpleHelp()
        {
            return string.Format("{0}\t{1}", name, help);
        }

        public string GetDetailedHelp()
        {
            if(helpDetails != "")
                return string.Format("{0} {1} - {2}\n{3}", name, commandFormat, help, helpDetails);
            else
                return string.Format("{0} {1} - {2}", name, commandFormat, help);
        }

        public abstract void Execute(string[] commandLine);
    }
}
