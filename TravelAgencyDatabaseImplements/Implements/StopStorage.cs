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
        public List<StopViewModel> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Stops.Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).Select(CreateModel).ToList();
            }
        }
        public List<StopViewModel> GetFilteredList(StopBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                return context.Stops.Where(rec => rec.TourId == model.StopTour && rec.OperatorLogin == OperatorStorage.AutorizedOperator).Select(CreateModel).ToList();
            }
        }
        public StopViewModel GetElement(StopBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var stop = context.Stops.Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).FirstOrDefault(rec => rec.Id == model.Id);
                return stop != null ? CreateModel(stop) : null;
            }
        }
        public void Insert(StopBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Stop stop = new Stop()
                        {
                            StopName = model.StopName,
                            CheckinDateStop = model.CheckinDateStop,
                            DateofDepatureStop=model.DateofDepatureStop,
                            OperatorLogin = OperatorStorage.AutorizedOperator,
                            TourId = model.StopTour,
                            Tour = context.Tours.FirstOrDefault(rec => rec.Id == model.StopTour)
                        };
                        context.Stops.Add(stop);
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
        public void Update(StopBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Stops.Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.StopName = model.StopName;
                        element.CheckinDateStop = model.CheckinDateStop;
                        element.DateofDepatureStop = model.DateofDepatureStop;
                        element.TourId = model.StopTour;
                        element.Tour = context.Tours.FirstOrDefault(rec => rec.Id == model.StopTour);
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
        public void Delete(StopBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Stop element = context.Stops.Where(rec => rec.OperatorLogin == OperatorStorage.AutorizedOperator).FirstOrDefault(rec => rec.Id == model.Id);
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
        }
        private static StopViewModel CreateModel(Stop stop)
        {
            return new StopViewModel
            {
                Id = stop.Id,
                StopName = stop.StopName,
                CheckinDateStop = stop.CheckinDateStop,
                DateofDepatureStop=stop.DateofDepatureStop,
                OperatorLogin = stop.OperatorLogin,
                StopTour = stop.TourId
            };
        }
    }
}
