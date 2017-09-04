using System.Collections.Generic;

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
