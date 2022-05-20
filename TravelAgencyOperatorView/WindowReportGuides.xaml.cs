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

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowReportGuides.xaml
    /// </summary>
    public partial class WindowReportGuides : Window
    {
        private readonly IReportOperatorLogic reportOperator;
        public WindowReportGuides(IReportOperatorLogic reportOperator)
        {
            InitializeComponent();
            this.reportOperator = reportOperator;
            DataGridTextColumn textColumnDate = new DataGridTextColumn();
            textColumnDate.Header = "Дата";
            textColumnDate.Binding = new Binding("dateCreate");
            DataGrid.Columns.Add(textColumnDate);
            DataGridTextColumn textColumnWeight = new DataGridTextColumn();
            textColumnWeight.Header = "Имя гида";
            textColumnWeight.Binding = new Binding("name");
            DataGrid.Columns.Add(textColumnWeight);
            DataGridTextColumn textColumnExcursionName = new DataGridTextColumn();
            textColumnExcursionName.Header = "Название экскурсий";
            textColumnExcursionName.Binding = new Binding("excursionName");
            DataGrid.Columns.Add(textColumnExcursionName);
            DataGridTextColumn textColumnTourName = new DataGridTextColumn();
            textColumnTourName.Header = "Название тура";
            textColumnTourName.Binding = new Binding("tourName");
            DataGrid.Columns.Add(textColumnTourName);
            /*DataGridTextColumn textColumnCookName = new DataGridTextColumn();
            textColumnCookName.Header = "Имя гида";
            textColumnCookName.Binding = new Binding("guideName");
            DataGrid.Columns.Add(textColumnCookName);*/
        }
        private class itemGuide
        {
            public string dateCreate { get; set; }
            public string name { get; set; }
            public string excursionName { get; set; }
            public string tourName { get; set; }            
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerAfter.SelectedDate == null || DatePickerBefore.SelectedDate == null ||
                DatePickerAfter.SelectedDate >= DatePickerBefore.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка");
                return;
            }
            try
            {
                var dict = reportOperator.GetGuides(new ReportOperatorBindingModel()
                {
                    DateFrom = DatePickerAfter.SelectedDate,
                    DateTo= DatePickerBefore.SelectedDate
                });
                if (dict != null)
                {
                    DataGrid.Items.Clear();
                    foreach (var elem in dict)
                    {
                        DataGrid.Items.Add(new itemGuide()
                        {
                            dateCreate = elem.DateCreate.ToShortDateString(),
                            name = elem.GuideName.ToString()
                        });
                        for (int i = 0; i < Math.Max(elem.Tours.Count, elem.Excursions.Count); ++i)
                        {
                            itemGuide newItem = new itemGuide();
                            if (i < elem.Tours.Count)
                            {
                                newItem.tourName = elem.Tours[i].TourName;
                            }
                            if (i < elem.Excursions.Count)
                            {
                                newItem.excursionName = elem.Excursions[i].Name;
                                //newItem.placeCount = elem.Places[i].DateOfVisit.ToString();
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

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "pdf|*.pdf";
            if (dialog.ShowDialog() == true)
            {
                reportOperator.saveGuidesToPdfFile(new ReportOperatorBindingModel()
                {
                    DateFrom = DatePickerAfter.SelectedDate,
                    DateTo = DatePickerBefore.SelectedDate,
                    FileName = dialog.FileName
                });
            }
        }
    }
}
