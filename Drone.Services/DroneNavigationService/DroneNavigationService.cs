using Drone.Model;
using Drone.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Drone.Services
{
    public class DroneNavigationService: IDroneNavigationService
    {
        private readonly ISettingsService settingsService;

        public DroneNavigationService(ISettingsService _settingsService)
        {
            settingsService = _settingsService;
        }
       
        public async Task<List<Drone.Model.DroneOutputModel>> GetDroneCoordinatesFromApi(string droneInputModel)
        {
            string uri = settingsService.GetWebApiPath();
            string[] stringSeparators = new string[] { "\r\n" };
            var commands = droneInputModel.TrimEnd().Split(stringSeparators, StringSplitOptions.None);
            List<DroneInputModel> inputModels = new List<DroneInputModel>();
            var gridDimensions = commands[0].Split(' ');  // First line sets the Battlefield dimensoins

            DroneControlModel droneControlModel = new DroneControlModel();
            droneControlModel.GridTopX = int.Parse(gridDimensions[0]);
            droneControlModel.GridTopY = int.Parse(gridDimensions[1]);

            for (int i = 1; i < commands.Length; i += 2)
            {
                DroneInputModel model = new DroneInputModel();
                var postion = commands[i].Split(' ');
                model.Position.PositionX = int.Parse(postion[0]);
                model.Position.PositionY = int.Parse(postion[1]);
                model.Position.Heading = postion[2];
                model.Instructions = commands[i + 1];

                droneControlModel.DroneInputs.Add(model);
            }
            string inputJson = JsonConvert.SerializeObject(droneControlModel);
            List<DroneOutputModel> FinalDroneCoordinates = new List<DroneOutputModel>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(droneControlModel), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(uri, content);
                    FinalDroneCoordinates = await response.Content.ReadAsAsync<List<Drone.Model.DroneOutputModel>>();
                    return FinalDroneCoordinates;
                }
            }
            catch (Exception ex)
            {
                //Log Exception 
                Console.WriteLine(ex.Message);
                throw new Exception("Error occured while accessing the service API");
            }

        }
    }
}
