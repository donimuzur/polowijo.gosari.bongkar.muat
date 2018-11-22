using polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS;
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
        
        public MASTER_MENU()
        {
            InitializeComponent();
            _petugasView = new Petugas_View();
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
    }
}
