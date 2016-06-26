using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.web
{
    public class AppController : Controller
    {
        private IWorldRepository _context;

        private IMailService _mailService;
        public AppController(IMailService service, IWorldRepository context)
        {
            _mailService = service;
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context.GetAllTrips();
            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid) {
            _mailService.SendMail("test@mail.com", cvm.Email, cvm.Name, cvm.Message);
            return View("Thanks",cvm.Email);
            }
            return View(cvm);   
        }
    }
}
