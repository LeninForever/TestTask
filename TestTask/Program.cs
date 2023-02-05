using Microsoft.EntityFrameworkCore;
using TestTask.DAL;
using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;
using TestTask.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudydbContext>(
    optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerString")));
builder.Services.AddScoped<IStudyGroupsRepository, StudyGroupsSpRepository>();
builder.Services.AddScoped<ITeachersRepository, TeacherSpRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeSpRepository>();
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

app.UseAuthorization();
app.UseMiddleware<ExceprionHandlingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Groups}/{action=Index}/{id?}");

app.Run();