using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAA.WizardTools.Types;

namespace FAA.WizardTools.Editor
{
    public class WizardEditor
    {
        MessageHandler messageHandler;
        Wizard wizard;

        public WizardEditor(MessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
            wizard = new Wizard();
        }

        public List<string> StepNames
        {
            get
            {
                return wizard.Steps.StepNames;
            }
        }

        public List<string> ParamNames
        {
            get
            {
                return wizard.Params.ParamNames;
            }
        }

        // RebindParams
        /*public void CopyStep()
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

        public void CopyParam()
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
        
         private static Wizard wizard;
        public static Wizard GetWizard
        {
            get { return wizard;  }
        }

        public static bool Loaded {
            get {
                return wizard.Loaded;
            }
        }
        public static bool ChangesSaved { get; set; }
        static WizardInstanceManager()
        {
            wizard = new Wizard();
            ChangesSaved = true;
        }

        public static bool LoadFromString(string wizardData)
        {
            wizard = new Wizard();
            var data = StringUtils.GetListFromText(wizardData);
            if (data.First() != WSConstants.Objects.Wizard || data.Last() != WSConstants.Markup.End)
                return false;

            wizard.LoadFromDataList(data);
            ChangesSaved = true;
            return true;
        }

        public static bool LoadFromFolder(string folderPath)
        {
            wizard = new Wizard();
            wizard.LoadFromFolder(folderPath);
            ChangesSaved = true;
            return true;
        }

        public static bool SaveToFolder(string folderPath)
        {
            wizard.SaveToFolder(folderPath);
            ChangesSaved = true;
            return true;
        }*/

    }
}
