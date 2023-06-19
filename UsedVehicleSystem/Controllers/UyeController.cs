using Microsoft.AspNetCore.Mvc;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Controllers
{
	public class UyeController : Controller
	{
        IUyeYonetimi _uyeYonetimi;

        public UyeController(IUyeYonetimi uyeYonetimi)
        {
            _uyeYonetimi = uyeYonetimi;
        }

        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GirisYap(string Eposta, string Sifre)
        {
            if(Eposta == null || Sifre == null) 
            {
                ViewBag.Hata = "Giriş alanları bos birakilamaz.";
                return View("Giris");
            }

            Uye girisYapilan = _uyeYonetimi.GirisYap(Eposta, Sifre);
            if(girisYapilan == null)
            {
                ViewBag.Hata = "Giriş alanları yanlış.";
                return View("Giris");
            }
            else
            {
                HttpContext.Session.SetString("SessionID", girisYapilan.ID.ToString());
                return RedirectToAction("Profil");
            }
        }

        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KayitOl(Uye kayitlanacakUye, string uyelikTuru, string? telefonNumarasi = null)
        {
            if(kayitlanacakUye.ad == null || kayitlanacakUye.soyad == null || kayitlanacakUye.eposta == null || kayitlanacakUye.sifre == null  || uyelikTuru == null)
            {
                ViewBag.Hata = "Boş alan bırakamazsınız. ";
                return View("Kayit");
            }
            if (_uyeYonetimi.UyeVarMi(kayitlanacakUye.eposta))
            {
                ViewBag.Hata = "Eposta hesabı önceden kullanılmış mevcut.";
                return View("Kayit");
            }

            bool musteriMi = uyelikTuru.ToUpperInvariant().ToString() == ("1").ToUpperInvariant() ? true : false;

            Uye kayit = null;

            if (musteriMi)
            {
                kayit = _uyeYonetimi.UyeOl(kayitlanacakUye);
            }
            else
            {
                string telNo;
                if (telefonNumarasi != null)
                    telNo = telefonNumarasi;
                else
                    telNo = "1111111111";

                kayit = _uyeYonetimi.UyeOl(kayitlanacakUye, telNo);
            }

            HttpContext.Session.SetString("SessionID", kayit.ID.ToString());
            return RedirectToAction("Profil");
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Home()
        {
            if(HttpContext.Session.GetString("SessionID") == null)
            {
                return View("Giris");
            }

            Uye gonderilecekUye = _uyeYonetimi.UyeGetir(int.Parse(HttpContext.Session.GetString("SessionID")));

            if(gonderilecekUye == null)
            {
                HttpContext.Session.Remove("SessionID");
                ViewBag.Hata = "Giriş bilgileri hatalı. Lütfen tekrar deneyin.";
                return View("Giris");
            }

            ViewBag.Uye = gonderilecekUye;
            ViewBag.AracSaticisiMi = !_uyeYonetimi.MusteriMi(int.Parse(HttpContext.Session.GetString("SessionID")));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("SessionID");
            return RedirectToAction("Home");
        }

        public IActionResult Profil()
        {
            Uye uye;
            if (HttpContext.Session.GetString("SessionID") != null)
            {
                uye = _uyeYonetimi.UyeGetir(int.Parse(HttpContext.Session.GetString("SessionID")));
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(uye);
        }
    }
}
