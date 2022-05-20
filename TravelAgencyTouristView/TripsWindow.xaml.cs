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
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BussinessLogicsContracts;
using Unity;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для TripsWindow.xaml
    /// </summary>
    public partial class TripsWindow : Window
    {
        private readonly ITripLogic tripLogic;
        public TripsWindow(ITripLogic tripLogic)
        {
            InitializeComponent();
            this.tripLogic = tripLogic;
        }
        private void LoadData()
        {
            var list = tripLogic.Read(null);
            if (list != null)
            {
                TripsData.ItemsSource = list;
                // Console.WriteLine(TripsData.Columns);
                TripsData.Columns[0].Visibility = Visibility.Hidden;
                TripsData.Columns[3].Visibility = Visibility.Hidden;
                TripsData.Columns[4].Visibility = Visibility.Hidden;
                TripsData.Columns[5].Visibility = Visibility.Hidden;
                // TripsData.Columns[6].Visibility = Visibility.Hidden;
                TripsData.Columns[1].Header = "Название";
                TripsData.Columns[2].Header = "Дата";
                TripsData.SelectedItem = null;
            }
        }
        private void TripsWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CreateTripClick(object sender, RoutedEventArgs e)
        {
            TripWindow tripWindow = App.Container.Resolve<TripWindow>();
            tripWindow.ShowDialog();
            LoadData();
        }
        private void DeleteTripClick(object sender, RoutedEventArgs e)
        {
            if (TripsData.SelectedItem == null)
            {
                MessageBox.Show("Выберите путешествие");
                return;
            }
            int selecctedTripId = ((TripViewModel)TripsData.SelectedItem).Id;
            tripLogic.Delete(new TripBindingModel { Id = selecctedTripId });
            LoadData();
        }
        private void UpdateTripClick(object sender, RoutedEventArgs e)
        {
            if (TripsData.SelectedItem == null)
            {
                MessageBox.Show("Выберите путешествие");
                return;
            }
            int selecctedTripId = ((TripViewModel)TripsData.SelectedItem).Id;
            TripWindow tripWindow = App.Container.Resolve<TripWindow>();
            tripWindow.Id = selecctedTripId;
            tripWindow.ShowDialog();
            LoadData();
        }
        private void BindingClick(object sender, RoutedEventArgs e)
        {
            BindExcursionTripsWindow bindOrderTripsWindow = App.Container.Resolve<BindExcursionTripsWindow>();
            bindOrderTripsWindow.ShowDialog();
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
