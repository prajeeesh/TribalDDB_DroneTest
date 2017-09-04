using Drone.Model;
using Drone.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Drone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDroneNavigationService droneNavigationService;
        public HomeController(IDroneNavigationService _droneNavigationService)
        {
            droneNavigationService = _droneNavigationService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string droneInputModel)
        {
            DroneOutputViewModel finalDroneCoordinates = new DroneOutputViewModel();
            try
            {
                //TODO - Serverside validation
                var errorMessage = ValidateInputValues(droneInputModel);
                                
                if (String.IsNullOrEmpty(errorMessage))
                {
                    finalDroneCoordinates.DroneOutputs = await droneNavigationService.GetDroneCoordinatesFromApi(droneInputModel);
                    
                }
                finalDroneCoordinates.ErrorMessage = errorMessage;                
            }
            catch (Exception ex)
            {
                //Log the error message
                finalDroneCoordinates.ErrorMessage = "Error occured.Please contact Administrator";
            }
            return View(finalDroneCoordinates);
        }
        /// <summary>
        /// TODO Serverside validation
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public string ValidateInputValues(string values)
        {
            var errorMessage = string.Empty;

            return errorMessage;
        }
    }
}