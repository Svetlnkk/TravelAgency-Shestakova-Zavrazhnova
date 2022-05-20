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
using System.Text.RegularExpressions;

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        private readonly IOperatorLogic operatorLogic;
        public WindowRegistration(IOperatorLogic operatorLogic)
        {
            InitializeComponent();
            this.operatorLogic = operatorLogic;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;
            string name = NameTextBox.Text;
            string email = EmailTextBox.Text;
            if (login == "" || password == "")
            {
                MessageBox.Show("Для регистрации необходимо ввести логин и пароль");
                return;
            }
            if (name == "" || email == "")
            {
                MessageBox.Show("Поля \"имя\" и \"Email\" - обязательны к заполнению");
                return;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Данные введенные в поле \"Email\" должны соответствовать адресу электронной почте");
                return;
            }
            try
            {
                operatorLogic.Create(new OperatorBindingModel { Login = login, Password = password, Email = email, Name = name });
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
