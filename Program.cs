using BlogDapper.Repository;
using BlogDapperNew;
using BlogDapperNew.Data;
using BlogDapperNew.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();
// builder.Services.AddScoped<IPublicacionRepository, PublicacionRepositoryContrib>();
// builder.Services.AddScoped<IPublicacionRepository, PublicacionRepositorySP>();

var app = builder.Build();

app.UseHttpsRedirection();

app.ConfigureApi();

app.Run();
