using UsedVehicleSystem.Facade;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
    public interface ISistemYonetimi
    {
        // TEMEL
        public SistemYoneticisi GirisYap(string takmaAd, string sifre);

        // DIGER
        public SistemYoneticisi YoneticiGetir(int ID);
        public List<Ilan> TumIlanlariGetir();
        public Ilan IlanGetir(int ID);
        public bool IlanOnayla(int ID);
        public Arac AracGetir(int ID);
        public bool AracMarkaEkle(AracMarkasi marka);
        public bool AracModelEkle(AracModeli model);
        public List<Arac> TumAraclariGetir();
        public List<AracModeli> TumAracModelleriniGetir();
        public List<AracMarkasi> TumAracMarkalariniGetir();
    }

    public class SistemYonetimi : ISistemYonetimi
    {
        ISistemYoneticisiRepository sistemYoneticisiRepo;
        IFacade facade;

        public SistemYonetimi(IFacade Facade, ISistemYoneticisiRepository SistemYoneticisiRepository)
        {
            sistemYoneticisiRepo = SistemYoneticisiRepository;
            facade = Facade;
        }

        #region SY_Temel
        public SistemYoneticisi GirisYap(string takmaAd, string sifre)
        {
            return sistemYoneticisiRepo.GirisYap(takmaAd, sifre);
        }
        public bool AracMarkaEkle(AracMarkasi marka)
        {
            return facade.AracMarkaEkle(marka);
        }

        public bool AracModelEkle(AracModeli model)
        {
            return facade.AracModelEkle(model);
        }

        public bool IlanOnayla(int ID)
        {
            return facade.IlanOnayla(ID);
        }

        #endregion



        #region SY_Dondur

        public Arac AracGetir(int ID)
        {
            return facade.AracGetir(ID);
        }

        public List<Arac> TumAraclariGetir()
        {
            return facade.AraclariGetir();
        }

        public List<AracModeli> TumAracModelleriniGetir()
        {
            return facade.AracTumModelGetir();
        }

        public List<AracMarkasi> TumAracMarkalariniGetir()
        {
            return facade.AracTumMarkaGetir();
        }

        public SistemYoneticisi YoneticiGetir(int ID)
        {
            return sistemYoneticisiRepo.YoneticiGetir(ID);
        }

        public Ilan IlanGetir(int ID)
        {
            return facade.IlanGetir(ID);
        }

        public List<Ilan> TumIlanlariGetir()
        {
            return facade.TumIlanlariGetir();
        }
        #endregion
    }
}
