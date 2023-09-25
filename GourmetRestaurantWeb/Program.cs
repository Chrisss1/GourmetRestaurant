using GourmetRestaurant.DataAccess.Data;
using GourmetRestaurant.DataAccess.DbInitializer;
using GourmetRestaurant.DataAccess.Repository;
using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection")
	));
//stripe settings configuration
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
//Scaffolding, AddDefaultIdentity -> AddIdentity, AddDefaultTokenProviders()
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//Add service for fake email sender
builder.Services.AddSingleton<IEmailSender, EmailSender>();
//add dbinitializer
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
//register CategoryRepository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//authorization
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Identity/Account/Login";
	options.LogoutPath = "/Identity/Account/Logout";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
//session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(100);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
//facebook authentication
builder.Services.AddAuthentication().AddFacebook(options =>
{
	options.AppId = "239583202150621";
	options.AppSecret = "2f8c8eb64b27df04f46563d2cbfbb16a";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//call seeddatabase method
SeedDatabase();

//stripe secret key configuration
string key = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
StripeConfiguration.ApiKey = key;


//Scaffolding
app.UseAuthentication();

app.UseAuthorization();
//session
app.UseSession();
app.MapRazorPages();
app.MapControllers();
app.Run();

//idbinitializer
void SeedDatabase()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
		dbInitializer.Initialize();
	}
}
