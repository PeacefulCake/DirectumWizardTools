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
    /// Interaction logic for ItemSelector.xaml
    /// </summary>
    public partial class ItemSelector : Window
    {
        public ItemSelector()
        {
            InitializeComponent();
            Loaded += Main_Loaded;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> sl = new List<string>();
            sl.Add("val");
            sl.Add("valasd");
            sl.Add("vasdsadal");
            sl.Add("3243rval");
            //DataGrid_ParamNames.AutoGenerateColumns = true;
            ComboBox_ItemsList.ItemsSource = sl;
        }

        private void ComboBox_ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox itemsList = sender as ComboBox;
            TextBox_SelectedItem.Text = itemsList.SelectedItem as string;
        }
    }
}
