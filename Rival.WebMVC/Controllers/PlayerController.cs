﻿using Microsoft.AspNet.Identity;
using Rival.Models.PlayerModels;
using Rival.Models.Players;
using Rival.Services.PlayerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rival.WebMVC.Controllers
{
    [Authorize]
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePlayerService();

            if (service.CreatePlayer(model))
            {
                TempData["SaveResult"] = "Your player was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player could not be created.");

            return View(model);
        }

        // GET Edit
        public ActionResult Edit(int id)
        {
            var service = CreatePlayerService();
            var detail = service.GetPlayerById(id);
            var model = new PlayerEdit
            {
                PlayerId = detail.PlayerId,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                City = detail.City,
                State = detail.State,
                PreferredSetNumber = detail.PreferredSetNumber,
                Availability = detail.Availability
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreatePlayerService();
            var model = svc.GetPlayerById(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlayerEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.PlayerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePlayerService();

            if (service.EditPlayer(model))
            {
                TempData["SaveResult"] = "Your player was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your player could not be updated.");
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