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
        public decimal Cost { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Экскурсии")]
        public Dictionary<int, (string, string)> GuideExcursions { get; set; }
        
        
    }
}
