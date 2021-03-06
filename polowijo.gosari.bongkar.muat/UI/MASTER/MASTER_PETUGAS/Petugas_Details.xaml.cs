﻿using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.Core;
using polowijo.gosari.DAL.IServices;
using polowijo.gosari.DAL.Services;
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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS
{
    /// <summary>
    /// Interaction logic for Petugas_Details.xaml
    /// </summary>
    public partial class Petugas_Details : Window
    {
        private IPekerjaServices _pekerjaService;
        public Petugas_Details(int Id)
        {
            _pekerjaService = new PekerjaServices();

            var Data =_pekerjaService.GetById(Id);

            InitializeComponent();
                        
            if (Data != null)
            {
                IdPetugas.Text = Data.ID.ToString();
                NamaPetugas.Text = Data.NAMA_PETUGAS;
                Alamat.Text = Data.ALAMAT;
                FirstName.Text = Data.FIRST_NAME;
                LastName.Text = Data.LAST_NAME;
                Handphone.Text = Data.HANDPHONE;
                StatusPetugas.SelectedItem = Data.STATUS;
            }
        }
        private void CloseWin()
        {
            DialogResult = true;
            this.Close();
        }
        private void Btn_Simpan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _pekerjaService = new PekerjaServices();
                if (string.IsNullOrEmpty(NamaPetugas.Text))
                {
                    MessageBox.Show("Nama Petugas tidak boleh kosong","Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                };

                var Dto = new MasterPetugasDto();
                Dto.NAMA_PETUGAS = NamaPetugas.Text;
                Dto.HANDPHONE = Handphone.Text;
                Dto.FIRST_NAME = FirstName.Text;
                Dto.ALAMAT = Alamat.Text;
                Dto.LAST_NAME = LastName.Text;
                Dto.ID = int.Parse(IdPetugas.Text);
                Dto.STATUS = (Status)StatusPetugas.SelectedItem;

                var GetDataExisting = _pekerjaService.GetAll().Where(x => !string.IsNullOrEmpty(x.NAMA_PETUGAS) && x.NAMA_PETUGAS.ToUpper() == Dto.NAMA_PETUGAS.ToUpper()).FirstOrDefault();
                if (GetDataExisting != null && GetDataExisting.ID !=Dto.ID)
                {
                    MessageBox.Show("Nama Petugas sudah ada", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                _pekerjaService.Save(Dto);
                MessageBox.Show("Update Data Sukses", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseWin();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Update Data Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void Btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public static bool IsValid(string str)
        {
            long i;
            return long.TryParse(str, out i) && i >= 0;
        }

        private void Handphone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
    }
}
