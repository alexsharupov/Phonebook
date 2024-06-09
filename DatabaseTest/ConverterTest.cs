using Database.Test;
using DataBase.Http;
using System.Text.Json;

namespace DatabaseTest
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        [DataRow(Const.responce)] // ������ �������� �� �����������
        public void ConverterRequest(string i)
        {
            var data = UsersGet.ConvertRequest(i, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            Assert.IsNotNull(data);
        }
        [TestMethod]
        [DataRow(Const.responce)] // ��� ��������� ���� �� ������������
        public void ConverterRequestUser(string i)
        {
            var data = UsersGet.ConvertRequest(i, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            Assert.IsNotNull(data.results);
        }
        [TestMethod]
        [DataRow(Const.responce)] // ������ ���� ���� ������������
        public void ConverterRequestUserOne(string i)
        {
            var data = UsersGet.ConvertRequest(i, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            Assert.AreEqual(1, data.results.Count);


        }
    }
}