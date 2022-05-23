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
        private readonly ITourLogic tourLogic;
        private readonly IGuideLogic guideLogic;
        private int? id;
        private Dictionary<int, int> tourGuides;
        public WindowTour(ITourLogic tourLogic, IGuideLogic guideLogic)
        {
            InitializeComponent();
            this.tourLogic = tourLogic;
            this.guideLogic = guideLogic;
        }        
        private void WindowTour_Loaded(object sender, RoutedEventArgs e)
        {
            var list = guideLogic.Read(null);
            CanSelectedGuidesListBox.ItemsSource = list;
            CanSelectedGuidesListBox.SelectedItem = null;
            SelectedGuidesListBox.SelectedItem = null;
            if (id.HasValue)
            {
                try
                {
                    var view = tourLogic.Read(new TourBindingModel { Id = id, OperatorLogin = WindowAuthorization.AutorizedOperator })?[0];
                    if (view != null)
                    {
                        NameTextBox.Text = view.TourName.ToString();                        
                        var canSelectedList = guideLogic.Read(null)
                            /*.Where(rec => view.GuideTours.ContainsKey(rec.Id))*/;
                        /*if (view.GuideTours.Count != 0)
                        {
                            CountBox.Text = view.GuideTours.First().Value.ToString();
                        }*/
                        foreach (GuideViewModel i in CanSelectedGuidesListBox.Items)
                        {
                            if (canSelectedList.FirstOrDefault(rec => rec.Id == i.Id) != null)
                            {
                                SelectedGuidesListBox.Items.Add(i);
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            
            if (SelectedGuidesListBox.Items.Count == 0)
            {
                MessageBox.Show("Выберите гида", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            try
            {
                string name = NameTextBox.Text;
                tourGuides = new Dictionary<int, int>();                
                tourLogic.CreateOrUpdate(new TourBindingModel
                {
                    Id = id,
                    TourName = NameTextBox.Text,                    
                    TourGuides = tourGuides,
                    OperatorLogin = WindowAuthorization.AutorizedOperator
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);                
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
                int removeIndex = SelectedGuidesListBox.SelectedIndex;
                SelectedGuidesListBox.SelectedItem = null;
                SelectedGuidesListBox.Items.RemoveAt(removeIndex);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedGuidesListBox.SelectedItem != null) {
                var changeItem = CanSelectedGuidesListBox.SelectedItem;
                if (CanSelectedGuidesListBox.SelectedItems.Count == 1)
                {
                    SelectedGuidesListBox.Items.Add(changeItem);
                }
                CanSelectedGuidesListBox.SelectedItem = null;
            }
        }
    }
}
