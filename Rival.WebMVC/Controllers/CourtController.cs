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
        private readonly ICourtService _service;

        public CourtController(ICourtService service)
        {
            _service = service;
        }
        // GET: Note
        public ActionResult Index()
        {
            var model = _service.GetCourts();

            return View(model);
        }

        // GET: Createsss
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourtCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_service.CreateCourt(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = _service.GetCourtById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _service.GetCourtById(id);
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

            model.UserId = User.Identity.GetUserId();

            if (_service.EditCourt(model))
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
            var model = _service.GetCourtById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourt(int id)
        {
            _service.DeleteCourt(id);

            TempData["SaveResult"] = "Your court was deleted";

            return RedirectToAction("Index");
        }
    }
}