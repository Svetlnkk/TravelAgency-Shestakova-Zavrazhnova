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
        public void Delete(GuideBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            Guide element = context.Guides.FirstOrDefault(rec => rec.Id == model.Id);
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

        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var guide = context.Guides
            .Include(rec => rec.TourGuides)
            .Include(rec => rec.ExcursionGuides)      
            .Include(rec => rec.Operator)
            .FirstOrDefault(rec => rec.GuideName == model.GuideName || rec.Id == model.Id);
            return guide != null ? CreateModel(guide) : null;
        }

        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();

            return context.Guides            
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.Date.Date == model.Date.Date) ||
            (model.DateFrom.HasValue && model.DateTo.HasValue && rec.Date.Date >= model.DateFrom.Value.Date && rec.Date.Date <= model.DateTo.Value.Date) ||
            (!String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin))            
            .Select(CreateModel)
            .ToList();
        }

        public List<GuideViewModel> GetFullList()
        {
            using var context = new TravelAgencyDatabase();
            return context.Guides   
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(GuideBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Guides.Add(CreateModel(model, new Guide(), context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(GuideBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Guides.FirstOrDefault(rec => rec.Id ==
                model.Id);
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
        private static Guide CreateModel(GuideBindingModel model, Guide guide, TravelAgencyDatabase context)
        {
            guide.GuideName = model.GuideName;
            guide.Cost = model.Cost;
            guide.OperatorLogin = model.OperatorLogin;
            guide.Date = model.Date;
            if (model.Id.HasValue)
            {
                var guideExcursion = context.ExcursionGuides.Where(rec => rec.ExcursionId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ExcursionGuides.RemoveRange(guideExcursion.Where(rec => !model.GuideExcursions.ContainsKey(rec.ExcursionId)).ToList());
                context.SaveChanges();
            }
            // добавили новые
            foreach (var cd in model.GuideExcursions)
            {
                context.ExcursionGuides.Add(new ExcursionGuide
                {
                    ExcursionId = cd.Key,
                    GuideId = guide.Id,
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
                GuideName = guide.GuideName,
                Cost = guide.Cost,
                Date = guide.Date,
                GuideExcursions = guide.ExcursionGuides
            .ToDictionary(recII => recII.ExcursionId,
            recII => (recII.Excursion?.Name, recII.Excursion.Type))
            };
        }
    }
}
