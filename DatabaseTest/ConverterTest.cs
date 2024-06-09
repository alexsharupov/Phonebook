using Database.Test;
using DataBase.Http;
using System.Text.Json;

namespace DatabaseTest
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        [DataRow(Const.responce)] // Вообще проходит ли конвертация
        public void ConverterRequest(string i)
        {
            var data = UsersGet.ConvertRequest(i, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            Assert.IsNotNull(data);
        }
        [TestMethod]
        [DataRow(Const.responce)] // При конверции есть ли пользователь
        public void ConverterRequestUser(string i)
        {
            var data = UsersGet.ConvertRequest(i, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            Assert.IsNotNull(data.results);
        }
        [TestMethod]
        [DataRow(Const.responce)] // Должен быть один пользователь
        public void ConverterRequestUserOne(string i)
        {
            var data = UsersGet.ConvertRequest(i, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            Assert.AreEqual(1, data.results.Count);


        }
    }
}