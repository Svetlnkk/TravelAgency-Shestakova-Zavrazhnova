using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyContracts.BindingModels;
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
    /// Логика взаимодействия для WindowGuide.xaml
    /// </summary>
    public partial class WindowGuide : Window
    {
        GuideLogic guideLogic = new GuideLogic(new GuideStorage());
        public int Id { set { id = value; } }
        private int? id;
        public WindowGuide()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CostBox.Text == "" || !int.TryParse(CostBox.Text, out int number))
            {
                MessageBox.Show("Введите зарплату в виде числа");
                return;
            }
            int cost = Convert.ToInt32(CostBox.Text);
            string fio = FIOBox.Text;
            guideLogic.CreateOrUpdate(new GuideBindingModel { Id = id, GuideName = fio, Cost = cost });
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowGuide_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = guideLogic.Read(new GuideBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        FIOBox.Text = view.GuideName;
                        CostBox.Text = view.Cost.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
