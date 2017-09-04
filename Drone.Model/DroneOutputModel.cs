/// <summary>
/// Model has the final coordinates and of the drone after the exploration
/// instructions are applied.
/// </summary>
namespace Drone.Model
{
    public class DroneOutputModel
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string Heading { get; set; }
    }
}
