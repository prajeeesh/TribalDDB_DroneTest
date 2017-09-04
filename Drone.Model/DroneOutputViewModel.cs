using System.Collections.Generic;
/// <summary>
/// View model used for displaying the output to UI
/// </summary>
namespace Drone.Model
{
    public class DroneOutputViewModel
    {
        public DroneOutputViewModel()
        {
            DroneOutputs = new List<DroneOutputModel>();
        }

        public List<DroneOutputModel> DroneOutputs { get; set; }
        public string ErrorMessage { get; set; }
    }
}
