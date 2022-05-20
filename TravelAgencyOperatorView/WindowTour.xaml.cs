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
using TravelAgencyContracts.BussinessLogicsContracts;

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowTour.xaml
    /// </summary>
    public partial class WindowTour : Window
    {
        public int Id { set { id = value; } }
        ITourLogic tourLogic;        
        private int? id;
        private Dictionary<int, (string, decimal)> tourGuides;
        public WindowTour(ITourLogic tourLogic)
        {
            InitializeComponent();
            this.tourLogic = tourLogic;
        }
        private void LoadData()
        {
            try
            {
                if (tourGuides != null)
                {
                    SelectedGuidesListBox.Items.Clear();
                    foreach (var mm in tourGuides)
                    {
                        SelectedGuidesListBox.Items.Add(new object[] { mm.Key, mm.Value.Item1, mm.Value.Item2 });
                    }
                }                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void WindowTour_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TourViewModel view = tourLogic.Read(new TourBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tourGuides = view.TourGuides;
                        NameTextBox.Text = view.TourName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                tourGuides = new Dictionary<int, (string, decimal)>();
            }
            LoadData();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            
            if (tourGuides.Count == 0)
            {
                MessageBox.Show("Выберите гида", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            try
            {
                tourLogic.CreateOrUpdate(new TourBindingModel
                {
                    Id = id,
                    TourName = NameTextBox.Text,                    
                    TourGuides = tourGuides
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK,
               MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGuidesListBox.SelectedItems.Count == 1)
            {
                //tourGuides.Remove(tourGuides.Where(rec => rec.Value == (int)SelectedGuidesListBox.SelectedItem).ToList()[0].Key);
                LoadData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //var window = App.Container.Resolve<WindowGuides>();
            if (CanSelectedGuidesListBox.SelectedItems.Count == 1)
            {
                //tourGuides.Add(((GuideViewModel)CanSelectedGuidesListBox.SelectedItem).Id, (window.GuideName, window.Cost));
                LoadData();
            }
        }
    }
}
