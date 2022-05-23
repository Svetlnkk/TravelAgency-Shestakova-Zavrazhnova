using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TravelAgencyContracts.ViewModels
{
    public class StopViewModel
    {
        public int Id { get; set; }        
        [DisplayName("Название остановки")]
        public string StopName { get; set; }
        [DisplayName("Дата заезда")]
        public DateTime CheckinDateStop { get; set; }
        [DisplayName("Дата выезда")]
        public DateTime DateofDepatureStop { get; set; }
        public String OperatorLogin { get; set; }
        public int TourId { get; set; }

    }
}
