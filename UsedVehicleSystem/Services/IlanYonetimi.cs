using UsedVehicleSystem.Database;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
	public interface IIlanYonetimi
	{
        // TEMEL
        public Ilan IlanEkle(Ilan ilan);
        public Ilan IlanGuncelle(int ilanID, Ilan guncelIlan);
        public bool IlanSil(int ID);
        public void IlanYayindanKaldir(Ilan ilan);
        public bool IlanOnayla(int ID);

        // DONDUR
        public Ilan IlanGetir(int ilanID);
        public List<Ilan> IlanAra(Ilan filtrelenmisIlan);
        public List<Ilan> TumIlanlariGetir();
        public List<Ilan> IlanlariListele(AracSaticisi saticisi);
    }

	public class IlanYonetimi : IIlanYonetimi
	{
		private IIlanRepository _ilanRepository;
        private IRepoMediator _repoMediator;

        public IlanYonetimi(IRepoMediator repoMediator, IIlanRepository ilanRepository)
		{
			_repoMediator = repoMediator;
            _ilanRepository = ilanRepository;
		}



        #region Ilan_Temel
        public Ilan IlanEkle(Ilan ilan)
        {
            return _ilanRepository.IlanEkle(ilan);
        }

        public Ilan IlanGuncelle(int ilanID, Ilan guncelIlan)
        {
            return _ilanRepository.IlanGuncelle(ilanID, guncelIlan);
        }

        public void IlanYayindanKaldir(Ilan ilan)
        {
            _ilanRepository.IlanSil(ilan.ID);
        }

        public bool IlanOnayla(int ID)
        {
            return _ilanRepository.IlanOnayla(ID);
        }

        public bool IlanSil(int ID)
        {
            return _ilanRepository.IlanSil(ID);
        }

        #endregion

        #region Ilan_Dondur

        public List<Ilan> IlanAra(Ilan filtrelenmisIlan)
        {
            return _repoMediator.IlanAra(filtrelenmisIlan);
        }

        public List<Ilan> TumIlanlariGetir()
        {
            return _ilanRepository.TumIlanlariGetir();
        }

        public List<Ilan> IlanlariListele(AracSaticisi saticisi)
		{
			return _ilanRepository.IlanlariGetir(saticisi.ID);
		}

        public Ilan IlanGetir(int ilanID)
        {
            return _repoMediator.IlanGetir(ilanID);
		}
        #endregion
    }
}
