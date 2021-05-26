using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCityMyPlaces.Data;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Controllers

{
    [Authorize]
    public class FamilyController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<FamilyController> _logger;

        public FamilyController(IUserRepository userRepository,
            ILocationRepository locationRepository,
            ILogger<FamilyController> logger)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult AddFamilyMember(FamilyViewModel model)
        {
            if (!ModelState.IsValid || User.Identity?.Name == null)
                return RedirectToAction("Error");
            
            var currentEmail = User.Identity.Name;
            _userRepository.AddRelationship(currentEmail, model.Email);
            return RedirectToAction("Family");
        }

        public IActionResult Accept(string id)
        {
            if (User.Identity?.Name == null || string.IsNullOrEmpty(id))
                return RedirectToAction("Error");
            
            var currentEmail = User.Identity.Name;
            _userRepository.AddRelationship(currentEmail, id);
            return RedirectToAction("Family");
        }
        
        public IActionResult Cancel(string id)
        {
            if (User.Identity?.Name == null || string.IsNullOrEmpty(id))
                return RedirectToAction("Error");
            
            var currentEmail = User.Identity.Name;
            _userRepository.RemoveRelationship(currentEmail, id);
            return RedirectToAction("Family");
        }

        public IActionResult Family()
        {
            if (User.Identity?.Name == null)
                return RedirectToAction("Error");
            
            return View(_userRepository.GetByEmail(User.Identity.Name));
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
