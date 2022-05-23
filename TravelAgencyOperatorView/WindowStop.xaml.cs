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
    /// Логика взаимодействия для WindowStop.xaml
    /// </summary>
    public partial class WindowStop : Window
    {
        private readonly IStopLogic stopLogic;
        private readonly ITourLogic tourLogic;
        public int Id { set { id = value; } }
        private int? id;
        public WindowStop(IStopLogic stopLogic, ITourLogic tourLogic)
        {
            InitializeComponent();
            this.stopLogic = stopLogic;
            this.tourLogic = tourLogic;
        }

        private void WindowStop_Loaded(object sender, RoutedEventArgs e)
        {
            var list = tourLogic.Read(null);
            ToursComboBox.ItemsSource = list;
            ToursComboBox.SelectedItem = null;            
            if (id.HasValue)
            {
                try
                {
                    var view = stopLogic.Read(new StopBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameBox.Text = view.StopName;
                        DatePickerCheckin.Text = view.CheckinDateStop.ToShortDateString();
                        DatePickerDepatureof.Text = view.DateofDepatureStop.ToShortDateString();
                        ToursComboBox.SelectedIndex = list.IndexOf(list.FirstOrDefault(rec => rec.Id == view.TourId));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (DatePickerCheckin.SelectedDate == null || DatePickerDepatureof.SelectedDate == null ||
                DatePickerDepatureof.SelectedDate <= DatePickerCheckin.SelectedDate)
            {
                MessageBox.Show("Дата заезда должна быть меньше даты выезда", "Ошибка");
                return;
            }
            if (ToursComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур");
                return;
            }
            string name = NameBox.Text;

            stopLogic.CreateOrUpdate(new StopBindingModel
            {
                Id = id,
                StopName = name,
                CheckinDateStop = DatePickerCheckin.DisplayDate,
                DateofDepatureStop = DatePickerDepatureof.DisplayDate,
                TourId = ((TourViewModel)ToursComboBox.SelectedItem).Id
            });
            this.Close();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
