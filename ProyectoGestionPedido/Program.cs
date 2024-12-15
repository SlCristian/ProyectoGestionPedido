using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data;
using ProyectoGestionPedido.Data.DataAccess;
using ProyectoGestionPedido.Data.Interface;
using static ProyectoGestionPedido.Data.ApplicationDbContext;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
//AGREGANDO PARA LAS INTERFACES//
//ACA SE PONE PARA LAS INTERFACES//
builder.Services.AddSingleton<IDACliente,ClienteDA>();
builder.Services.AddSingleton<IDADetallePedido,DetallePedidoDA>();
builder.Services.AddSingleton<IDAMetodoTransporte, MetodoTransporteDA>();
builder.Services.AddSingleton<IDAPedido, PedidoDA>();
builder.Services.AddSingleton<IDAProducto, ProductoDA>();
builder.Services.AddSingleton<IDARutas, RutasDA>();
//AGREGANDO PARA LOS ROLES//
builder.Services.AddAuthorization(
    options => options.AddPolicy(
        "AllowLayoutAdministrador",
    policy => policy.RequireRole("Administrador")));

builder.Services.AddAuthorization(
    options => options.AddPolicy(
        "AllowLayoutVendedor",
    policy => policy.RequireRole("Vendedor")));

builder.Services.AddAuthorization(
    options => options.AddPolicy(
        "AllowLayoutCliente",
    policy => policy.RequireRole("Cliente")));



//FINAL//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
