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
    public class MatchController : Controller
    {

        // GET: Match

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MatchService(userId);
            var model = service.GetMatches();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchDetail model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMatchService();

            if (service.CreateMatch(model))
            {
                TempData["SaveResult"] = "Your match was logged.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Match could not be created.");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateMatchService();
            var detail = service.GetMatchById(id);
            var model = new MatchDetail
            {
                MatchId = detail.MatchId,
                PlayerOne = detail.PlayerOne,
                PlayerTwo = detail.PlayerTwo,
                Date = detail.Date,
                Court = detail.Court,
                FinalScore = detail.FinalScore,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MatchDetail model)
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

        public ActionResult Details(int id)
        {
            var service = CreateMatchService();
            var model = service.GetMatchById(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateMatchService();
            var model = service.GetMatchById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlayer(int id)
        {
            var service = CreateMatchService();

            service.DeleteMatch(id);

            TempData["SaveResult"] = "Your match was deleted";

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