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
using FAA.WizardEncoding;

namespace TextDecoder
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

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            // Посчитать количество пробелов

            int indentCount = EncodedText.Text.IndexOfAny(new char[] { '\'', '#' });
            if (indentCount != -1)
            {
                IndentCount.Text = (indentCount / 2).ToString();
                DecodedText.Text = WizardUTFEncoder.DecodeText(EncodedText.Text);
            }
            else DecodedText.Text = "";



        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            string indents = new string(' ', Convert.ToInt32(IndentCount.Text) * 2);
            EncodedText.Text = WizardUTFEncoder.EncodeText(DecodedText.Text, indents);
        }

    }
}
