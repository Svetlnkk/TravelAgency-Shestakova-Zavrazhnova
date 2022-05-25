﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ExcursionLogic : IExcursionLogic
    {
        private readonly IExcursionStorage excursionStorage;
        private readonly IGuideStorage guideStorage;
        public ExcursionLogic(IExcursionStorage excursionStorage, IGuideStorage guideStorage)
        {
            this.excursionStorage = excursionStorage;
            this.guideStorage = guideStorage;
        }
        public List<ExcursionViewModel> Read(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return excursionStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ExcursionViewModel> { excursionStorage.GetElement(model) };
            }
            return excursionStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ExcursionBindingModel model)
        {
            if (model.Id.HasValue)
            {
                excursionStorage.Update(model);
            }
            else
            {
                excursionStorage.Insert(model);
            }
        }
        public void Delete(ExcursionBindingModel model)
        {
            var element = excursionStorage.GetElement(model);
            if (element == null)
            {
                throw new Exception("Элмент не найден");
            }
            excursionStorage.Delete(model);
        }
        
    }
}
