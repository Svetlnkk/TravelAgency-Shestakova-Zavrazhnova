using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BussinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class GuideLogic : IGuideLogic
    {
        private readonly IGuideStorage guideStorage;        
        public GuideLogic(IGuideStorage guideStorage)
        {
            this.guideStorage = guideStorage;            
        }
        public List<GuideViewModel> Read(GuideBindingModel model)
        {
            if (model == null)
            {
                return guideStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GuideViewModel> { guideStorage.GetElement(model) };
            }
            return guideStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(GuideBindingModel model)
        {
            var element = guideStorage.GetElement(new GuideBindingModel
            {
                GuideName = model.GuideName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть гид с таким именем");
            }
            if (model.Id.HasValue)
            {
                guideStorage.Update(model);
            }
            else
            {
                guideStorage.Insert(model);
            }
        }
        public void Delete(GuideBindingModel model)
        {
            var element = guideStorage.GetElement(new GuideBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Гид не найден");
            }
            guideStorage.Delete(model);
        }
        
    }
}
