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
    /// Логика взаимодействия для WindowStops.xaml
    /// </summary>
    public partial class WindowStops : Window
    {
        private readonly IStopLogic stopLogic;
        public WindowStops(IStopLogic stopLogic)
        {
            InitializeComponent();
            this.stopLogic = stopLogic;
        }
        private void LoadData()
        {
            var list = stopLogic.Read(null);
            if (list != null)
            {
                StopsData.ItemsSource = list;
                StopsData.Columns[0].Visibility = Visibility.Hidden;
                StopsData.Columns[4].Visibility = Visibility.Hidden;
                StopsData.Columns[1].Header = "Название";
                StopsData.Columns[2].Header = "Дата заезда";
                StopsData.Columns[3].Header = "Дата выезда";
                StopsData.Columns[5].Header = "Id тура";
                StopsData.SelectedItem = null;
            }
        }

        private void WindowStops_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            WindowStop windowStop = App.Container.Resolve<WindowStop>();
            windowStop.ShowDialog();
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (StopsData.SelectedItem == null)
            {
                MessageBox.Show("Выберите остановку");
                return;
            }
            int selecctedStopId = ((StopViewModel)StopsData.SelectedItem).Id;
            WindowStop windowStop = App.Container.Resolve <WindowStop>();
            windowStop.Id = selecctedStopId;
            windowStop.ShowDialog();
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StopsData.SelectedItem == null)
            {
                MessageBox.Show("Выберите остановку");
                return;
            }
            int selecctedStopId = ((StopViewModel)StopsData.SelectedItem).Id;
            stopLogic.Delete(new StopBindingModel { Id = selecctedStopId });
            LoadData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
