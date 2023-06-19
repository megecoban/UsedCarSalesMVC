using Microsoft.AspNetCore.Mvc;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Services;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Facade;

namespace UsedVehicleSystem.Controllers
{
	public class IlanController : Controller
	{
        IFacade _facade;
        IIlanYonetimi _ilanYonetimi;

        public IlanController(IIlanYonetimi ilanYonetimi, IFacade facade)
		{
			_ilanYonetimi = ilanYonetimi;
            _facade = facade;
        }

        private AracSaticisi AktifAracSaticisiniGetir()
        {
            if (HttpContext.Session.GetString("SessionID") == null)
            {
                return null;
            }
            else
            {
                int ID = int.Parse(HttpContext.Session.GetString("SessionID"));
                return _facade.AracSaticisiGetir(ID);
            }
        }

        public IActionResult IlanEkle()
		{
            AracSaticisi aracSaticisi = AktifAracSaticisiniGetir();

            if (aracSaticisi == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Markalar = _facade.AracTumMarkaGetir();
            ViewBag.Modeller = _facade.AracTumModelGetir();
            ViewBag.AracSaticisi = aracSaticisi;

            return View();
		}

		[HttpPost]
		public IActionResult IlanEkle(Ilan ilan, int kilometre, string aracMarkasiID, string aracModelID, string? AracDonanimlari = "")
        {
            AracSaticisi aracSaticisi = AktifAracSaticisiniGetir();
            if (aracSaticisi == null)
                return RedirectToAction("Index", "Home");

            ilan.aracSaticisiID = aracSaticisi.ID;
            ilan.aracSaticisi = aracSaticisi;

            Arac ilandakiArac = new Arac();
            ilandakiArac.kilometre = kilometre;
            ilandakiArac.aracMarkasiID = int.Parse(aracMarkasiID);
            ilandakiArac.aracMarkasi = _facade.AracMarkaGetir(int.Parse(aracMarkasiID));
            ilandakiArac.aracModeliID = int.Parse(aracModelID);
            ilandakiArac.aracModeli = _facade.AracModelGetir(int.Parse(aracModelID));

            //ilandaki aracı kaydet
            _facade.AracEkle(ilandakiArac);
            ViewBag.AracSaticisi = aracSaticisi;
            ViewBag.Ilan = _facade.IlanEkle(ilan, ilandakiArac, aracSaticisi, AracDonanimlari); ;
            return RedirectToAction("Ilanlarim");
		}

        [HttpPost]
        public IActionResult IlanGuncelle(Ilan ilan, int kilometre, string aracMarkasiID, string aracModelID)
        {
            AracSaticisi aracSaticisi = AktifAracSaticisiniGetir();
            if (aracSaticisi == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _ilanYonetimi.IlanGuncelle(ilan.ID, ilan);
                return RedirectToAction("Ilanlarim");
            }
        }

		public IActionResult Ilanlar() 
        {
            List<Ilan> ilanlar = _facade.TumIlanlariGetir();

            if(_facade.AracTumMarkaGetir() != null)
                ViewBag.Markalar = _facade.AracTumMarkaGetir();
            else
                ViewBag.Markalar = new List<AracMarkasi>();

            if (_facade.AracTumModelGetir() != null)
                ViewBag.Modeller = _facade.AracTumModelGetir();
            else
                ViewBag.Modeller = new List<AracMarkasi>();

            return View(ilanlar);
        }

        public IActionResult Ilanlarim()
        {
            AracSaticisi aracSaticisi = AktifAracSaticisiniGetir();
            if (aracSaticisi == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<Ilan> ilanlarim = _facade.IlanlariListele(aracSaticisi);
                return View(ilanlarim);
            }
        }
		
		public IActionResult IlanSil(int ID)
        {
            AracSaticisi aracSaticisi = AktifAracSaticisiniGetir();
            if (aracSaticisi == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _facade.AracSil(_ilanYonetimi.IlanGetir(ID).ilandakiAracID);
                _ilanYonetimi.IlanSil(ID);
                return RedirectToAction("Ilanlarim");
            }
		}

        public IActionResult IlanArama(Ilan ilan, string? Sanziman, string? AracTuru, string? UretimYili, string? YakitTuru, string? MotorTuru, string? Marka, string? Model)
        {
            ilan.ilandakiArac = new Arac
            {
                aracMarkasi = new AracMarkasi { ID = int.Parse(Marka ?? "0") },
                aracModeli = new AracModeli
                {
                    ID = int.Parse(Model ?? "0"),
                    sanziman = Sanziman ?? "",
                    aracTuru = AracTuru ?? "",
                    uretimYili = int.Parse(UretimYili ?? "0"),
                    yakitTuru = YakitTuru ?? "",
                    motorTuru = MotorTuru ?? ""
                }
            };

            List<Ilan> ilanlar = _ilanYonetimi.IlanAra(ilan);


            ViewBag.Filtre = ilan;
            ViewBag.Ilanlar = ilanlar;

            return View();
        }

		public IActionResult IlanGoruntule(int ID)
        {
            ViewBag.CanIEdit = false;
            ViewBag.Uye = _facade.UyeGetir(int.Parse(HttpContext.Session.GetString("SessionID")));

            AracSaticisi aracSaticisi = AktifAracSaticisiniGetir();

            Ilan ilan = _ilanYonetimi.IlanGetir(ID);

            List<Yorum> aracSaticisinaYorumlar = _facade.TumYorumlariGetir(ilan.aracSaticisiID);
            ViewBag.SaticiyaYapilanYorumar = aracSaticisinaYorumlar;
            if (ilan == null)
                return RedirectToAction("Index", "Home");
            if (aracSaticisi != null)
            {
                ViewBag.CanIEdit = true;
            }

            if (_facade.AracTumMarkaGetir() != null)
                ViewBag.Markalar = _facade.AracTumMarkaGetir();
            else
                ViewBag.Markalar = new List<AracMarkasi>();

            if (_facade.AracTumModelGetir() != null)
                ViewBag.Modeller = _facade.AracTumModelGetir();
            else
                ViewBag.Modeller = new List<AracModeli>();

            return View(ilan);
		}

        [HttpPost]
        public IActionResult YorumYap(Yorum yorum)
        {
            _facade.YorumEkle(yorum.AracSaticisiID, yorum.yorumIcerigi, yorum.musteriAdi);
            return RedirectToAction("Ilanlar");
        }

        public IActionResult IlanKarsilastir(int ilanID1, int ilanID2)
        {
            Ilan ilan1 = _facade.IlanGetir(ilanID1);
            Ilan ilan2 = _facade.IlanGetir(ilanID2);

            ViewBag.Ilan1 = ilan1;
            ViewBag.Ilan2 = ilan2;

            return View();
		}

	}
}
