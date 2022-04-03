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
                return context.Guides.Include(rec => rec.GuideExcursions).Include(rec => rec.GuideTours).Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).Select(CreateModel).ToList();
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
                return context.Guides.Include(rec => rec.GuideExcursions).Include(rec => rec.GuideTours).Where(rec => rec.Id == model.Id && rec.OperatorLogin == OperatorStorage.AutorizedOperator ||
                    rec.Date > model.after && rec.Date < model.before && rec.OperatorLogin == OperatorStorage.AutorizedOperator).Select(CreateModel).ToList();
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
                var guide = context.Guides.Include(rec => rec.GuideExcursions).Include(rec => rec.GuideTours).Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).FirstOrDefault(rec => rec.Id == model.Id);
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
                            OperatorLogin = OperatorStorage.AutorizedOperator
                        };
                        context.Guides.Add(guide);
                        context.SaveChanges();
                        CreateModel(model, guide, context);
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
                        var element = context.Guides.Include(rec => rec.GuideExcursions).Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
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
                Guide element = context.Guides.Include(rec => rec.GuideExcursions).Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).FirstOrDefault(rec => rec.Id == model.Id);
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
        public void AddTour((int, (int, int)) addTour)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (context.GuideTours.FirstOrDefault(rec => rec.GuideId == addTour.Item1 && rec.TourId == addTour.Item2.Item1) != null)
                        {
                            var guideTour = context.GuideTours.FirstOrDefault(rec => rec.GuideId == addTour.Item1 && rec.TourId == addTour.Item2.Item1);
                            guideTour.TourCount = addTour.Item2.Item2;
                        }
                        else
                        {
                            context.GuideTours.Add(new GuideTour { GuideId = addTour.Item1, TourId = addTour.Item2.Item1, TourCount = addTour.Item2.Item2 });
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
        private static Guide CreateModel(GuideBindingModel model, Guide guide, TravelAgencyDatabase context)
        {
            guide.GuideName = model.GuideName;
            guide.Cost = model.Cost;
            guide.Date = model.Date;
            if (model.Id.HasValue)
            {
                var guideExcursions = context.GuideExcursions.Where(rec => rec.GuideId == model.Id.Value).ToList();
                context.GuideExcursions.RemoveRange(guideExcursions.Where(rec => !model.GuideExcursions.ContainsKey(rec.ExcursionId)).ToList());
                context.SaveChanges();
                guideExcursions = context.GuideExcursions.Where(rec => rec.GuideId == model.Id.Value).ToList();
                foreach (var updateExcursions in guideExcursions)
                {
                    updateExcursions.ExcursionCount = model.GuideExcursions[updateExcursions.ExcursionId];
                    model.GuideExcursions.Remove(updateExcursions.ExcursionId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.GuideExcursions)
            {
                context.GuideExcursions.Add(new GuideExcursion
                {
                    GuideId = guide.Id,
                    ExcursionId = pc.Key,
                    ExcursionCount = pc.Value
                });
                context.SaveChanges();
            }
            return guide;
        }
        private static GuideViewModel CreateModel(Guide guide)
        {
            return new GuideViewModel
            {
                Id = guide.Id,
                Date = guide.Date,
                GuideName = guide.GuideName,
                Cost = guide.Cost,
                OperatorLogin = guide.OperatorLogin,
                GuideExcursions = guide.GuideExcursions.ToDictionary(rec => rec.ExcursionId, rec => rec.ExcursionCount),
                GuideTours = guide.GuideTours.ToDictionary(rec => rec.TourId, rec => rec.TourCount)
            };
        }
    }
}
