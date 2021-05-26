using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IUserRepository userRepository,
            ILocationRepository locationRepository,
            ILogger<StatisticsController> logger)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }
        
        public IActionResult Statistics()
        {
            return View(new StatisticsViewModel()
            {
                Users = _userRepository.GetAll(),
                CurrentUser = _userRepository.GetByEmail(User.Identity?.Name)
            });
        }
        
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}