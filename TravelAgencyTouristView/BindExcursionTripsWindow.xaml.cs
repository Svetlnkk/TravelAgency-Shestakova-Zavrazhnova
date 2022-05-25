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
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BussinessLogicsContracts;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для BindExcursionTripsWindow.xaml
    /// </summary>
    public partial class BindExcursionTripsWindow : Window
    {
        private readonly ITripLogic tripLogic;
        private readonly IExcursionLogic excursionLogic;
        public BindExcursionTripsWindow(ITripLogic tripLogic, IExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.tripLogic = tripLogic;
            this.excursionLogic = excursionLogic;
        }
        private void LoadData()
        {
            var listTrip = tripLogic.Read(new TripBindingModel { TouristLogin = AuthorizationWindow.AutorizedTourist });
            if (listTrip != null)
            {
                TripsListBox.ItemsSource = listTrip;
                TripsListBox.SelectedItem = null;
            }
            var listExcursion = excursionLogic.Read(new ExcursionBindingModel { TouristLogin = AuthorizationWindow.AutorizedTourist });
            if (listTrip != null)
            {
                ExcursionsListBox.ItemsSource = listExcursion;
                ExcursionsListBox.SelectedItem = null;
            }
        }
        private void BindExcursionTripsWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BindClick(object sender, RoutedEventArgs e)
        {
            if (ExcursionsListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите экскурсию");
                return;
            }
            if (TripsListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите путешествия");
                return;
            }
            if (CountBox.Text == "" || !int.TryParse(CountBox.Text, out int number))
            {
                MessageBox.Show("Введите количество в виде числа");
                return;
            }
            int excursionId = ((ExcursionViewModel)ExcursionsListBox.SelectedItem).Id;
            foreach(TripViewModel i in TripsListBox.SelectedItems)
            {
                tripLogic.AddExcursion(new AddTripExcursionBindingModel
                {
                    TripId = i.Id,
                    ExcursionId = excursionId,
                    ExcursionCount = Convert.ToInt32(CountBox.Text),
                    TouristLogin = AuthorizationWindow.AutorizedTourist
                });
            }
            MessageBox.Show("Привязка создана");
        }
    }
}
