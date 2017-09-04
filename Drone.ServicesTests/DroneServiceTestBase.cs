using Autofac;
using Drone.Services;
using Drone.Services.Interface;

namespace Drone.ServicesTests
{
    public class DroneServiceTestBase
    {
        protected ContainerBuilder builder;
        public DroneServiceTestBase()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<DroneNavigationService>().As<IDroneNavigationService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingsService>().As<ISettingsService>().InstancePerLifetimeScope();
        }
    }
    
   
}
