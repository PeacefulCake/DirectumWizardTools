using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class LoadFromFolder : BaseCommand
    {
        public LoadFromFolder() : base()
        {
            name = "load";
            commandFormat = "";
            help = "Загрузить МД из папки";
            helpDetails = "";
        }

        public override void Execute(string[] commandLine)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    WizardInstanceManager.LoadFromFolder(fbd.SelectedPath);
                    Console.WriteLine("МД успешно импортирован из папки: " + fbd.SelectedPath);
                }
                else Console.WriteLine("Папка для импорта не выбрана");
            }
        }
    }
}
