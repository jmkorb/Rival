using Microsoft.AspNet.Identity;
using Rival.Data;
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
        private ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly IPlayerService _service;

        public PlayerController(IPlayerService service)
        {
            _service = service;
        }

        // GET: Player
        public ActionResult Index()
        {
            var model = _service.GetPlayers();

            return View(model);
        }
        // GET Player/Create
        public ActionResult Create()
        {
            //In case a user navigated to Player/Create, if they already have on they cannot create another
            var userId = Guid.Parse(User.Identity.GetUserId());
            if (ctx.Players.Where(e => e.UserId == userId).Count() == 1)
            {
                ModelState.AddModelError("", "Your player is already created");
                return RedirectToAction("Edit");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerCreate model)
        {

            if (!ModelState.IsValid) return View(model);

            var userId = Guid.Parse(User.Identity.GetUserId());

            model.UserId = User.Identity.GetUserId();
            // In case a user navigated to Player/Create, if they already have on they cannot create another
            if (ctx.Players.Where(e => e.UserId == userId).Count() == 1)
            {
                ModelState.AddModelError("", "Your player is already created");
                return RedirectToAction("Edit");
            }

            if (_service.CreatePlayer(model))
            {
                TempData["SaveResult"] = "Your player was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player could not be created.");

            return View(model);
        }

        // GET Edit
        public ActionResult Edit()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var currentPlayer = ctx.Players.Single(e => e.UserId == userId);
            var detail = _service.GetPlayerById(currentPlayer.Id);

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = Guid.Parse(User.Identity.GetUserId());
            var currentPlayer = ctx.Players.Single(e => e.UserId == userId);

            if (model.PlayerId != currentPlayer.Id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();

            if (_service.EditPlayer(model))
            {
                TempData["SaveResult"] = "Your player was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your player could not be updated.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = _service.GetPlayerById(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var model = _service.GetPlayerById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlayer(int id)
        {
            _service.DeletePlayer(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Your player was deleted";

            return RedirectToAction("Index");
        }
    }
}