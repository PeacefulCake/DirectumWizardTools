using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using FAA.WizardEncoding;
using WizardTools.Types;
using WizardTools.Utils;

namespace WizardTools
{
    class Program
    {
        static void Main(string[] args)
        {
            // Нужны эксперименты с разными символами: решетка, квадратные скобки, Title с более чем 64 символами.
            // Всетаки отступы не делаются именно когда только строка
            //string str = File.ReadAllText(@"D:\DevProjects\WorkFriendly\WizardTools\WizardTools\WizardXML\Wizard_structured.xml", Encoding.UTF8);
            //XElement contacts = XElement.Parse(str);
            string wizardStrangeText = @"#1050#1086#1086#1088#1076#1080#1085#1072#1090#1072' X'";
            string wizardStrangeText2 = @"                '  // '#1047#1072#1074#1077#1088#1096#1080#1090#1100' '#1074#1099#1095#1080#1089#1083#1077#1085#1080#1103' '#1085#1072' '#1096#1072#1075#1077', '#1076#1072#1085#1085#1072#1103' '#1092#1091#1085#1082#1094#1080#1103' '#1074#1089#1077#1075#1076#1072' '#1089#1090#1072#1074#1080#1090#1089 +
                #1103' '#1074' '#1082#1086#1085#1094#1077' '#1089#1086#1073#1099#1090#1080#1103#13#10'  SMEV3ProcessStepCompletion(Wizard; wfatPrev' +
                'ious)'";
            string wizardStrangeText3 = @"            '  Params = Wizard.Params'#13#10'  SMEVProcessMultiStepBegin(Wizard; ;'#13 +
            #10'                            Params.ValueByName('#39'MLCadNumberCoun' +
            't'#39');'#13#10'                            Params.ValueByName('#39'MLCadNumbe' +
            'rPage'#39');'#13#10'                            Params.ValueByName('#39'_AddLa' +
            'ndCadNumbers'#39');                          '#13#10'                     ' +
            '       Params.ValueByName('#39'MLCadNumberData'#39');'#13#10'                 ' +
            '           ArrayOf(Params.ValueByName('#39'CadNumber1'#39');'#13#10'          ' +
            '                          Params.ValueByName('#39'CadNumber2'#39');'#13#10'   ' +
            '                                 Params.ValueByName('#39'CadNumber3'#39 +
            ');'#13#10'                                    Params.ValueByName('#39'CadN' +
            'umber4'#39');'#13#10'                                    Params.ValueByNam' +
            'e('#39'CadNumber5'#39'));'#13#10'                            TRUE)'";

            string wizardStrangeText4 = @"        #1050#1072#1076#1072#1089#1090#1088#1086#1074#1099#1077' '#1085#1086#1084#1077#1088#1072' '#1080#1085#1099#1093' '#1086#1073#1098#1077#1082#1090#1086#1074' '#1085#1077#1076#1074#1080#1078#1080#1084#1086#1089#1090#1080' ('#1079#1077#1084#1077#1083#1100#1085#1099#1093' '#1091#1095#1072#1089#1090#1082#1086 +
        #1074'), '#1074' '#1087#1088#1077#1076#1077#1083#1072#1093' '#1082#1086#1090#1086#1088#1099#1093' '#1088#1072#1089#1087#1086#1083#1086#1078#1077#1085' '#1086#1073#1098#1077#1082#1090' '#1085#1077#1076#1074#1080#1078#1080#1084#1086#1089#1090#1080";

            /*string curString = wizardStrangeText4;
            Console.WriteLine(curString);
            Console.WriteLine();
            string srcString = WizardUTFEncoder.DecodeText(curString);
            Console.WriteLine(srcString);
            Console.WriteLine();
            Console.WriteLine(WizardUTFEncoder.EncodeText(srcString, "         "));*/
            /*TSBWizardEvent ev = new TSBWizardEvent();

            TSBWizard w = new TSBWizard()
            {
                Code = new WizardString(),
                ID = 12345,
                Title = new WizardString(),
                Events = new TSBWizardEvent[] { ev }
            };

            ev.EventType = TWizardEventType.wetWizardBeforeSelection;
            ev.ISBLText.LoadFromStringList(StringListUtils.GetListFromText(wizardStrangeText2));

            w.Code.DecodedValue = "WizardCode";
            // w.Title.DecodedValue = "ТестовыйМастер_bak 11.02.03";
            w.Title.LoadFromStringList(StringListUtils.GetListFromText(wizardStrangeText));

            Console.WriteLine(w.ToStructuredString());

            TWizardEventType a = TWizardEventType.wetActionExecute;
            string aa = a.ToString();
            Console.WriteLine(aa);*/

            string someObj = @"    object TSBHer
      Value = 123
    end
    object TSBHer
      Value = 123
    end
    object TSBHer
      Value = 123
    end>";

            /*var linedObj = StringListUtils.GetListFromText(someObj);
            var obj1 = StringListUtils.PickObject(linedObj, 0);
            var obj2 = StringListUtils.PickObject(linedObj, 0);
            var obj3 = StringListUtils.PickObject(linedObj, 0);*/

            string wholeWizard = File.ReadAllText(@"D:\DevProjects\WorkFriendly\WizardTools\WizardTools\WizardDataExamples\Wizard22.txt");

            TSBWizard Wizard = new TSBWizard();

            Wizard.LoadFromString(wholeWizard);

            Console.WriteLine(Wizard.ToStructuredString());


            Console.ReadKey();
        }
    }
}
