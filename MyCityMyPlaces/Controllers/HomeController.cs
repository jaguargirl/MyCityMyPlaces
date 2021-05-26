using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUserRepository userRepository,
            ILocationRepository locationRepository,
            ILogger<HomeController> logger)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity?.Name == null)
                return View();
            
            var email = User.Identity.Name.Trim().ToLower();
            var user = _userRepository.GetByEmail(email);
            if (user != null)
                return View();
            var name = ((ClaimsIdentity) User.Identity).Claims.First(c => c.Type == "name").Value;
            user = new User()
            {
                Email = User.Identity.Name,
                Name = name,
            };
            _userRepository.Add(user);

            return View();
        }

        public IActionResult Privacy()
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
