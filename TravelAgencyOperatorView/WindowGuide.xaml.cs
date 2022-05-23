using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyContracts.BindingModels;
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
using Unity;

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowGuide.xaml
    /// </summary>
    public partial class WindowGuide : Window
    {
        private readonly IGuideLogic guideLogic;
        public int Id { set { id = value; } }
        private int? id;
        public WindowGuide(IGuideLogic guideLogic)
        {
            InitializeComponent();
            this.guideLogic = guideLogic;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (FIOBox.Text == "")
            {
                MessageBox.Show("Заполните ФИО", "Ошибка");
                return;
            }
            if (CostBox.Text == "" || !int.TryParse(CostBox.Text, out int number))
            {
                MessageBox.Show("Введите зарплату в виде числа");
                return;
            }            
            guideLogic.CreateOrUpdate(new GuideBindingModel
            {
                Id = id,
                GuideName = FIOBox.Text,
                Cost = Convert.ToDecimal(CostBox.Text),
                Date = DateTime.Now,
                OperatorLogin = WindowAuthorization.AutorizedOperator
            });
            MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK,
               MessageBoxImage.Information);                
                Close();            
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
