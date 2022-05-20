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
    public class StopStorage : IStopStorage
    {
        public void Delete(StopBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            Stop element = context.Stops.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Stops.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public StopViewModel GetElement(StopBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var order = context.Stops
            .Include(rec => rec.Tour)
            .Include(rec => rec.Operator)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public List<StopViewModel> GetFilteredList(StopBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            return context.Stops
            .Include(rec => rec.Tour)
            .Include(rec => rec.Operator)
            .Where(rec => rec.Id == model.Id)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<StopViewModel> GetFullList()
        {
            using var context = new TravelAgencyDatabase();
            return context.Stops
            .Include(rec => rec.Tour)
            .Include(rec => rec.Operator)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(StopBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Stops.Add(CreateModel(model, new Stop()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(StopBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Stops.FirstOrDefault(rec => rec.Id ==
                model.Id);
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
        private static Stop CreateModel(StopBindingModel model, Stop stop)
        {
            stop.TourId = (int)model.TourId;
            stop.OperatorLogin = model.OperatorLogin;
            stop.CheckinDateStop = model.CheckinDateStop;
            stop.DateofDepatureStop = model.DateofDepatureStop;
            return stop;
        }
        private static StopViewModel CreateModel(Stop stop)
        {
            return new StopViewModel
            {
                Id = stop.Id,
                TourId = stop.TourId,
                TourName = stop.Tour.TourName,
                CheckinDateStop = stop.CheckinDateStop,
                DateofDepatureStop = stop.DateofDepatureStop
            };
        }
    }
}
