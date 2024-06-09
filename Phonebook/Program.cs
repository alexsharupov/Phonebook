
using Database.Db;
using Database.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
var _contex = new ApplicationContext();

var users = await _contex.Users.ToListAsync<RandomUserEntity>();

app.MapGet("/", () => "Hello World!");

app.Run();
