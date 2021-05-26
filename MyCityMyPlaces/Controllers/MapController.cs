using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Controllers
{
    [Authorize]
    public class MapController : Controller

    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<MapController> _logger;

        public MapController(IUserRepository userRepository,
            ILocationRepository locationRepository,
            ILogger<MapController> logger)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddLocation(MapViewModel mapmodel)
        {

            if (!ModelState.IsValid || User.Identity?.Name == null)
                return RedirectToAction("Error");
            
            var currentEmail = User.Identity.Name;

            if (mapmodel.Comment != null && mapmodel.Name != null){
                _locationRepository.AddLocation(mapmodel.Lon, mapmodel.Lat, mapmodel.Comment, mapmodel.Name,
                    mapmodel.Shared, currentEmail);
            }
            else
            {
                _locationRepository.AddLocation(mapmodel.Lon, mapmodel.Lat, mapmodel.Shared, currentEmail);
            }
            return RedirectToAction("Map");
        }

        [HttpGet]
        public IActionResult GetLocations(MapViewModel mapmodel)
        {
            if (!ModelState.IsValid || User.Identity?.Name == null)
                return RedirectToAction("Error");
            
            var currentEmail = User.Identity.Name;
            var Locs = _userRepository.GetUserLocations(currentEmail);
            var data = "";
            foreach (var loc in Locs)
            {
                data += loc.Lat + "," + loc.Lon + ";";
            }
            TempData["coord"] = data;
            return RedirectToAction("Map");
        }
        public IActionResult Map()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
