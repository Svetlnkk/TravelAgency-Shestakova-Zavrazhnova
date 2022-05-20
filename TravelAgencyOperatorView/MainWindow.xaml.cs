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
using Unity;

namespace TravelAgencyOperatorView
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

        private void Guides_Click(object sender, RoutedEventArgs e)
        {
            WindowGuides windowGuides = App.Container.Resolve<WindowGuides>();
            windowGuides.ShowDialog();
        }

        private void Tours_Click(object sender, RoutedEventArgs e)
        {
           WindowTours windowTours = App.Container.Resolve <WindowTours>();
           windowTours.ShowDialog();
        }

        private void Stops_Click(object sender, RoutedEventArgs e)
        {
           WindowStops windowStops = App.Container.Resolve <WindowStops>();
           windowStops.ShowDialog();
        }

        private void ReportExcursionsbyTours_Click(object sender, RoutedEventArgs e)
        {
            WindowReportTourbyExcursion windowReportTourExc = App.Container.Resolve<WindowReportTourbyExcursion>();
            windowReportTourExc.ShowDialog();
        }

        private void ReportGuides_Click(object sender, RoutedEventArgs e)
        {
            WindowReportGuides windowReportGuide = App.Container.Resolve<WindowReportGuides>();
            windowReportGuide.ShowDialog();
        }
    }
}
