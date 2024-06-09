
using Database.Db;
using Database.Models;
using DataBase.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Phonebook.Models;
using System.Text;

////��������� ���������� ������, ��� �������� ���� � �������� �������������, ���� �� ���
var runner = ServiceProviderFactory.ServiceProvider.GetRequiredService<IApplicationRunner>();
await runner.Run();
////
/////�������� �������� ������ � ��
var _contex = new ApplicationContext();
var users = await _contex.Users.Select(u =>
new WebUser() { userid = u.userid, gender = u.gender, password = u.login.password, thumbnail = u.picture.thumbnail, fullName = u.fullName, age = u.registered.age, registeredDate = u.registered.date })
    .ToListAsync<WebUser>();
////
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Logger.LogInformation($"�����������");
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "RandomUser API V1");
    });
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapPost("/api/users", async (DataSourceLoadOptionsBase loadOptions, CancellationToken token) =>
{
    var list = users;
    if (loadOptions.Sort != null)
        //�� ��� �������� ���, �� ��� ������, � ����, ����� ������ ������� � ��������
        foreach (var sort in loadOptions.Sort)
        {
            if (sort.Selector == "fullName") list = sort.Desc ? users.OrderByDescending(o => o.fullName).ToList() : users.OrderBy(o => o.fullName).ToList();
            if (sort.Selector == "gender") list = sort.Desc ? users.OrderByDescending(o => o.gender).ToList() : users.OrderBy(o => o.gender).ToList();
            if (sort.Selector == "password") list = sort.Desc ? users.OrderByDescending(o => o.password).ToList() : users.OrderBy(o => o.password).ToList();
            if (sort.Selector == "age") list = sort.Desc ? users.OrderByDescending(o => o.age).ToList() : users.OrderBy(o => o.age).ToList();
            if (sort.Selector == "registeredDate") list = sort.Desc ? users.OrderByDescending(o => o.registeredDate).ToList() : users.OrderBy(o => o.registeredDate).ToList();
        }
    return new
    {
        data = list.Skip(loadOptions.Skip).Take(loadOptions.Take),
        totalCount = users.Count(),
    };
});
//app.MapControllers();
//app.MapGet("/", () => $"������ � ���� {users.Count} �������������"); 

app.Run();
