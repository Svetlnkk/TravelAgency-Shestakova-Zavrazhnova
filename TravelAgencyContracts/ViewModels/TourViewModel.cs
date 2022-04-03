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
        public string OperatorLogin { get; set; }
        override
        public string ToString()
        {
            return String.Format(@"Id = {0}, Название тура = {1}", Id, TourName);
        }
    }
}
