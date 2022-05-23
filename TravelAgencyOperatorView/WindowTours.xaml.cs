using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BussinessLogicsContracts;
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
using Unity;

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowTours.xaml
    /// </summary>
    public partial class WindowTours : Window
    {
        private readonly ITourLogic tourLogic;
        public WindowTours(ITourLogic tourLogic)
        {
            InitializeComponent();
            this.tourLogic = tourLogic;
        }
        private void LoadData()
        {
            var list = tourLogic.Read(null);
            if (list != null)
            {
                ToursData.ItemsSource = list;
                ToursData.Columns[0].Visibility = Visibility.Hidden;
                ToursData.Columns[2].Visibility = Visibility.Hidden;
                ToursData.Columns[3].Visibility = Visibility.Hidden;
                ToursData.Columns[1].Header = "Название тура";
                ToursData.SelectedItem = null;
            }
        }

        private void WindowTours_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            WindowTour windowTour = App.Container.Resolve<WindowTour>();
            windowTour.ShowDialog();
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ToursData.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур");
                return;
            }
            int selecctedTourId = ((TourViewModel)ToursData.SelectedItem).Id;
            WindowTour windowTour = App.Container.Resolve<WindowTour>();
            windowTour.Id = selecctedTourId;
            windowTour.ShowDialog();
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ToursData.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур");
                return;
            }
            int selecctedTourId = ((TourViewModel)ToursData.SelectedItem).Id;
            tourLogic.Delete(new TourBindingModel { Id = selecctedTourId });
            LoadData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
