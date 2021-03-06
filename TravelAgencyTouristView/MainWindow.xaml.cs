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
// using TravelAgencyBusinessLogic.OfficePackage.Implements;
using TravelAgencyContracts.BussinessLogicsContracts;
using Unity;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ExcursionsClick(object sender, RoutedEventArgs e)
        {
            ExcursionsWindow excursionsWindow = App.Container.Resolve<ExcursionsWindow>();
            excursionsWindow.ShowDialog();
        }
        private void PlacesClick(object sender, RoutedEventArgs e)
        {
            PlacesWindow placesWindow = App.Container.Resolve<PlacesWindow>();
            placesWindow.ShowDialog();
        }
        private void TripsClick(object sender, RoutedEventArgs e)
        {
            TripsWindow tripsWindow = App.Container.Resolve<TripsWindow>();
            tripsWindow.ShowDialog();
        }
        private void ReportTripsClick(object sender, RoutedEventArgs e)
        {
            ReportTripsWindow reportTripsWindow = App.Container.Resolve<ReportTripsWindow>();
            reportTripsWindow.ShowDialog();
        }
        //private void ReportGuidesbyTripsClick(object sender, RoutedEventArgs e)
        //{
        //    ReportGuidesbyTripsWindow reportGuidesbyTripsWindow = App.Container.Resolve<ReportGuidesbyTripsWindow>();
        //    reportGuidesbyTripsWindow.ShowDialog();
        //}
    }
}
