using Bulky_Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inject custom extensions
builder.Services.InjectDbContext(builder.Configuration)
				.InjectServiceScopes();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.UseExceptionHandler("/Home/Error/");
}


app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");
app.ConfigureCORS(builder.Configuration);
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
