using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TravelAgencyContracts.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО гида")]
        public string GuideName { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Tours { get; set; }
    }
}
