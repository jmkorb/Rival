using Microsoft.AspNet.Identity;
using Rival.Data;
using Rival.Models.Matches;
using Rival.Services.MatchServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rival.WebMVC.Controllers
{
    [Authorize]
    public class MatchController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MatchService(userId);
            var model = service.GetMatches();

            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.PlayerTwoId = new SelectList(ctx.Players, "Id", "FullName");
            ViewBag.CourtId = new SelectList(ctx.Courts, "Id", "Location");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchCreate model)
        {
            

            if (!ModelState.IsValid) return View(model);

            var service = CreateMatchService();

            if (service.CreateMatch(model))
            {
                TempData["SaveResult"] = "Your match was created. Select edit to add winner and final score.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Match could not be created.");

            ViewBag.PlayerTwo = new SelectList(ctx.Players, "Id", "FullName", model.PlayerTwoId);
            ViewBag.Court = new SelectList(ctx.Courts, "Id", "Location", model.CourtId);

            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateMatchService();
            var model = svc.GetMatchById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateMatchService();
            var detail = service.GetMatchById(id);
            var model =
                new MatchEdit
                {
                    MatchId = detail.MatchId,
                    Date = detail.Date,
                    Court = detail.Court,
                    Winner = detail.Winner,
                    FinalScore = detail.FinalScore
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MatchEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MatchId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateMatchService();

            if (service.EditMatch(model))
            {
                TempData["SaveResult"] = "Your match was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your match could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateMatchService();
            var model = svc.GetMatchById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMatch(int id)
        {
            var service = CreateMatchService();

            service.DeleteMatch(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private MatchService CreateMatchService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MatchService(userId);
            return service;
        }
    }
}