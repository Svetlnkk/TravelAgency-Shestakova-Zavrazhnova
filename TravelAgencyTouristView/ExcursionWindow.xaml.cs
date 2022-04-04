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
using TravelAgencyContracts.BussinessLogicsContracts;


namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для ExcursionWindow.xaml
    /// </summary>
    public partial class ExcursionWindow : Window
    {
        private readonly IExcursionLogic excursionLogic;
        public int Id { set { id = value; } }
        private int? id;
        public ExcursionWindow(IExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.excursionLogic = excursionLogic;
        }
        private void ExcursionWindowLoad(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = excursionLogic.Read(new ExcursionBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameBox.Text = view.Name;
                        TypeBox.Text = view.Type;
                        TimeBox.Text = view.Time.ToString();
                        PriceBox.Text = view.Price.ToString();
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
            string name = NameBox.Text;
            string type = TypeBox.Text;
            int time = Convert.ToInt32(TimeBox.Text);
            int price = Convert.ToInt32(PriceBox.Text);
            excursionLogic.CreateOrUpdate(new ExcursionBindingModel { Id = id, Name = name, Type = type, Time = time, Price = price });
            this.Close();
        }
    }
}
