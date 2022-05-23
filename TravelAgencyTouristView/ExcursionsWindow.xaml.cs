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
    /// Логика взаимодействия для ExcursionsWindow.xaml
    /// </summary>
    public partial class ExcursionsWindow : Window
    {
        private readonly IExcursionLogic excursionLogic;
        public ExcursionsWindow(IExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.excursionLogic = excursionLogic;

        }
        private void LoadData()
        {
            var list = excursionLogic.Read(null);
            if (list != null)
            {
                ExcursionsData.ItemsSource = list;
                ExcursionsData.Columns[0].Visibility = Visibility.Hidden;
                ExcursionsData.Columns[5].Visibility = Visibility.Hidden;
                ExcursionsData.Columns[1].Header = "Название";
                ExcursionsData.Columns[2].Header = "Тип";
                ExcursionsData.Columns[3].Header = "Продолжительность";
                ExcursionsData.Columns[4].Header = "Стоимость";
                ExcursionsData.SelectedItem = null;
            }
        }
        private void ExcursionsWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CreateExcursionClick(object sender, RoutedEventArgs e)
        {
            ExcursionWindow excursionWindow = App.Container.Resolve<ExcursionWindow>();
            excursionWindow.ShowDialog();
            LoadData();
        }
        private void DeleteExcursionClick(object sender, RoutedEventArgs e)
        {
            if (ExcursionsData.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            int selecctedExcursionId = ((ExcursionViewModel)ExcursionsData.SelectedItem).Id;
            excursionLogic.Delete(new ExcursionBindingModel { Id = selecctedExcursionId});
            LoadData();
        }
        private void UpdateExcursionClick(object sender, RoutedEventArgs e)
        {
            if (ExcursionsData.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            int selecctedExcursionId = ((ExcursionViewModel)ExcursionsData.SelectedItem).Id;
            ExcursionWindow excursionWindow = App.Container.Resolve<ExcursionWindow>();
            excursionWindow.Id = selecctedExcursionId;
            excursionWindow.ShowDialog();
            LoadData();
        }
    }
}
