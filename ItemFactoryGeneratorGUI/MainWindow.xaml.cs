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
using SharpEntity;

namespace ItemFactoryGeneratorGUI
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

        private void SharpItemInfo_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MakeItemButton_Click(object sender, RoutedEventArgs e)
        {
            int t_level = 1;
 
            try
            {
                t_level = Int32.Parse(this.ItemLevelTextBox.Text);

                if(t_level > 100)
                {
                    t_level = 100;
                }
                else if(t_level < 1)
                {
                    t_level = 1;
                }
            }
            catch(Exception)
            {
                t_level = 1;
            }

            this.ItemDisplayInfo.AddItem(ItemFactory.MakeItem((Constants.ItemType)this.TypeListBox.SelectedIndex, this.ItemNameTextBox.Text, t_level, (Constants.BeingClassType)this.BeingTypeListBox.SelectedIndex));
        }
    }
}
