using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace TravelAgencyContracts.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО гида")]
        public string GuideName { get; set; }
        [DisplayName("Стоимость услуги гида")]
        public int Cost { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        public string OperatorLogin { get; set; }
        public Dictionary<int, int> GuideExcursions { get; set; }
        public Dictionary<int, int> GuideTours { get; set; }
        override
        public string ToString()
        {
            return String.Format(@"ФИО = {0}, Зарплата = {1}, Дата = {2}", GuideName, Cost, Date.ToString("d", CultureInfo.GetCultureInfo("en-US")));
        }

    }
}
