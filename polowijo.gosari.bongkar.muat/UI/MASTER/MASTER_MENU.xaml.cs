using polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_ITEM;
using polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS;
using polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_SUPPLIER;
using polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_TRANSPORT;
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

namespace polowijo.gosari.bongkar.muat.UI.MASTER
{
    /// <summary>
    /// Interaction logic for MASTER_MENU.xaml
    /// </summary>
    public partial class MASTER_MENU : UserControl
    {
        private UserControl _petugasView;
        private UserControl _supplierView;
        private UserControl _transportView;
        private UserControl _itemView;
        public MASTER_MENU()
        {
            InitializeComponent();
            _petugasView = new Petugas_View();
            _supplierView = new Supplier_View();
            _transportView = new Transport_View();
            _itemView = new Item_View();
        }
        
        private void Btn_Master_Pekerja_Click(object sender, RoutedEventArgs e)
        {
            _petugasView = new Petugas_View();

            Window parentWindow = Window.GetWindow(this);
            var Content = (Grid)parentWindow.Content;
            
            var ContentChild1 = (Grid)Content.Children[0];
            var Header = (TextBlock)ContentChild1.Children[0];
            Header.Text = "Bongkar & Muat Barang - Master Pekerja";

            var ContentChild2 = (DockPanel)Content.Children[2];
            ContentChild2.Children.Clear();
            ContentChild2.Children.Add(_petugasView);
            
        }

        private void Btn_Master_Supplier_Click(object sender, RoutedEventArgs e)
        {

            _supplierView = new Supplier_View();

            Window parentWindow = Window.GetWindow(this);
            var Content = (Grid)parentWindow.Content;

            var ContentChild1 = (Grid)Content.Children[0];
            var Header = (TextBlock)ContentChild1.Children[0];
            Header.Text = "Bongkar & Muat Barang - Master Supplier";

            var ContentChild2 = (DockPanel)Content.Children[2];
            ContentChild2.Children.Clear();
            ContentChild2.Children.Add(_supplierView);
        }

        private void Btn_Master_Item_Click(object sender, RoutedEventArgs e)
        {
            _itemView = new Item_View();

            Window parentWindow = Window.GetWindow(this);
            var Content = (Grid)parentWindow.Content;

            var ContentChild1 = (Grid)Content.Children[0];
            var Header = (TextBlock)ContentChild1.Children[0];
            Header.Text = "Bongkar & Muat Barang - Master Barang";

            var ContentChild2 = (DockPanel)Content.Children[2];
            ContentChild2.Children.Clear();
            ContentChild2.Children.Add(_itemView);
        }

        private void Btn_Master_Transport_Click(object sender, RoutedEventArgs e)
        {
            _transportView = new Transport_View();

            Window parentWindow = Window.GetWindow(this);
            var Content = (Grid)parentWindow.Content;

            var ContentChild1 = (Grid)Content.Children[0];
            var Header = (TextBlock)ContentChild1.Children[0];
            Header.Text = "Bongkar & Muat Barang - Master Transport";

            var ContentChild2 = (DockPanel)Content.Children[2];
            ContentChild2.Children.Clear();
            ContentChild2.Children.Add(_transportView);
        }
    }
}
