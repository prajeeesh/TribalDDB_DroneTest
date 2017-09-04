using Autofac;
using Drone.Services.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Drone.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Drone.ServicesTests
{
    [TestClass()]
    public class DroneServiceTests :DroneServiceTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void GetDroneCoordinatesFromApiTest()
        {
            int recordCount = 0;
            string droneNavigationInput = "5 5\r\n1 2 N\r\nLMLMLMLMM";
            DroneOutputModel expectedResult = new DroneOutputModel()
            { PositionX = 1, PositionY = 3, Heading = "N" };

            using (var container = builder.Build())
            {
                var droneNavigationService = container.Resolve<IDroneNavigationService>();
                var model = droneNavigationService.GetDroneCoordinatesFromApi(droneNavigationInput);
                model.Wait();
                recordCount = model.Result.Count;
                if (model.Result != null && model.Result.Count > 0)
                {
                    var result = model.Result[0];
                    Assert.AreEqual(expectedResult.PositionX, result.PositionX);
                    Assert.AreEqual(expectedResult.PositionY, result.PositionY);
                    Assert.AreEqual(expectedResult.Heading, result.Heading);
                }
            }
        }
    }
}
