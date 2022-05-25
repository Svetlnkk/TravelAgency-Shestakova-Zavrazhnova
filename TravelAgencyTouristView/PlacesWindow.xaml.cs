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
using Unity;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для PlacesWindow.xaml
    /// </summary>
    public partial class PlacesWindow : Window
    {
        private readonly IPlaceLogic placeLogic;
        public PlacesWindow( IPlaceLogic placeLogic)
        {
            InitializeComponent();
            this.placeLogic = placeLogic;
        }
        private void LoadData()
        {
            var list = placeLogic.Read(new PlaceBindingModel { TouristLogin = AuthorizationWindow.AutorizedTourist });
            if (list != null)
            {
                PlacesData.ItemsSource = list;
                PlacesData.Columns[0].Visibility = Visibility.Hidden;
                PlacesData.Columns[3].Visibility = Visibility.Hidden;
                PlacesData.Columns[1].Header = "Название";
                PlacesData.Columns[2].Header = "Дата посещения";
                PlacesData.Columns[4].Header = "Id экскурсии";
                PlacesData.SelectedItem = null;
            }
        }
        private void PlacesWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CreatePlaceClick(object sender, RoutedEventArgs e)
        {
            PlaceWindow placeWindow = App.Container.Resolve<PlaceWindow>();
            placeWindow.ShowDialog();
            LoadData();
        }
        private void DeletePlaceClick(object sender, RoutedEventArgs e)
        {
            if (PlacesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите места");
                return;
            }
            int selecctedPlaceId = ((PlaceViewModel)PlacesData.SelectedItem).Id;
            placeLogic.Delete(new PlaceBindingModel { Id = selecctedPlaceId, TouristLogin = AuthorizationWindow.AutorizedTourist });
            LoadData();
        }
        private void UpdatePlaceClick(object sender, RoutedEventArgs e)
        {
            if (PlacesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите места");
                return;
            }
            int selecctedPlaceId = ((PlaceViewModel)PlacesData.SelectedItem).Id;
            PlaceWindow placeWindow = App.Container.Resolve<PlaceWindow>();
            placeWindow.Id = selecctedPlaceId;
            placeWindow.ShowDialog();
            LoadData();
        }
    }
}
