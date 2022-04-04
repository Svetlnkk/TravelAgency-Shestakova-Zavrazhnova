using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
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

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowGuides.xaml
    /// </summary>
    public partial class WindowGuides : Window
    {
        GuideLogic guideLogic = new GuideLogic(new GuideStorage());
        public WindowGuides()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            var list = guideLogic.Read(null);
            if (list != null)
            {
                GuidesData.ItemsSource = list;
                GuidesData.Columns[0].Visibility = Visibility.Hidden;
                GuidesData.Columns[4].Visibility = Visibility.Hidden;
                GuidesData.Columns[5].Visibility = Visibility.Hidden;
                GuidesData.Columns[6].Visibility = Visibility.Hidden;
                GuidesData.Columns[1].Header = "ФИО";
                GuidesData.Columns[2].Header = "Зарплата";
                GuidesData.Columns[3].Header = "Дата";
                GuidesData.SelectedItem = null;
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            WindowGuide windowGuide = new WindowGuide();
            windowGuide.ShowDialog();
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (GuidesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите гида");
                return;
            }
            int selecctedGuideId = ((GuideViewModel)GuidesData.SelectedItem).Id;
            guideLogic.Delete(new GuideBindingModel { Id = selecctedGuideId });
            LoadData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BindingExcursion_Click(object sender, RoutedEventArgs e)
        {
            WindowBindingExcursions windowBindingExcursions = new WindowBindingExcursions();
            windowBindingExcursions.ShowDialog();
        }

        private void WindowGuides_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (GuidesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите гид");
                return;
            }
            int selecctedGuideId = ((GuideViewModel)GuidesData.SelectedItem).Id;
            WindowGuide windowGuide = new WindowGuide();
            windowGuide.Id = selecctedGuideId;
            windowGuide.ShowDialog();
            LoadData();
        }
    }
}
