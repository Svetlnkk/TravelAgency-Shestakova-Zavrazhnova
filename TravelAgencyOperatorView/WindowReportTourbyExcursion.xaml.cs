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

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowReportTourbyExcursion.xaml
    /// </summary>
    public partial class WindowReportTourbyExcursion : Window
    {
        private readonly ITourStorage tourStorage;
        private readonly IReportOperatorLogic reportOperatorLogic;
        public WindowReportTourbyExcursion(ITourStorage tourStorage, IReportOperatorLogic reportOperatorLogic)
        {
            InitializeComponent();
            this.tourStorage = tourStorage;
            this.reportOperatorLogic = reportOperatorLogic;
        }
        private void LoadData()
        {
            var list = tourStorage.GetFullList();
            if (list != null)
            {
                ToursListBox.ItemsSource = list;
                ToursListBox.SelectedItem = null;
            }
        }

        private void WindowReportTourbyExcursion_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void WordClick(object sender, RoutedEventArgs e)
        {
            if (ToursListBox.SelectedItem != null)
            {
                var list = ToursListBox.SelectedItems.Cast<TourViewModel>().ToList();
                var dialog = new SaveFileDialog();
                dialog.Filter = "docx|*.docx";
                if (dialog.ShowDialog() == true)
                {
                    reportOperatorLogic.saveExcursionsToWord(new ReportOperatorBindingModel() { FileName = dialog.FileName, tours = list });
                }
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                MessageBox.Show("Выберите путешествия", "Ошибка");
            }
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            if (ToursListBox.SelectedItem != null)
            {
                var list = ToursListBox.SelectedItems.Cast<TourViewModel>().ToList();
                var dialog = new SaveFileDialog();
                dialog.Filter = "xlsx|*.xlsx";
                if (dialog.ShowDialog() == true)
                {
                    reportOperatorLogic.saveExcursionsToExcel(new ReportOperatorBindingModel() { FileName = dialog.FileName, tours = list });
                }
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                MessageBox.Show("Выберите путешествия", "Ошибка");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
