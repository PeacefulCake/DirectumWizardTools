using FAA.WizardConsole;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace FAA.WizardEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Menu_OpenFromText_Click(object sender, RoutedEventArgs e)
        {
            TextEdit te = new TextEdit();
            te.OnOkButton = (string text) =>
            {
                WizardInstanceManager.LoadFromString(text);
            };
            te.ShowDialog();
        }

        private void Menu_SaveAsText_Click(object sender, RoutedEventArgs e)
        {
            if (!WizardInstanceManager.Loaded)
            {
                return;
            }
            TextEdit te = new TextEdit();
            te.TextField.Text = WizardInstanceManager.GetWizard.ExportToString();
            te.ShowDialog();
        }

        private void Menu_SaveAsFolder_Click(object sender, RoutedEventArgs e)
        {
            if (!WizardInstanceManager.Loaded)
            {
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    WizardInstanceManager.SaveToFolder(fbd.SelectedPath);
                    System.Windows.MessageBox.Show("Я сделяль!");
                }
            }
        }

        private void Menu_OpenFromFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    WizardInstanceManager.LoadFromFolder(fbd.SelectedPath);
                    System.Windows.MessageBox.Show("Я сделяль!");
                }
            }
        }
    }
}
