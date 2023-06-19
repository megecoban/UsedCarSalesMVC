using Microsoft.AspNetCore.Mvc;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Controllers
{
	public class YoneticiController : Controller
    {
        IUyeYonetimi _uyeYonetimi;
        IIlanYonetimi _ilanYonetimi;
        IAracYonetimi _aracYonetimi;

        public YoneticiController(IUyeYonetimi uyeYonetimi, IIlanYonetimi ilanYonetimi, IAracYonetimi aracYonetimi)
        {
            _uyeYonetimi = uyeYonetimi;
            _ilanYonetimi = ilanYonetimi;
            _aracYonetimi = aracYonetimi;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GirisYap(SistemYoneticisi sy)
        {
            return View();
        }

        public IActionResult Ilanlar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IlanOnayla(Ilan ilan)
        {
            return View();
        }

        public IActionResult Araclar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AracMarkaEkle(AracMarkasi marka)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AracModelEkle(AracModeli model)
        {
            return View();
        }

    }
}
