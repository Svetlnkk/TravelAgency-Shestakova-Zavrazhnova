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
using System.Collections.ObjectModel;
using TravelAgencyContracts.BussinessLogicsContracts;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для TripWindow.xaml
    /// </summary>
    public partial class TripWindow : Window
    {
        private readonly ITripLogic tripLogic;
        private readonly ITourLogic tourLogic;
        public int Id { set { id = value; } }
        private int? id;
        public TripWindow(ITripLogic tripLogic, ITourLogic tourLogic)
        {
            InitializeComponent();
            this.tripLogic = tripLogic;
            this.tourLogic = tourLogic;
        }
        private void TripWindowLoad(object sender, RoutedEventArgs e)
        {
            var list = tourLogic.Read(null);
            ReadyListBox.ItemsSource = list;
            ReadyListBox.SelectedItem = null;
            SelectedListBox.SelectedItem = null;
            if (id.HasValue)
            {
                try
                {
                    var view = tripLogic.Read(new TripBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameBox.Text = view.Name.ToString();
                        DatePicker.SelectedDate = view.Date;
                        var selectedList = tourLogic.Read(null).Where(rec => view.TripTours.ContainsKey(rec.Id));
                        if (view.TripTours.Count != 0)
                        {
                            CountBox.Text = view.TripTours.First().Value.ToString();
                        }
                        foreach(TourViewModel i in ReadyListBox.Items)
                        {
                            if (selectedList.FirstOrDefault(rec => rec.Id == i.Id) != null)
                            {
                                SelectedListBox.Items.Add(i);
                            }
                        }
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
            if (CountBox.Text == "" || !int.TryParse(CountBox.Text, out int numberCount))
            {
                MessageBox.Show("Введите количество в виде числа");
                return;
            }
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }
            if (SelectedListBox.Items.Count == 0)
            {
                MessageBox.Show("Выберите туры");
                return;
            }
            string name = NameBox.Text;
            int count = Convert.ToInt32(CountBox.Text);
            DateTime date = (DateTime)DatePicker.SelectedDate;
            Dictionary<int, int> tripTours = new Dictionary<int, int>();
            foreach(TourViewModel i in SelectedListBox.Items)
            {
                tripTours.Add(i.Id, count);
            }
            tripLogic.CreateOrUpdate(new TripBindingModel
            {
                Id = id,
                Name = name,
                Date = date,
                TripTours = tripTours
            });
            this.Close();
        }
        private void ReadyListBoxChange(object sender, SelectionChangedEventArgs args)
        {
            if (ReadyListBox.SelectedItem != null)
            {
                var changeItem = ReadyListBox.SelectedItem;
                if (!SelectedListBox.Items.Contains(changeItem))
                {
                    SelectedListBox.Items.Add(changeItem);
                }
                ReadyListBox.SelectedItem = null;
            }
        }
        private void SelectedListBoxChange(object sender, SelectionChangedEventArgs args)
        {
            if (SelectedListBox.SelectedItem != null)
            {
                int removeIndex = SelectedListBox.SelectedIndex;
                SelectedListBox.SelectedItem = null;
                SelectedListBox.Items.RemoveAt(removeIndex);
            }
        }
    }
}
