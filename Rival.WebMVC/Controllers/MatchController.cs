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
        private readonly IMatchService _service;

        public MatchController(IMatchService service)
        {
            _service = service;
        }

        // GET: Match
        public ActionResult Index()
        {
            var model = _service.GetMatches();

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

            model.UserId = User.Identity.GetUserId();

            if (_service.CreateMatch(model))
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
            var model = _service.GetMatchById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var detail = _service.GetMatchById(id);
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

            model.UserId = User.Identity.GetUserId();

            if (_service.EditMatch(model))
            {
                TempData["SaveResult"] = "Your match was updated.";
                return RedirectToAction("Details", new { id = id });
            }

            ModelState.AddModelError("", "Your match could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var model = _service.GetMatchById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMatch(int id)
        {
            _service.DeleteMatch(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }
    }
}