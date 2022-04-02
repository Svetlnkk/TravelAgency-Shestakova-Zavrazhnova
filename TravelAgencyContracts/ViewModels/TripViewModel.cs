using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace TravelAgencyContracts.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название путешествия")]
        public string Name { get; set; }
        [DisplayName("Дата путешествия")]
        public DateTime Date { get; set; }
        public string TouristLogin { get; set; }
        public Dictionary<int, int> TripExcursions { get; set; }
        public Dictionary<int, int> TripTours { get; set; }
        override
        public string ToString()
        {
            return String.Format(@"Название = {0}, Дата = {1}", Name, Date.ToString("d", CultureInfo.GetCultureInfo("en-US")));
        }
    }
}
