﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.Model;
namespace Drone.Services.Interface
{
    public interface IDroneNavigationService
    {
        Task<List<Drone.Model.DroneOutputModel>> GetDroneCoordinatesFromApi(string droneControlJsonModel);
    }
}
