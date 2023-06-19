using Microsoft.EntityFrameworkCore;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Database
{
	public class SystemDBContext : DbContext
    {
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<AracSaticisi> AracSaticilari { get; set; }
        public DbSet<SistemYoneticisi> SistemYoneticileri { get; set; }
        public DbSet<Ilan> Ilanlar { get; set; }
		public DbSet<Yorum> Yorumlar { get; set; }
		public DbSet<Arac> Araclar { get; set; }
		public DbSet<AracMarkasi> AracMarkalari { get; set; }
		public DbSet<AracModeli> AracModelleri { get; set; }
        public DbSet<AracDonanimi> AracDonanimlari { get; set; }

		public bool StartupCheck = false;

        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<(Type, string)> ForTables = new List<(Type, string)>
            {
                (typeof(Uye), "Uyeler"),
                (typeof(Musteri), "Musteriler"),
                (typeof(AracSaticisi), "AracSaticilari"),
            };

            foreach (var (t, name) in ForTables)
            {
                modelBuilder.Entity(t).ToTable(name);
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=my.db");
		}

		public void StartUp()
		{
			if (!StartupCheck)
				this.Database.EnsureCreated();
			StartupCheck = true;
		}
	}
}
