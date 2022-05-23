using TravelAgencyDatabaseImplements.Models;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class GuideStorage : IGuideStorage
    {
        public List<GuideViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Guides
                .Include(rec => rec.ExcursionGuides).Include(rec => rec.TourGuides)
                .Select(CreateModel)
                .ToList();
            }
        }
        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                if(model.DateFrom.HasValue && model.DateTo.HasValue)
                {
                    return context.Guides.Include(rec => rec.ExcursionGuides).Include(rec => rec.TourGuides)
                .Where(rec => rec.Date > model.DateFrom && rec.Date < model.DateTo &&
                !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                .Select(CreateModel)
                .ToList();
                }
                else
                {
                    return context.Guides.Include(rec => rec.ExcursionGuides).Include(rec => rec.TourGuides)
                .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                .Select(CreateModel)
                .ToList();
                }
                
            }
        }
        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var tt = GetFullList();
                var guide = context.Guides
                .Include(rec => rec.TourGuides)
                .Include(rec => rec.ExcursionGuides)
                .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return guide != null ? CreateModel(guide) : null;
            }
        }
        public void Insert(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Guide guide = new Guide()
                        {
                            GuideName = model.GuideName,
                            Date = model.Date,
                            Cost = model.Cost,
                            OperatorLogin = model.OperatorLogin
                        };
                        context.Guides.Add(guide);
                        context.SaveChanges();
                        CreateModel(model, guide);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Guides.Include(rec => rec.TourGuides)
                            .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                            .FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Guide element = context.Guides.Include(rec => rec.TourGuides)
                    .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Guides.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void AddExcursion(AddGuideExcursionBindingModel addExcursion)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (context.ExcursionGuides.FirstOrDefault(rec => rec.GuideId == addExcursion.GuideId && rec.ExcursionId == addExcursion.ExcursionId) != null)
                        {
                            var guideExcursion = context.ExcursionGuides.FirstOrDefault(rec => rec.GuideId == addExcursion.GuideId && rec.ExcursionId == addExcursion.ExcursionId);
                            guideExcursion.ExcursionCount = addExcursion.ExcursionCount;
                        }
                        else
                        {
                            context.ExcursionGuides.Add(new ExcursionGuide { GuideId = addExcursion.GuideId, ExcursionId = addExcursion.ExcursionId, ExcursionCount = addExcursion.ExcursionCount });
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        private static Guide CreateModel(GuideBindingModel model, Guide guide)
        {
            guide.GuideName = model.GuideName;
            guide.Cost = model.Cost;            
            return guide;
        }
        private static GuideViewModel CreateModel(Guide guide)
        {
            return new GuideViewModel
            {
                Id = guide.Id,
                Date=guide.Date,
                GuideName = guide.GuideName,
                Cost = guide.Cost,
                OperatorLogin=guide.OperatorLogin,
                GuideExcursions=guide.ExcursionGuides.ToDictionary(rec => rec.ExcursionId, rec => rec.ExcursionCount)                               
            };
        }
    }
}
