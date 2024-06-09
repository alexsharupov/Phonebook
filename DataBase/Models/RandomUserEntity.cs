using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Models
{
    //Привет automapper

    public class RandomUserEntity
    {
        public RandomUserEntity()
        {
            
        }

        public RandomUserEntity(RandomUser user)
        {
            gender = user.gender;
            userid = user.login.userid;
            login = new LoginEntity(user.login);
        }

        public string gender { get; set; }
        public Guid userid { get; set; }
        public LoginEntity login { get; set; }
        
    }
    public class LoginEntity
    {
        public LoginEntity()
        {
            
        }
        public LoginEntity(Login login)
        {
            userid = login.userid;
            username = login.username;
            password = login.password;
            salt = login.salt;
            md5 = login.md5;
            sha1 = login.sha1;
            sha256 = login.sha256;
        }
        public Guid userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }

}
