using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyDatabaseImplements.Implements;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.OfficePackage;
using TravelAgencyBusinessLogic.OfficePackage.Implements;

namespace TravelAgencyOperatorView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WindowAuthorization authorizationWindow = Container.Resolve<WindowAuthorization>();
            authorizationWindow.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IGuideStorage, GuideStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPlaceStorage, PlaceStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStopStorage, StopStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITripStorage, TripStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExcursionStorage, ExcursionStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourStorage, TourStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOperatorStorage, OperatorStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGuideLogic, GuideLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPlaceLogic, PlaceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStopLogic, StopLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITripLogic, TripLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExcursionLogic, ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourLogic, TourLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOperatorLogic, OperatorLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
