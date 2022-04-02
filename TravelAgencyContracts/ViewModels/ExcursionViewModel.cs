using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TravelAgencyContracts.ViewModels
{
    public class ExcursionViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название экскурсии")]
        public string Name { get; set; }
        [DisplayName("Тип экскурсии")]
        public string Type { get; set; }
        [DisplayName("Продолжительность экскурсии")]
        public double Time { get; set; }
        [DisplayName("Стоимость экскурсии")]
        public int Price { get; set; }
        public string TouristLogin { get; set; }
        override
        public string ToString()
        {
            return String.Format(@"Id = {0}, Название = {1}, Тип = {2}, Продолжительность = {3}, Стоимость = {4}", Id, Name, Type, Time, Price);
        }
    }
}
