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

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для PlaceWindow.xaml
    /// </summary>
    public partial class PlaceWindow : Window
    {
        private readonly IPlaceLogic placeLogic;
        private readonly IExcursionLogic excursionLogic;
        public int Id { set { id = value; } }
        private int? id;
        public PlaceWindow(IPlaceLogic placeLogic, IExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.placeLogic = placeLogic;
            this.excursionLogic = excursionLogic;
        }
        private void PlaceWindowLoad(object sender, RoutedEventArgs e)
        {
            var list = excursionLogic.Read(null);
            ExcursionsComboBox.ItemsSource = list;
            ExcursionsComboBox.SelectedItem = null;
            //ExcursionsComboBox.DisplayMemberPath = "Calorie";
            if (id.HasValue)
            {
                try
                {
                    var view = placeLogic.Read(new PlaceBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameBox.Text = view.Name;
                        DatePicker.SelectedDate = view.DateOfVisit;
                        ExcursionsComboBox.SelectedIndex = list.IndexOf(list.FirstOrDefault(rec => rec.Id == view.PlaceExcursion));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (ExcursionsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите экскурсию");
                return;
            }
            string name = NameBox.Text;
            DateTime date = (DateTime)DatePicker.SelectedDate;
            placeLogic.CreateOrUpdate(new PlaceBindingModel { Id = id, Name = name, 
                DateOfVisit = date, PlaceExcursion = ((ExcursionViewModel)ExcursionsComboBox.SelectedItem).Id});
            this.Close();
        }
    }
}
