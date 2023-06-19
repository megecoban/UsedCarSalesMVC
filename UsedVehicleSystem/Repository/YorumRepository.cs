using UsedVehicleSystem.Database;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Repository
{
    public interface IYorumRepository
    {
        public Yorum YorumEkle(int AracSaticisi, string Mesaj, string ad);
        public Yorum YorumGetir(int ID);
        public List<Yorum> TumYorumlariGetir(int AracSaticisi);
    }

    public class YorumRepository : IYorumRepository
    {
        private SystemDBContext dbContext;

        public YorumRepository(SystemDBContext context)
        {
            dbContext = context;
        }

        public Yorum YorumEkle(int AracSaticisi, string Mesaj, string ad)
        {
            Yorum yorum = new Yorum()
            {
                AracSaticisiID = AracSaticisi,
                yorumIcerigi = Mesaj,
                musteriAdi = ad,
            };

            dbContext.Yorumlar.Add(yorum);

            dbContext.SaveChanges();

            return yorum;
        }

        public List<Yorum> TumYorumlariGetir(int AracSaticisi)
        {
            return dbContext.Yorumlar.Where(y => y.AracSaticisiID == AracSaticisi).ToList();
        }

        public Yorum YorumGetir(int ID)
        {
            return dbContext.Yorumlar.FirstOrDefault(y => y.yorumID == ID);
        }
    }
}
