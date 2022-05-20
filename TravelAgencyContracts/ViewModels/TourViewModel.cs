using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TravelAgencyContracts.ViewModels
{
    public class TourViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название тура")]
        public string TourName { get; set; }
        [DisplayName("Гиды")]
        public Dictionary<int, (string, decimal)> TourGuides { get; set; }      
    }
}
