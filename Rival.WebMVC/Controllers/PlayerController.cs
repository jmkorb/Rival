using Microsoft.AspNet.Identity;
using Rival.Models.Players;
using Rival.Services.PlayerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rival.WebMVC.Controllers
{
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerService(userId);
            var model = service.GetPlayers();

            return View(model);
        }
        // GET Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePlayerService();

            if (service.CreatePlayer(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        private PlayerService CreatePlayerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerService(userId);
            return service;
        }
    }
}