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
    public class TourStorage : ITourStorage
    {
        public void Delete(TourBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Tours.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }

        }

        public TourViewModel GetElement(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var tour = context.Tours            
            .Include(rec => rec.TourGuides)            
            .FirstOrDefault(rec => rec.TourName == model.TourName ||
            rec.Id == model.Id);
            return tour != null ? CreateModel(tour) : null;
        }

        public List<TourViewModel> GetFilteredList(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            return context.Tours
            .Include(rec => rec.TourGuides)            
            .Where(rec => rec.TourName.Contains(model.TourName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<TourViewModel> GetFullList()
        {
            using var context = new TravelAgencyDatabase();
            return context.Tours
            .Include(rec => rec.TourGuides)
            .ThenInclude(rec => rec.Guide)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(TourBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Tour tour = new Tour()
                {
                    TourName = model.TourName,                    
                    OperatorLogin = model.OperatorLogin
                };
                context.Tours.Add(tour);
                context.SaveChanges();
                CreateModel(model, tour, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(TourBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Tours.FirstOrDefault(rec => rec.Id ==
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
        private static Tour CreateModel(TourBindingModel model, Tour tour, TravelAgencyDatabase context)
        {
            tour.TourName = model.TourName;            
            if (model.Id.HasValue)
            {
                var lpGuides = context.TourGuides.Where(rec =>
                rec.TourId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.TourGuides.RemoveRange(lpGuides.Where(rec =>
                !model.TourGuides.ContainsKey(rec.GuideId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in lpGuides)
                {
                    model.TourGuides.Remove(updateIngredient.GuideId);
                }
                context.SaveChanges();
            }
            foreach (var wi in model.TourGuides)
            {
                context.TourGuides.Add(new GuideTour
                {
                    TourId = tour.Id,
                    GuideId = wi.Key,
                });
                context.SaveChanges();
            }
            return tour;
        }

        private static TourViewModel CreateModel(Tour tour)
        {
            return new TourViewModel
            {
                Id = tour.Id,
                TourName = tour.TourName,
                TourGuides = tour.TourGuides
            .ToDictionary(recII => recII.GuideId,
            recII => (recII.Guide?.GuideName, recII.Guide.Cost))
            };
        }
    }
}
