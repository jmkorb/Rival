using Microsoft.AspNet.Identity;
using Rival.Data;
using Rival.Services.PlayerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rival.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly IPlayerService _playerService;

        public ActionResult Dashboard()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var userPlayer = ctx.Players.Single(e => e.UserId == userId);

            var detailModel = _playerService.GetPlayerById(userPlayer.Id);

            return View(detailModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}