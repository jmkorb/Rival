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
        // GET: Note
        public ActionResult Index()
        {
            var service = CreateCourtService();
            var model = service.GetCourts();

            return View(model);
        }

        // GET: Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourtCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCourtService();

            if (service.CreateCourt(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCourtService();
            var model = svc.GetCourtById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var service = CreateCourtService();
            var detail = service.GetCourtById(id);
            var model =
                new CourtEdit
                {
                    CourtId = detail.CourtId,
                    Location = detail.Location,
                    Condition = detail.Condition
                };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
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

            ModelState.AddModelError("", "The could not be updated.");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCourtService();
            var model = svc.GetCourtById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourt(int id)
        {
            var service = CreateCourtService();

            service.DeleteCourt(id);

            TempData["SaveResult"] = "Your court was deleted";

            return RedirectToAction("Index");
        }

        private CourtService CreateCourtService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CourtService(userId);
            return service;
        }
    }
}