using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.StoragesContracts
{
    public interface IGuideStorage
    {
        List<GuideViewModel> GetFullList();
        List<GuideViewModel> GetFilteredList(GuideBindingModel model);
        GuideViewModel GetElement(GuideBindingModel model);
        void Insert(GuideBindingModel model);
        void Update(GuideBindingModel model);
        void Delete(GuideBindingModel model);
        void AddTour((int, (int, int)) addTour);
    }
}
