using UsedVehicleSystem.Database;
using UsedVehicleSystem.Facade;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Repository;
using UsedVehicleSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{options.IdleTimeout = TimeSpan.FromSeconds(60);});

var myServices = new List<(Type, Type)>
{
    (typeof(IAracRepository),               typeof(AracRepository)),
    (typeof(IAracYonetimi),                 typeof(AracYonetimi)),
    (typeof(IUyeYonetimi),                  typeof(UyeYonetimi)),
    (typeof(IUyeRepository),                typeof(UyeRepository)),
    (typeof(IIlanRepository),               typeof(IlanRepository)),
    (typeof(IIlanYonetimi),                 typeof(IlanYonetimi)),
    (typeof(IYorumRepository),              typeof(YorumRepository)),
    (typeof(IYorumYonetimi),                typeof(YorumYonetimi)),
    (typeof(ISistemYoneticisiRepository),   typeof(SistemYoneticisiRepository)),
    (typeof(ISistemYonetimi),               typeof(SistemYonetimi)),
    (typeof(IRepoMediator),                 typeof(RepoMediator)),
    (typeof(IFacade),                       typeof(Facade))
};

builder.Services.AddDbContext<SystemDBContext>();

foreach (var (s, implementation) in myServices)
{
    builder.Services.AddScoped(s, implementation);
}

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
