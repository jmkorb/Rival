using Microsoft.AspNet.Identity;
using Rival.Models.Courts;
using Rival.Services.CourtServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rival.WebMVC.Controllers
{
    [Authorize]
    public class CourtController : Controller
    {
        // GET Court/Index
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CourtService();
            var model = service.GetCourts();

            return View(model);
        }
        // GET Court/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourtCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCourtService();

            if (service.CreateCourt(model))
            {
                TempData["SaveResult"] = "You created a new court.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "A new court could not be created.");

            return View(model);
        }

        // GET Edit
        public ActionResult Edit(int id)
        {
            var service = CreateCourtService();
            var detail = service.GetCourtById(id);
            var model = new CourtEdit
            {
                CourtId = detail.CourtId,
                Location = detail.Location,
                Condition = detail.Condition
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CourtEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CourtId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCourtService();

            if (service.EditCourt(model))
            {
                TempData["SaveResult"] = "The court was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The court could not be updated.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateCourtService();
            var model = service.GetCourtById(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateCourtService();
            var model = service.GetCourtById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourt(int id)
        {
            var service = CreateCourtService();

            service.DeleteCourt(id);

            TempData["SaveResult"] = "The was deleted";

            return RedirectToAction("Index");
        }

        private CourtService CreateCourtService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CourtService();
            return service;
        }
    }
}