using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IGuideLogic
    {
        List<GuideViewModel> Read(GuideBindingModel model);
        void CreateOrUpdate(GuideBindingModel model);
        void Delete(GuideBindingModel model);
        void AddExcursion(AddGuideExcursionBindingModel addExcursion);
    }
}
