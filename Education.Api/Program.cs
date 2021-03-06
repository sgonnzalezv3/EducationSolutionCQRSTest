using Education.Application.Cursos;
using Education.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/* Inyectando la cadena de conexion Bd */
builder.Services.AddDbContext<EducationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
/* Inyectando MediaTR */
builder.Services.AddMediatR(typeof(GetCursoQuery.GetCursoQueryHandler).Assembly);

/* Agregando fluent Validation */
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CreateCursoCommand>());
/* Inyectando Automapper */
builder.Services.AddAutoMapper(typeof(GetCursoQuery.GetCursoQueryHandler));

/* Configurando CORS */

builder.Services.AddCors(x => x.AddPolicy("corsApp", builder =>
 {
     builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
 }));

builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
