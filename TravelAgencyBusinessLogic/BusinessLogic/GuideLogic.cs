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
        private readonly IExcursionStorage excursionStorage;
        public GuideLogic(IGuideStorage guideStorage, IExcursionStorage excursionStorage)
        {
            this.guideStorage = guideStorage;
            this.excursionStorage = excursionStorage;
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
            var element = guideStorage.GetElement(model);
            if (element == null)
            {
                throw new Exception("Гид не найден");
            }
            guideStorage.Delete(model);
        }
        public void AddExcursion(AddGuideExcursionBindingModel addExcursion)
        {
            var guide = guideStorage.GetElement(new GuideBindingModel { Id = addExcursion.GuideId, OperatorLogin = addExcursion.OperatorLogin });
            if (guide == null)
            {
                throw new Exception("Гид не найден");
            }
            var excursion = excursionStorage.GetElement(new ExcursionBindingModel { Id = addExcursion.ExcursionId, OperatorLogin = addExcursion.OperatorLogin });
            if (excursion == null)
            {
                throw new Exception("Экскурсия не найдена");
            }
            guideStorage.AddExcursion(addExcursion);
        }
        
    }
}
