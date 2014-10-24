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
    /// Interaction logic for SharpItemInfo.xaml
    /// </summary>
    public partial class SharpItemInfo : UserControl
    {
        List<IItem> m_item_list = new List<IItem>();

        public SharpItemInfo()
        {
            InitializeComponent();

            this.ItemDataGrid.ItemsSource = m_item_list;
        }

        public List<IItem> ItemList
        {
            get { return m_item_list; }
            set { m_item_list = value; }
        }

        public void AddItem(IItem item)
        {
            m_item_list.Add(item);
            this.ItemDataGrid.Items.Refresh();
        }
    }
}
