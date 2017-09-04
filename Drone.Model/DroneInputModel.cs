namespace Drone.Model
{
    /// <summary>
    /// Model holds input information pertaining to the drone that has been
    /// deployed.This includes the position and series of instructions.
    /// </summary>
    public class DroneInputModel
    {
        public DroneInputModel()
        {
            Position = new DroneOutputModel();
        }

       public DroneOutputModel Position { get; set; }
       public string Instructions { get; set; }

    }
}
