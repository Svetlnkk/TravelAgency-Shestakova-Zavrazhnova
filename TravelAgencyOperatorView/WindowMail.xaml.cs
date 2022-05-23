using TravelAgencyBusinessLogic.MailWorker;
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
    /// Логика взаимодействия для WindowMail.xaml
    /// </summary>
    public partial class WindowMail : Window
    {
        private readonly IReportOperatorLogic reportOperatorLogic;
        private readonly IOperatorLogic operatorLogic;
        private readonly MailKitWorker mailKitWorker;
        public DateTime DateAfter { get; set; }
        public DateTime DateBefore { get; set; }
        public WindowMail(IReportOperatorLogic reportOperatorLogic, IOperatorLogic operatorLogic)
        {
            InitializeComponent();
            this.reportOperatorLogic = reportOperatorLogic;
            this.operatorLogic = operatorLogic;
            mailKitWorker = new MailKitWorker();
        }

        private void WindowMail_Loaded(object sender, RoutedEventArgs e)
        {
            MailAdressBox.Text = operatorLogic.GetOperatorData(new OperatorBindingModel { Login = WindowAuthorization.AutorizedOperator }).Email;
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(MailAdressBox.Text);
            if (!match.Success)
            {
                MessageBox.Show("Данные введенные в поле \"Адрес почты\" должны соответствовать адресу электронной почте");
                return;
            }
            try
            {
                reportOperatorLogic.saveGuidesToPdfFile(new ReportOperatorBindingModel()
                {
                    DateFrom = DateAfter,
                    DateTo = DateBefore,
                    FileName = "reportGuides.pdf",
                    OperatorLogin = WindowAuthorization.AutorizedOperator
                });
                mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = MailAdressBox.Text,
                    Subject = "Отчет. Турфирма \"Иван Сусанин\"",
                    Text = "Отчет по гидам за период от " + DateAfter.ToShortDateString() + " до " + DateBefore.ToShortDateString(),
                    FileAttachment = "reportGuides.pdf"
                });
                MessageBox.Show("Письмо успешно отправлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
