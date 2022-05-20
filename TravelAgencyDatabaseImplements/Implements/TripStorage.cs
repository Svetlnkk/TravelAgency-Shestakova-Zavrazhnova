using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyDatabaseImplements.Models;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class TripStorage : ITripStorage
    {
        public List<TripViewModel> GetFullList()
        {
            using var context = new TravelAgencyDatabase();
            return context.Trips.Include(rec => rec.TripTours).Include(rec => rec.TripExcursions).Where(rec => rec.TouristLogin == TouristStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public List<TripViewModel> GetFilteredList(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            return context.Trips.Include(rec => rec.TripTours).Include(rec => rec.TripExcursions).Where(rec => rec.Id == model.Id && rec.TouristLogin == TouristStorage.AutorizedWorker ||
                rec.Date > model.after && rec.Date < model.before && rec.TouristLogin == TouristStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public TripViewModel GetElement(TripBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var trip = context.Trips.Include(rec => rec.TripTours).Include(rec => rec.TripExcursions).Where(rec => rec.TouristLogin == TouristStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            return trip != null ? CreateModel(trip) : null;
        }
        public void Insert(TripBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Trip trip = new Trip()
                {
                    Name = model.Name,
                    Date = model.Date,
                    TouristLogin = TouristStorage.AutorizedWorker
                };
                context.Trips.Add(trip);
                context.SaveChanges();
                CreateModel(model, trip, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(TripBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Trips.Include(rec => rec.TripTours).Where(rec => rec.TouristLogin == TouristStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(TripBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            Trip element = context.Trips.Include(rec => rec.TripTours).Where(rec => rec.TouristLogin == TouristStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Trips.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void AddExcursion((int, (int, int)) addedExcursion)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (context.TripExcursions.FirstOrDefault(rec => rec.TripId == addedExcursion.Item1 && rec.ExcursionId == addedExcursion.Item2.Item1) != null)
                {
                    var tripExcursion = context.TripExcursions.FirstOrDefault(rec => rec.TripId == addedExcursion.Item1 && rec.ExcursionId == addedExcursion.Item2.Item1);
                    tripExcursion.ExcursionCount = addedExcursion.Item2.Item2;
                }
                else
                {
                    context.TripExcursions.Add(new TripExcursions { TripId = addedExcursion.Item1, ExcursionId = addedExcursion.Item2.Item1, ExcursionCount = addedExcursion.Item2.Item2 });
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
        private static Trip CreateModel(TripBindingModel model, Trip trip, TravelAgencyDatabase context)
        {
            trip.Name = model.Name;
            trip.Date = model.Date;
            if (model.Id.HasValue)
            {
                var tripTours = context.TripTours.Where(rec => rec.TripId == model.Id.Value).ToList();
                context.TripTours.RemoveRange(tripTours.Where(rec => !model.TripTours.ContainsKey(rec.TourId)).ToList());
                context.SaveChanges();
                tripTours = context.TripTours.Where(rec => rec.TripId == model.Id.Value).ToList();
                foreach (var updateTours in tripTours)
                {
                    updateTours.TourCount = model.TripTours[updateTours.TourId];
                    model.TripTours.Remove(updateTours.TourId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.TripTours)
            {
                context.TripTours.Add(new TripTours
                {
                    TripId = trip.Id,
                    TourId = pc.Key,
                    TourCount = pc.Value
                });
                context.SaveChanges();
            }
            return trip;
        }
        private static TripViewModel CreateModel(Trip trip)
        {
            return new TripViewModel
            {
                Id = trip.Id,
                Date = trip.Date,
                Name = trip.Name,
                TouristLogin = trip.TouristLogin,
                TripTours = trip.TripTours.ToDictionary(rec => rec.TourId, rec => rec.TourCount),
                TripExcursions = trip.TripExcursions.ToDictionary(rec => rec.ExcursionId, rec => rec.ExcursionCount)
            };
        }
    }
}
