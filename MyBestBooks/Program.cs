using Microsoft.EntityFrameworkCore;
using MyBestBooks.DataAccess.Data;
using MyBestBooks.DataAccess.Repository;
using MyBestBooks.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyBestBooks.Utility;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();//telling the application that we want to use entity framework core
builder.Services.AddDbContext /* this is the Dbcontext that's there in the framework core */
    <ApplicationDbContext>( // here we have added DbContext to our container
    options /*we can use anything we want option or o or anything*/ => options.UseSqlServer
    /* when we use the sql server we have to define the connection string right here... (from the appsetting.json) */
    (builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
               // with the previous particular line we have added roles(admin-customer-...) to our identity and that's
               // included in our project now. now we can display all the available roles but we need to seed those roles
               // we should check that (if we already have rule) in the GetHandler (OnGetAsync) in the register class
builder.Services.AddRazorPages(); // until now our application is supporting onle the MVC pages...
                                  // with this line here it will support the razor pages too.
                                  // all the identity pages are done with razor. so it should support the razor pages to map it (line 43)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// once we have the implementation of EmailSender (In the Utility folder) all we have to do is to register that in our services (like for UnitOfWork)
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

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
app.UseAuthentication(); // the authorization should always be before the authorization
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
