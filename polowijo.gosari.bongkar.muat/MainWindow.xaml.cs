using polowijo.gosari.bongkar.muat.Mapper;
using polowijo.gosari.bongkar.muat.UI.HOME;
using polowijo.gosari.bongkar.muat.UI.MASTER;
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

namespace polowijo.gosari.bongkar.muat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Home_View _homeView;
        private MASTER_MENU _masterMenu;
        private CustomMapper _mapper;
        public MainWindow()
        {
            InitializeComponent();

            _mapper = new CustomMapper();

            _homeView = new Home_View();
            _masterMenu = new MASTER_MENU();
            
            Init();
        }
        private void Init()
        {
            MainView.Children.Clear();
            MainView.Children.Add(_homeView);
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
        private void Lv_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var IdxSelected = Lv_Menu.SelectedIndex;
            if (IdxSelected == 0)
            {
                if(MainView != null)
                {
                    MainView.Children.Clear();
                    MainView.Children.Add(_homeView);

                    CaptionHeader.Text = "Bongkar & Muat Barang";
                }
            }
            else if (IdxSelected == 1)
            {
                MainView.Children.Clear();
                MainView.Children.Add(_masterMenu);
            }
            else if (IdxSelected == 2)
            {
                MainView.Children.Clear();
            }
            else if (IdxSelected == 3)
            {
                MainView.Children.Clear();
            }
        }
    }
}
