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
    /// Interaction logic for TextEdit.xaml
    /// </summary>
    public partial class TextEdit : Window
    {
        public Action<string> OnOkButton = null;

        public TextEdit()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (OnOkButton != null)
            {
                OnOkButton(TextField.Text);
            }
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
