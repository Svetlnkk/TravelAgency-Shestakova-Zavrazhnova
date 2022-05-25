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
using System.Configuration;
using Unity;
using TravelAgencyBusinessLogic.MailWorker;
using TravelAgencyContracts.BindingModels;

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
            var mailSender = new MailKitWorker();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                PopHost = ConfigurationManager.AppSettings["PopHost"],
                PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"])
            });
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
        private void ReportGuidesbyTripsClick(object sender, RoutedEventArgs e)
        {
            ReportGuidesByTripsWindow reportGuidesbyTripsWindow = App.Container.Resolve<ReportGuidesByTripsWindow>();
            reportGuidesbyTripsWindow.ShowDialog();
        }
    }
}
