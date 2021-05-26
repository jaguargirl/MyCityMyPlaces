using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCityMyPlaces.Data;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace MyCityMyPlaces.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(IUserRepository userRepository,
            ILocationRepository locationRepository,
            ILogger<LocationsController> logger)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        [HttpPost]
        [HttpGet]
        public IActionResult AddNewLocation(decimal lon, decimal lat, string comment, string name, string userEmail)
        {
            _locationRepository.AddLocation(lon, lat, comment, name, false, userEmail);
            return RedirectToAction("Locations");
        }

        public IActionResult ChangeShared(int locationId, bool shared)
        {
            Location location = _locationRepository.GetLocationById(locationId);
            _locationRepository.EditLocation(location.LocationId, location.Comment, location.Name, shared);
            return RedirectToAction("Locations");
        }

        [HttpPost]
        public IActionResult EditLocation(LocationViewModel location)
        {
            _locationRepository.EditLocation(location.LocationId, location.Comment, location.Name, location.Shared);
            return RedirectToAction("Locations");
        }

        [HttpPost]
        public JsonResult ShowLocation(LocationViewModel location)
        {
            return Json(true);
        }
        
        public IActionResult RemoveLocation(int locationId)
        {
            _locationRepository.RemoveLocation(locationId);
            return RedirectToAction("Locations");
        }

        public IActionResult Locations()
        {
            if (User.Identity?.Name == null)
                return RedirectToAction("Error");
            
            return View(_userRepository.GetByEmail(User.Identity.Name));
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
