using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
    public interface IAracYonetimi
    {
        // Temel
        public Arac AracEkle(Arac arac);
        public bool AracSil(int ID);
        public bool MarkaEkle(AracMarkasi aracMarkasi);
        public bool ModelEkle(AracModeli aracModeli);
        public bool AracDonanimiEkle(AracDonanimi donanim);


        // DONDUR
        public List<Arac> TumAraclariGetir();
        public List<AracMarkasi> TumMarkalariGetir();
        public List<AracModeli> TumModelleriGetir();

        public Arac AracGetir(int ID);
        public AracModeli ModelGetir(int ID);
        public AracMarkasi MarkaGetir(int ID);
    }

    public class AracYonetimi : IAracYonetimi
    {
        IAracRepository _aracRepository;

        public AracYonetimi(IAracRepository aracRepository)
        {
            _aracRepository = aracRepository;
        }

        #region Arac_Temel
        public Arac AracEkle(Arac arac)
        {
            return _aracRepository.AracEkle(arac);
        }

        public bool AracSil(int ID)
        {
            return _aracRepository.AracSil(ID);
        }

        public bool MarkaEkle(AracMarkasi aracMarkasi)
        {
            return _aracRepository.MarkaEkle(aracMarkasi);
        }

        public bool ModelEkle(AracModeli aracModeli)
        {
            return _aracRepository.ModelEkle(aracModeli);
        }

        public bool AracDonanimiEkle(AracDonanimi donanim)
        {
            return _aracRepository.DonanimEkle(donanim);
        }
        #endregion

        #region Arac_Dondur
        public Arac AracGetir(int ID)
        {
            return _aracRepository.AracGetir(ID);
        }

        public List<AracMarkasi> TumMarkalariGetir()
        {
            return _aracRepository.TumMarkalariGetir();
        }

        public List<AracModeli> TumModelleriGetir()
        {
            return _aracRepository.TumModelleriGetir();
        }

        public List<Arac> TumAraclariGetir()
        {
            return _aracRepository.TumAraclariGetir();
        }

        public AracModeli ModelGetir(int ID)
        {
            return _aracRepository.ModelGetir(ID);
        }

        public AracMarkasi MarkaGetir(int ID)
        {
            return _aracRepository.MarkaGetir(ID);
        }
        #endregion
    }
}
