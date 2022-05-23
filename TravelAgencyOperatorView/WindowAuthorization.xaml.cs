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
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public static string AutorizedOperator { get; private set; }
        private readonly IOperatorLogic operatorLogic;
        public WindowAuthorization(IOperatorLogic operatorLogic)
        {
            InitializeComponent();
            this.operatorLogic = operatorLogic;
        }

        private void Autorized_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;
            if (login == "" || password == "")
            {
                MessageBox.Show("Необходимо ввести логин и пароль");
                return;
            }
            if (operatorLogic.Login(new OperatorBindingModel { Login = login, Password = password }))
            {
                AutorizedOperator = login;
                MainWindow mainWindow = App.Container.Resolve<MainWindow>();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Пользователь не существует или пароль введен не верно");
                return;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistration windowRegistration = App.Container.Resolve <WindowRegistration>();
            windowRegistration.ShowDialog();
        }
    }
}
