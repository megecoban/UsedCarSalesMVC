using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
	public interface IUyeYonetimi
    {
		// TEMEL
        public Uye UyeOl(Uye uye, string? telefonNuamrasi = null);
        public Uye GirisYap(string Eposta, string Sifre);
		public bool UyeVarMi(string Eposta);
		public bool UyeVarMi(int ID);
        public bool MusteriMi(int ID);


		// DONDUR
        public Uye UyeGetir(int ID);
		public AracSaticisi AracSaticisiGetir(int ID);
    }

	public class UyeYonetimi : IUyeYonetimi
    {
		IUyeRepository _uyeRepository;

		public UyeYonetimi(IUyeRepository uyeRepository)
		{
			_uyeRepository = uyeRepository;
		}

        #region UyelikIslemleri
        public Uye UyeOl(Uye uye, string? telefonNuamrasi = null)
        {
            return _uyeRepository.UyeEkle(uye, telefonNuamrasi);
        }

        public Uye GirisYap(string Eposta, string Sifre)
		{
			return _uyeRepository.UyeGiris(Eposta, Sifre);
        }

        public Uye UyeGetir(int ID)
        {
            return _uyeRepository.UyeGetir(ID);
        }

        public AracSaticisi AracSaticisiGetir(int ID)
        {
            return _uyeRepository.AracSaticisiGetir(ID);
        }
        #endregion

        #region Kontrol
        public bool UyeVarMi(string Eposta)
        {
            return _uyeRepository.UyeVarMi(Eposta);
        }

        public bool MusteriMi(int ID)
        {
            if (!UyeVarMi(ID))
                return false;

            return _uyeRepository.MusteriMi(ID);
        }
        public bool UyeVarMi(int ID)
        {
            return _uyeRepository.UyeVarMi(ID);
        }
        #endregion

    }
}
