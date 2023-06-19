using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Facade
{

    public interface IFacade
    {
        // UYE
        public Uye UyeGetir(int ID);
        public AracSaticisi? AracSaticisiGetir(int ID);


        // ARAC
        public Arac AracEkle(Arac arac);
        public void AracSil(int aracID);
        public bool AracDonanimiEkle(AracDonanimi donanim);
        public AracMarkasi AracMarkaGetir(int ID);
        public AracModeli AracModelGetir(int ID);
        public bool AracMarkaEkle(AracMarkasi marka);
        public bool AracModelEkle(AracModeli model);
        public Arac AracGetir(int ID);
        public List<AracMarkasi> AracTumMarkaGetir();
        public List<AracModeli> AracTumModelGetir();
        public List<Arac> AraclariGetir();

        // YORUM
        public void YorumEkle(int saticiID, string yorumIcerigi, string musteriAdi);
        public List<Yorum> TumYorumlariGetir(int ID);

        // ILAN
        public Ilan IlanEkle(Ilan ilan, Arac ilandakiArac, AracSaticisi aracSaticisi, string? aracDonanimlari);
        public Ilan IlanGetir(int ID);
        public List<Ilan> TumIlanlariGetir();
        public List<Ilan> IlanlariListele(AracSaticisi aracSaticisi);
        public bool IlanOnayla(int ID);
    }

    public class Facade : IFacade
    {
        public IIlanYonetimi _ilanYonetimi;
        public IUyeYonetimi _hesapYonetimi;
        public IAracYonetimi _aracYonetimi;
        public IYorumYonetimi _yorumYonetimi;

        public Facade(IIlanYonetimi ilanYonetimi, IUyeYonetimi hesapYonetimi, IAracYonetimi aracYonetimi, IYorumYonetimi yorumYonetimi)
        {
            _ilanYonetimi = ilanYonetimi;
            _hesapYonetimi = hesapYonetimi;
            _aracYonetimi = aracYonetimi;
            _yorumYonetimi = yorumYonetimi;
        }

        public List<Arac> AraclariGetir()
        {
            return _aracYonetimi.TumAraclariGetir();
        }

        public List<AracMarkasi> AracTumMarkaGetir()
        {
            return _aracYonetimi.TumMarkalariGetir();
        }

        public List<AracModeli> AracTumModelGetir()
        {
            return _aracYonetimi.TumModelleriGetir();
        }

        public Arac AracGetir(int ID)
        {
            return _aracYonetimi.AracGetir(ID);
        }


        public bool AracMarkaEkle(AracMarkasi marka)
        {
            return _aracYonetimi.MarkaEkle(marka);
        }

        public bool AracModelEkle(AracModeli model)
        {
            return _aracYonetimi.ModelEkle(model);
        }

        public AracMarkasi AracMarkaGetir(int ID)
        {
            return _aracYonetimi.MarkaGetir(ID);
        }

        public AracModeli AracModelGetir(int ID)
        {
            return _aracYonetimi.ModelGetir(ID);
        }

        public Arac AracEkle(Arac arac)
        {
            return _aracYonetimi.AracEkle(arac);
        }
        public bool AracDonanimiEkle(AracDonanimi donanim)
        {
            return _aracYonetimi.AracDonanimiEkle(donanim);
        }

        public void AracSil(int aracID)
        {
            _aracYonetimi.AracSil(aracID);
        }

        public Uye UyeGetir(int ID)
        {
            return _hesapYonetimi.UyeGetir(ID);
        }
        public AracSaticisi? AracSaticisiGetir(int ID)
        {
            if ((_hesapYonetimi.UyeVarMi(ID) && !_hesapYonetimi.MusteriMi(ID)))
            {
                return _hesapYonetimi.AracSaticisiGetir(ID);
            }
            else
            {
                return null;
            }
        }

        public List<Yorum> TumYorumlariGetir(int ID)
        {
            return _yorumYonetimi.TumYorumlariGetir(ID);
        }

        public void YorumEkle(int saticiID, string yorumIcerigi, string musteriAdi)
        {
            _yorumYonetimi.YorumEkle(saticiID, yorumIcerigi, musteriAdi);
        }

        public Ilan IlanEkle(Ilan ilan, Arac ilandakiArac, AracSaticisi aracSaticisi,string? aracDonanimlari)
        {
            ilan.aracSaticisiID = aracSaticisi.ID;
            ilan.aracSaticisi = aracSaticisi;

            ilan.ilandakiAracID = ilandakiArac.ID;
            ilan.ilandakiArac = ilandakiArac;

            List<string> donanimlar = aracDonanimlari?.Split(",")?.ToList() ?? new List<string>();
            if (donanimlar.Count > 0)
            {
                for (int i = 0; i < donanimlar.Count; i++)
                {
                    string donanim = donanimlar[i];
                    AracDonanimi temp = new AracDonanimi() { donanimAdi = donanimlar[i].Trim(), bagliOlduguAracID = ilandakiArac.ID};
                    this.AracDonanimiEkle(temp);
                }
            }

            _ilanYonetimi.IlanEkle(ilan);
            return ilan;
        }

        public Ilan IlanGetir(int ID)
        {
            return _ilanYonetimi.IlanGetir(ID);
        }

        public bool IlanOnayla(int ID)
        {
            return _ilanYonetimi.IlanOnayla(ID);
        }

        public List<Ilan> TumIlanlariGetir()
        {
            return _ilanYonetimi.TumIlanlariGetir();
        }

        public List<Ilan> IlanlariListele(AracSaticisi aracSaticisi)
        {
            return _ilanYonetimi.IlanlariListele(aracSaticisi);
        }
    }
}
