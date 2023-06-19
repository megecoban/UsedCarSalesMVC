using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Controllers
{
	public class HomeController : Controller
	{
		private IUyeYonetimi _uyeYonetimi;

        public HomeController(IUyeYonetimi uyeYonetimi)
		{
            _uyeYonetimi = uyeYonetimi;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionID") != null)
            {
                int sessionID = int.Parse(HttpContext.Session.GetString("SessionID"));
                Uye uye = _uyeYonetimi.UyeGetir(sessionID);
                ViewBag.Uye = uye;
                ViewBag.MusteriMi = _uyeYonetimi.MusteriMi(uye.ID); 
            }
            return View();
        }

        public IActionResult Home()
        {
            return RedirectToAction("Index");
        }
	}
}