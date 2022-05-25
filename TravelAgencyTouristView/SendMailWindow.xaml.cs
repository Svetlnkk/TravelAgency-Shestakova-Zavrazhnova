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
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.BindingModels;
using TravelAgencyBusinessLogic.MailWorker;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для SendMailWindow.xaml
    /// </summary>
    public partial class SendMailWindow : Window
    {
        private readonly IReportLogic reportLogic;
        private readonly ITouristLogic touristLogic;
        private readonly MailKitWorker mailKitWorker;
        public DateTime DateAfter { get; set; }
        public DateTime DateBefore { get; set; }
        public SendMailWindow(IReportLogic reportLogic, ITouristLogic visitorLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            this.touristLogic = visitorLogic;
            mailKitWorker = new MailKitWorker();
        }
        private void SendMailWindowLoad(object sender, RoutedEventArgs e)
        {
            MailAdressBox.Text = touristLogic.GetTouristData(new TouristBindingModel { Login = AuthorizationWindow.AutorizedTourist}).Email;
        }
        private void SendMessageClick(object sender, RoutedEventArgs e)
        {
            //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            //Match match = regex.Match(MailAdressBox.Text);
            //if (!match.Success)
            //{
            //    MessageBox.Show("Данные введенные в поле \"Адрес почты\" должны соответствовать адресу электронной почте");
            //    return;
            //}
            //try
            //{
            //    reportLogic.saveTripsToPdfFile(new ReportBindingModel()
            //    {
            //        DateAfter = DateAfter,
            //        DateBefore = DateBefore,
            //        FileName = "reportOrdersByDate.pdf",
            //        TouristLogin = AuthorizationWindow.AutorizedTourist
            //    });
            //    mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
            //    {
            //        MailAddress = MailAdressBox.Text,
            //        Subject = "Отчет. Турфирма \"Иван Сусанин\"",
            //        Text = "Отчет по экскурсиям в промежутке дат от " + DateAfter.ToShortDateString() + " до " + DateBefore.ToShortDateString(),
            //        FileAttachment = "reportOrdersByDate.pdf"
            //    });
            //    MessageBox.Show("Письмо успешно отправлено");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка");
            //}
            //Close();
        }
    }
}
