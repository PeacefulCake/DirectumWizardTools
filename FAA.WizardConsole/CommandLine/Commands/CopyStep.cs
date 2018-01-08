using FAA.WizardTools.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class CopyStep : BaseCommand
    {
        public CopyStep() : base ()
        {
            name = "stepcopy";
            commandFormat = "<original_step> <new_step> <copy_params>";
            help = "Скопировать шаг МД";
            helpDetails = "\t<original_step> - Имя шага, который нужно скопировать\n\t<new_step> - Имя нового шага\n\t<copy_params> - Необходимость сделать копию параметров, используемых на шаге";
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

            string originalStepName = commandLine[1];
            string newStepName = commandLine[2];

            if (WizardInstanceManager.GetWizard.Steps.CopyStep(originalStepName, newStepName))
            {
                Console.WriteLine("Шаг успешно скопирован.");
            }
            else
            {
                Console.WriteLine("Неудачное копирование. Исходный шаг не найден, или шаг с новым именем уже существует.");
            }

            string copyParams; // TODO : Пошло довольно топорно, придется рефакторить
            if (commandLine.Length <= 3)
            {
                Console.Write("Сделать копию параметров? (Д/Y для копирования): ");
                copyParams = "false";
                var userResp = Console.ReadKey();
                if (userResp.KeyChar == 'Д' || userResp.KeyChar == 'д' || userResp.KeyChar == 'Y' || userResp.KeyChar == 'y')
                {
                    copyParams = "true";
                }
                Console.WriteLine();
            }
            else
            {
                copyParams = commandLine[3];
            }

            if (string.Equals(copyParams, "true", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Копирование и переименование параметров. Если нужно оставить тот же параметр - оставь поле пустым.");
                WizardStep newstep = WizardInstanceManager.GetWizard.Steps.GetStep(newStepName);
                var formElements = newstep.FormElements.GetAskableParams();

                int index = 0;
                WizardStepFormElement element;
                string tmpNewName;
                while (index < formElements.Count)
                {
                    element = formElements[index];
                    Console.Write(element.ParamName.DecodedValue + "->");
                    tmpNewName = Console.ReadLine();

                    if (tmpNewName != "")
                    {
                        if (WizardInstanceManager.GetWizard.Params.CopyParam(element.ParamName.DecodedValue, tmpNewName))
                        {
                            element.ParamName.DecodedValue = tmpNewName;
                            index++;
                        }
                        else
                        {
                            Console.WriteLine("Параметр с таким имененем уже есть (или еще какая проблема и тогда эта хрень зациклится (тогда оставь пустым)).");
                        }
                    }
                    else index++;
                }
            }
        }
    }
}
