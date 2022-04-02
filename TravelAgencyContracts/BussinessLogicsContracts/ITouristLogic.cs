using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;


namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface ITouristLogic
    {
        void Create(TouristBindingModel model);
        bool Login(TouristBindingModel model);
    }
}
