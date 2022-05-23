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

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowBindingExcursions.xaml
    /// </summary>
    public partial class WindowBindingExcursions : Window
    {
        private readonly IGuideLogic guideLogic;
        private readonly IExcursionLogic excursionLogic;
        public WindowBindingExcursions(IGuideLogic guideLogic, IExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.guideLogic = guideLogic;
            this.excursionLogic = excursionLogic;
        }
        private void LoadData()
        {
            try
            {
                var listGuide= guideLogic.Read(null);
                if (listGuide != null)
                {
                    GuideComboBox.ItemsSource = listGuide;
                    ExcursionListBox.SelectedItem =null;
                }
                var listExcursion = excursionLogic.Read(null);
                if (listGuide != null)
                {
                    ExcursionListBox.ItemsSource = listExcursion;
                    ExcursionListBox.SelectedItem = null;
                }
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
            if (CountBox.Text == "" || !int.TryParse(CountBox.Text, out int number))
            {
                MessageBox.Show("Введите количество в виде числа");
                return;
            }
            int excursionId = ((ExcursionViewModel)ExcursionListBox.SelectedItem).Id;
            foreach (GuideViewModel i in GuideComboBox.Items)
            {
                guideLogic.AddExcursion(new AddGuideExcursionBindingModel
                {
                    GuideId = i.Id,
                    ExcursionId=excursionId,
                    ExcursionCount= Convert.ToInt32(CountBox.Text),
                    OperatorLogin=WindowAuthorization.AutorizedOperator
                });
            }
            MessageBox.Show("Успешно");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
