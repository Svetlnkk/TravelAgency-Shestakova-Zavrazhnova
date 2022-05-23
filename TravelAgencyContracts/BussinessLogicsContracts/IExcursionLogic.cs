using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IExcursionLogic
    {
        List<ExcursionViewModel> Read(ExcursionBindingModel model);
        void CreateOrUpdate(ExcursionBindingModel model);
        void Delete(ExcursionBindingModel model);        
    }
}
