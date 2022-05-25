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
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;
using Microsoft.Win32;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для ReportGuidesByTripsWindow.xaml
    /// </summary>
    public partial class ReportGuidesByTripsWindow : Window
    {
        private readonly ITripLogic tripLogic;
        private readonly IReportLogic reportLogic;
        public ReportGuidesByTripsWindow(ITripLogic tripLogic, IReportLogic reportLogic)
        {
            InitializeComponent();
            this.tripLogic = tripLogic;
            this.reportLogic = reportLogic;
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoadData()
        {
            var list = tripLogic.Read(new TripBindingModel { TouristLogin = AuthorizationWindow.AutorizedTourist });
            if (list != null)
            {
                TripsListBox.ItemsSource = list;
                TripsListBox.SelectedItem = null;
            }
        }
        private void ReportGuidesByTripsWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void ExcelClick(object sender, RoutedEventArgs e)
        {
            if (TripsListBox.SelectedItem != null)
            {
                var list = TripsListBox.SelectedItems.Cast<TripViewModel>().ToList();
                var dialog = new SaveFileDialog();
                dialog.Filter = "xlsx|*.xlsx";
                if (dialog.ShowDialog() == true) {
                    reportLogic.saveGuidesToExcel(new ReportBindingModel() { FileName = dialog.FileName, trips = list, TouristLogin = AuthorizationWindow.AutorizedTourist });
                }
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                MessageBox.Show("Выберите путешествия", "Ошибка");
            }
        }
        private void WordClick(object sender, RoutedEventArgs e)
        {
            if (TripsListBox.SelectedItem != null)
            {
                var list = TripsListBox.SelectedItems.Cast<TripViewModel>().ToList();
                var dialog = new SaveFileDialog();
                dialog.Filter = "docx|*.docx";
                if (dialog.ShowDialog() == true)
                {
                    reportLogic.saveGuidesToWord(new ReportBindingModel() { FileName = dialog.FileName, trips = list, TouristLogin = AuthorizationWindow.AutorizedTourist });
                }
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                MessageBox.Show("Выберите путешествия", "Ошибка");
            }
        }
    }
}
