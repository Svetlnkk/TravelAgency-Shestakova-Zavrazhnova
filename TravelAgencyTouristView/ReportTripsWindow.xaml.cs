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
            DataGridTextColumn textColumnWeight = new DataGridTextColumn();
            textColumnWeight.Header = "Название путешествия";
            textColumnWeight.Binding = new Binding("name");
            DataGrid.Columns.Add(textColumnWeight);
            DataGridTextColumn textColumnCutleryName = new DataGridTextColumn();
            textColumnCutleryName.Header = "Название посещенного места";
            textColumnCutleryName.Binding = new Binding("placeName");
            DataGrid.Columns.Add(textColumnCutleryName);
            DataGridTextColumn textColumnCutleryCount = new DataGridTextColumn();
            textColumnCutleryCount.Header = "Количество посещенных мест";
            textColumnCutleryCount.Binding = new Binding("placeCount");
            DataGrid.Columns.Add(textColumnCutleryCount);
            DataGridTextColumn textColumnCookName = new DataGridTextColumn();
            textColumnCookName.Header = "Имя гида";
            textColumnCookName.Binding = new Binding("guideName");
            DataGrid.Columns.Add(textColumnCookName);
        }
        private class itemLucnh
        {
            public string dateCreate { get; set; }
            public string name { get; set; }
            public string placeName { get; set; }
            public string placeCount { get; set; }
            public string guideName { get; set; }
        }
        private void SendMessageClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "pdf|*.pdf";
            if (dialog.ShowDialog() == true)
            {
                reportLogic.saveTripsToPdfFile(new ReportBindingModel()
                {
                    DateAfter = DatePickerAfter.SelectedDate,
                    DateBefore = DatePickerBefore.SelectedDate,
                    FileName = dialog.FileName
                });
            }
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
                var dict = reportLogic.GetTripsPCView(new ReportBindingModel() { 
                    DateAfter = DatePickerAfter.SelectedDate,
                    DateBefore = DatePickerBefore.SelectedDate
                });
                if (dict != null)
                {
                    DataGrid.Items.Clear();
                    foreach (var elem in dict)
                    {
                        DataGrid.Items.Add(new itemLucnh() { 
                            dateCreate = elem.DateCreate.ToShortDateString(),
                            name = elem.Name.ToString()
                        });
                        for (int i = 0; i < Math.Max(elem.Guides.Count, elem.Places.Count); ++i)
                        {
                            itemLucnh newItem = new itemLucnh();
                            if (i < elem.Guides.Count)
                            {
                                newItem.guideName = elem.Guides[i].GuideName;
                            }
                            if (i < elem.Places.Count)
                            {
                                newItem.placeName = elem.Places[i].Name;
                                newItem.placeCount = elem.Places[i].DateOfVisit.ToString();
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
