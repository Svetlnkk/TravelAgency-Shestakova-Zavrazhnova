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
    /// Логика взаимодействия для WindowBindingExcursions.xaml
    /// </summary>
    public partial class WindowBindingExcursions : Window
    {
        GuideLogic guideLogic = new GuideLogic(new GuideStorage());
        ExcursionLogic excursionLogic/* = new ExcursionLogic(new ExcursionStorage())*/;
        public WindowBindingExcursions()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                GuideComboBox.ItemsSource = guideLogic.Read(null);
                ExcursionListBox.ItemsSource = excursionLogic.Read(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WindowBindingExcursion_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Bind_Click(object sender, RoutedEventArgs e)
        {
            if (GuideComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите гида");
                return;
            }
            if (ExcursionListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите экскурсии");
                return;
            }            
            int guideId = ((GuideViewModel)GuideComboBox.SelectedItem).Id;
            foreach (ExcursionViewModel i in ExcursionListBox.SelectedItems)
            {
                //excursionLogic.AddGuide((i.Id, guideId));
            }
            MessageBox.Show("Успешно");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
