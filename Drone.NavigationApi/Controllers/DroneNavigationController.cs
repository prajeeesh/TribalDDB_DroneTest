using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Drone.Model;
namespace Drone.NavigationApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DroneNavigationController : ApiController
    {
        // GET: api/values
        private int GridTopX = 0;
        private int GridTopY = 0;
        /// <summary>
        /// Api mothod that is exposed for the consumption.
        /// </summary>
        /// <param name="droneControlModel">Input instructions of the initial positions of the drone
        /// and the Gird coordinates</param>
        /// <returns>List of final coordinates based on the input provided</returns>
        [HttpGet]
        [Route("api/DroneNavigation/{droneControlModel}")]
        public List<Drone.Model.DroneOutputModel> GetDroneCoordinates([FromBody] DroneControlModel droneControlModel)
        {
            return ExecuteDroneNavigation(droneControlModel);
        }

        /// <summary>
        /// Mthod to move the x,y coordinates of the drone
        /// </summary>
        /// <param name="inputLocation"></param>
        /// <returns></returns>
        private DroneInputModel Move(DroneInputModel inputLocation)
        {

            string direction = inputLocation.Position.Heading;
            string move_direction = "";
            bool can_move = true;
            var currentPositionX = inputLocation.Position.PositionX;
            var currentPositionY = inputLocation.Position.PositionY;

            if (direction == "E" || direction == "W")
                move_direction = "X";
            else if (direction == "S" || direction == "N")
                move_direction = "Y";

            if (direction == "E" || direction == "N")
            {
                if (direction == "E")
                    if ((currentPositionX < 0 || (currentPositionX) > GridTopX))
                        can_move = false;

                if (direction == "N")
                    if (currentPositionY < 0 || (currentPositionY) > GridTopY)
                        can_move = false;

                if (can_move)
                {
                    if (move_direction == "X")
                        inputLocation.Position.PositionX += 1;
                    else
                        inputLocation.Position.PositionY += 1;
                }
            }
            else if (direction == "W" || direction == "S")
            {
                if (move_direction == "X" && currentPositionX > 0)
                    inputLocation.Position.PositionX -= 1;
                else if (move_direction == "Y" && currentPositionY > 0)
                    inputLocation.Position.PositionY -= 1;
            }

            return inputLocation;
        }

        /// <summary>
        /// Processes the navigation instructions
        /// </summary>
        /// <param name="droneInputCommand"></param>
        /// <returns></returns>
        private DroneOutputModel ExecuteCommand(DroneInputModel droneInputCommand)
        {
            var commands = droneInputCommand.Instructions.ToCharArray();
            DroneOutputModel finalCoodinates = new DroneOutputModel();
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'L':
                        switch (droneInputCommand.Position.Heading)
                        {
                            case "E":
                                droneInputCommand.Position.Heading = "N";
                                break;
                            case "N":
                                droneInputCommand.Position.Heading = "W";
                                break;
                            case "W":
                                droneInputCommand.Position.Heading = "S";
                                break;
                            case "S":
                                droneInputCommand.Position.Heading = "E";
                                break;
                        }
                        break;
                    case 'R':
                        switch (droneInputCommand.Position.Heading)
                        {
                            case "E":
                                droneInputCommand.Position.Heading = "S";
                                break;
                            case "N":
                                droneInputCommand.Position.Heading = "E";
                                break;
                            case "W":
                                droneInputCommand.Position.Heading = "N";
                                break;
                            case "S":
                                droneInputCommand.Position.Heading = "W";
                                break;
                        }
                        break;
                    case 'M':
                        droneInputCommand = Move(droneInputCommand);
                        break;
                }
            }

            finalCoodinates.PositionX = droneInputCommand.Position.PositionX;
            finalCoodinates.PositionY = droneInputCommand.Position.PositionY;
            finalCoodinates.Heading = droneInputCommand.Position.Heading;

            return finalCoodinates;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneControlModel"></param>
        /// <returns></returns>
        public List<DroneOutputModel> ExecuteDroneNavigation(DroneControlModel droneControlModel)
        {
            GridTopX = droneControlModel.GridTopX;
            GridTopY = droneControlModel.GridTopY;

            List<DroneOutputModel> FinalDroneCoordinates = new List<DroneOutputModel>();

            foreach (DroneInputModel input in droneControlModel.DroneInputs)
            {
                FinalDroneCoordinates.Add(ExecuteCommand(input));
            }
            return FinalDroneCoordinates;
        }
    }
}

