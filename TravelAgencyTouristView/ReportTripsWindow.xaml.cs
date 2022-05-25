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
using Microsoft.Win32;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.BindingModels;
using Unity;

namespace TravelAgencyTouristView
{
    /// <summary>
    /// Логика взаимодействия для ReportTripsWindow.xaml
    /// </summary>
    public partial class ReportTripsWindow : Window
    {
        private readonly IReportLogic reportLogic;
        public ReportTripsWindow(IReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            DataGridTextColumn textColumnDate = new DataGridTextColumn();
            textColumnDate.Header = "Дата создания путешествия";
            textColumnDate.Binding = new Binding("dateCreate");
            DataGrid.Columns.Add(textColumnDate);
            DataGridTextColumn textColumnName = new DataGridTextColumn();
            textColumnName.Header = "Название путешествия";
            textColumnName.Binding = new Binding("name");
            DataGrid.Columns.Add(textColumnName);
            DataGridTextColumn textColumnPlaceName = new DataGridTextColumn();
            textColumnPlaceName.Header = "Название экскурсии";
            textColumnPlaceName.Binding = new Binding("excursionName");
            DataGrid.Columns.Add(textColumnPlaceName);
            DataGridTextColumn textColumnGuideName = new DataGridTextColumn();
            textColumnGuideName.Header = "Имя гида";
            textColumnGuideName.Binding = new Binding("guideName");
            DataGrid.Columns.Add(textColumnGuideName);
        }
        private class itemTrip
        {
            public string dateCreate { get; set; }
            public string name { get; set; }
            public string excursionName { get; set; }
            public string guideName { get; set; }
        }
        private void SendMessageClick(object sender, RoutedEventArgs e)
        {
            if (DatePickerAfter.SelectedDate == null || DatePickerBefore.SelectedDate == null ||
                DatePickerAfter.SelectedDate >= DatePickerBefore.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка");
                return;
            }
            SendMailWindow sendMailWindow = App.Container.Resolve<SendMailWindow>();
            sendMailWindow.DateAfter = DatePickerAfter.SelectedDate.Value;
            sendMailWindow.DateBefore = DatePickerBefore.SelectedDate.Value;
            sendMailWindow.ShowDialog();
        }
        private void ShowClick(object sender, RoutedEventArgs e)
        {
            if (DatePickerAfter.SelectedDate == null || DatePickerBefore.SelectedDate == null ||
                DatePickerAfter.SelectedDate >= DatePickerBefore.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка");
                return;
            }
            try
            {
                var dict = reportLogic.GetTripsPCView(new ReportBindingModel()
                {
                    DateAfter = DatePickerAfter.SelectedDate,
                    DateBefore = DatePickerBefore.SelectedDate,
                    TouristLogin = AuthorizationWindow.AutorizedTourist
                });
                if (dict != null)
                {
                    DataGrid.Items.Clear();
                    foreach (var elem in dict)
                    {
                        DataGrid.Items.Add(new itemTrip()
                        {
                            dateCreate = elem.DateCreate.ToShortDateString(),
                            name = elem.Name.ToString()
                        });
                        for (int i = 0; i < Math.Max(elem.Guides.Count, elem.Excursions.Count); ++i)
                        {
                            Console.WriteLine(elem.Guides);
                            itemTrip newItem = new itemTrip();
                            if (i < elem.Guides.Count)
                            {
                                newItem.guideName = elem.Guides[i].GuideName;
                            }
                            if (i < elem.Excursions.Count)
                            {
                                newItem.excursionName = elem.Excursions[i].Name;
                            }
                            DataGrid.Items.Add(newItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
