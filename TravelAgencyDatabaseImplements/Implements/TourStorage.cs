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
        public List<TourViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tours
                    .Include(rec => rec.TourGuides)
                .Select(CreateModel)
                .ToList();
            }
        }
        public List<TourViewModel> GetFilteredList(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tours
                    .Include(rec => rec.TourGuides)
                .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                .Select(CreateModel)
                .ToList();
            }
        }
        public TourViewModel GetElement(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var tour = context.Tours
                    .Include(rec => rec.TourGuides)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return tour != null ? CreateModel(tour) : null;
            }
        }
        public void Insert(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
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
            }
        }

        public void Update(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Tours
                            .Include(rec => rec.TourGuides)
                            .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                            .FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Tour element = context.Tours
                    .Include(rec => rec.TourGuides)
                    .Where(rec => !String.IsNullOrEmpty(model.OperatorLogin) && rec.OperatorLogin == model.OperatorLogin)
                    .FirstOrDefault(rec => rec.Id == model.Id);
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

        }
        private static Tour CreateModel(TourBindingModel model, Tour tour, TravelAgencyDatabase context)
        {
            tour.TourName = model.TourName;
            tour.OperatorLogin = model.OperatorLogin;
            if (model.Id.HasValue)
            {
                var guideTours = context.TourGuides.Where(rec => rec.TourId == model.Id.Value).ToList();
                context.TourGuides.RemoveRange(guideTours.Where(rec => !model.TourGuides.ContainsKey(rec.GuideId)).ToList());
                context.SaveChanges();
                guideTours = context.TourGuides.Where(rec => rec.TourId == model.Id.Value).ToList();
                foreach (var updateGuides in guideTours)
                {
                    updateGuides.TourCount = model.TourGuides[updateGuides.GuideId];
                    model.TourGuides.Remove(updateGuides.GuideId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.TourGuides)
            {
                context.TourGuides.Add(new GuideTour
                {
                    TourId = tour.Id,
                    GuideId = pc.Key,
                    TourCount = pc.Value
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
                OperatorLogin=tour.OperatorLogin,
                GuideTours = tour.TourGuides.ToDictionary(rec => rec.GuideId, rec => rec.TourCount)
            };
        }
    }
}
