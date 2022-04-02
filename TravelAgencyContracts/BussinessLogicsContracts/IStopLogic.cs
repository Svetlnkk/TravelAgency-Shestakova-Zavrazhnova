using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IStopLogic
    {
        List<StopViewModel> Read(StopBindingModel model);
        void CreateOrUpdate(StopBindingModel model);
        void Delete(StopBindingModel model);
    }
}
