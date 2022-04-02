using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace TravelAgencyContracts.ViewModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название посещенного места")]
        public string Name { get; set; }
        [DisplayName("Дата визита места")]
        public DateTime DateOfVisit { get; set; }
        public string TouristLogin { get; set; }
        public int PlaceExcursion { get; set; }
    }
}
