using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.Core;
using polowijo.gosari.DAL.IServices;
using polowijo.gosari.DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS
{
    /// <summary>
    /// Interaction logic for Petugas_View.xaml
    /// </summary>
    public partial class Petugas_View : UserControl
    {
        private IPekerjaServices _pekerjaServices;
        private Window _insertWindow;
        public Petugas_View()
        {
            _pekerjaServices = new PekerjaServices();
            _insertWindow = new Petugas_Insert();

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            #region --- DGV Main ---
            //SourceMain.DataSource = ListCustomer;
            //Dgv_Main.DataSource = SourceMain;
            Dgv_Home.CanUserAddRows = false;
            Dgv_Home.CanUserDeleteRows = false;
            Dgv_Home.IsReadOnly = true;
            Dgv_Home.AutoGenerateColumns = false;

            DataGridTextColumn NamaPetugas = new DataGridTextColumn();
            DataGridTextColumn Status = new DataGridTextColumn();
            DataGridTextColumn StatusPerkawinan = new DataGridTextColumn();
            DataGridTextColumn Handphone = new DataGridTextColumn();
            DataGridTextColumn LastName = new DataGridTextColumn();
            DataGridTextColumn FirstName = new DataGridTextColumn();
            DataGridTextColumn Id = new DataGridTextColumn();

            Binding StatusBinding = new Binding("STATUS");
            StatusBinding.Converter = new EnumDescriptionConverter();
       
            NamaPetugas.Binding = new Binding("NAMA_PETUGAS");
            Status.Binding = StatusBinding;
            StatusPerkawinan.Binding = new Binding("STATUS_PERKAWINAN");
            Handphone.Binding = new Binding("HANDPHONE");
            FirstName.Binding = new Binding("FIRST_NAME");
            LastName.Binding = new Binding("LAST_NAME");
            Id.Binding = new Binding("ID");

            NamaPetugas.Header = "Nama Petugas";
            Status.Header = "Status";
            StatusPerkawinan.Header = "Status Perkawinan";
            Handphone.Header = "Handphone";
            FirstName.Header = "First Name";
            LastName.Header = "Last Name";
            Id.Header = "ID";

            Dgv_Home.Columns.Add(NamaPetugas);
            Dgv_Home.Columns.Add(FirstName);
            Dgv_Home.Columns.Add(LastName);
            Dgv_Home.Columns.Add(Handphone);
            Dgv_Home.Columns.Add(StatusPerkawinan);
            Dgv_Home.Columns.Add(Status);
            Dgv_Home.Columns.Add(Id);

            Id.Visibility = Visibility.Hidden;
            StatusPerkawinan.Visibility = Visibility.Hidden;
            #endregion

            PopulateData();
        }
        private void PopulateData()
        {
            var Data = AutoMapper.Mapper.Map<List<MasterPetugasDto>>(_pekerjaServices.GetAll());
            Dgv_Home.ItemsSource = Data;
        }
        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _insertWindow = new Petugas_Insert();
            var result = _insertWindow.ShowDialog();
            if (result == true)
            {
                PopulateData();
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var _itemSourceList = new CollectionViewSource() { Source = Dgv_Home.ItemsSource };
            var Filter = Filter_NamaPetugas.Text;

            // ICollectionView the View/UI part 
            ICollectionView Itemlist = _itemSourceList.View;

            // your Filter
            var yourCostumFilter = new Predicate<object>(item => ((MasterPetugasDto)item).NAMA_PETUGAS.Contains(Filter));

            //now we add our Filter
            Itemlist.Filter = yourCostumFilter;
            Dgv_Home.ItemsSource = Itemlist;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            PopulateData();
        }
    }
}
