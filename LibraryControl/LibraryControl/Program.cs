using LibraryControl.Business.Abstract;
using LibraryControl.Business.Concrete;
using LibraryControl.DataAccess.Abstract;
using LibraryControl.DataAccess.Concrete;
using LibraryControl.Entity.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//configuration
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//dependency injection
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookManager>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IWriterRepository, WriterRepository>();
builder.Services.AddScoped<IWriterService, WriterManager>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleManager>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllers();

    endpoints.MapControllerRoute(
                    name: "categories",
                    pattern: "categories/{search?}",
                    defaults: new { controller = "admin", action = "categories" }
                );
    endpoints.MapControllerRoute(
                    name: "writers",
                    pattern: "writers/{search?}",
                    defaults: new { controller = "admin", action = "writers" }
                );
    endpoints.MapControllerRoute(
                    name: "booksale",
                    pattern: "booksale/{id?}",
                    defaults: new { controller = "admin", action = "booksale" }
                );
    endpoints.MapControllerRoute(
                    name: "sales",
                    pattern: "sales",
                    defaults: new { controller = "admin", action = "sales" }
                );
    endpoints.MapControllerRoute(
                    name: "registerbook",
                    pattern: "registerbook",
                    defaults: new { controller = "admin", action = "registerbook" }
                );
    endpoints.MapControllerRoute(
                    name: "editbook",
                    pattern: "editbook/{id?}",
                    defaults: new { controller = "admin", action = "editbook" }
                );
    endpoints.MapControllerRoute(
                    name: "bookdetail",
                    pattern: "book/{id?}",
                    defaults: new { controller = "admin", action = "book" }
                );
    endpoints.MapControllerRoute(
                    name: "bookswithcategory",
                    pattern: "books/{categoryName?}",
                    defaults: new { controller = "admin", action = "books" }
                );
    endpoints.MapControllerRoute(
                    name: "booksdefault",
                    pattern: "",
                    defaults: new { controller = "admin", action = "books" }
                );
    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
});

app.Run();
