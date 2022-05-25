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
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.BussinessLogicsContracts;
using Unity;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public static string AutorizedTourist { get; private set; }
        private readonly ITouristLogic workerLogic;
        public AuthorizationWindow(ITouristLogic workerLogic)
        {
            InitializeComponent();
            this.workerLogic = workerLogic;
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = App.Container.Resolve<RegistrationWindow>();
            registrationWindow.ShowDialog();
        }
        private void AutorizedClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;
            if (login == "" || password == "")
            {
                MessageBox.Show("Для авторизации необходимо ввести логин и пароль");
                return;
            }
            if (workerLogic.Login(new TouristBindingModel { Login = login, Password = password}))
            {
                AutorizedTourist = login;
                MainWindow mainWindow = App.Container.Resolve<MainWindow>();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не существует или пароль введен не верно");
                return;
            }
        }
    }
}
