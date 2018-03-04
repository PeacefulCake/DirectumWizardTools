using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class ExportToFolder : BaseCommand
    {
        public ExportToFolder() : base()
        {
            name = "save";
            commandFormat = "";
            help = "Сохранить МД в папку";
            helpDetails = "";
        }

        public override void Execute(string[] commandLine)
        {
            if (!WizardInstanceManager.Loaded)
            {
                Console.WriteLine("МД не загружен");
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    WizardInstanceManager.SaveToFolder(fbd.SelectedPath);
                    Console.WriteLine("МД успешно экспортирован в папку: " + fbd.SelectedPath);
                }
                else Console.WriteLine("Папка для экспорта не выбрана");
            }
        }
    }
}
