using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FAA.WizardEditor
{
    /// <summary>
    /// Interaction logic for StepCopyDialog.xaml
    /// </summary>
    public partial class StepCopyDialog : Window
    {
        public List<ParamNamePair> ParamPairs;
        public class ParamNamePair
        {
            public string OldName { get; set; }
            public string NewName { get; set; }
        }

        public StepCopyDialog()
        {
            ParamPairs = new List<ParamNamePair>();
            InitializeComponent();
            
            /*ParamNamePair p = new ParamNamePair();
            p.OldName = "OldParamName";
            p.NewName = "NewParamName";
            ParamPairs.Add(p);

            p = new ParamNamePair();
            p.OldName = "SecondParamName";
            p.NewName = "SecondParamName";
            ParamPairs.Add(p);

            p = new ParamNamePair();
            p.OldName = "ThirdParamName";
            p.NewName = "ThirdParamName";
            ParamPairs.Add(p);*/

            Loaded += Main_Loaded;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            //DataGrid_ParamNames.AutoGenerateColumns = true;
            DataGrid_ParamNames.ItemsSource = ParamPairs;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ParamPairs.Add(new ParamNamePair() { OldName = "AddedName", NewName = "NewAddedName" });
            // TODO : Имея список всех параметров можно сверить на дублирование
            
        }
    }
}
