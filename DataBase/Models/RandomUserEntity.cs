using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            picture = new PictureEntity(user.picture, user.login.userid);
            name = new NameEntity(user.name, user.login.userid);
            registered = new RegisteredEntity(user.registered, user.login.userid);

        }

        public string gender { get; set; }
        public Guid userid { get; set; }
        virtual public LoginEntity login { get; set; } = new();
        virtual public PictureEntity picture { get; set; } = new();
        virtual public NameEntity name { get; set; } = new();
        virtual public RegisteredEntity registered { get; set; } = new();
        public string fullName { get { return name.title + ". " + name.first + " " + name.last; } }
      
    }

    public class RegisteredEntity
    {
        public RegisteredEntity()
        {
            
        }
        public RegisteredEntity(Registered registered, Guid userId)
        {
            date = registered.date;
            age = registered.age;
            userid = userId;
        }
        public Guid userid { get; set; }
        [ForeignKey("userid")]
        virtual public RandomUserEntity randomUserEntity { get; set; }
        public DateTime date { get; set; }
        public ushort age { get; set; }
    }

    public class NameEntity
    {
        public NameEntity()
        {
            
        }
        public NameEntity(Name name, Guid userId)
        {
            title = name.title;
            first = name.first;
            last = name.last;
            userid = userid;
        }
        public Guid userid { get; set; }
        [ForeignKey("userid")]
        virtual public RandomUserEntity randomUserEntity { get; set; }
        public string? title { get; set; }
        public string? first { get; set; }
        public string? last { get; set; }
    }

    public class PictureEntity
    {
        public PictureEntity()
        {
            
        }

        public PictureEntity(Picture pic, Guid userId)
        {
            large = pic.large;
            medium = pic.medium;
            thumbnail = pic.thumbnail;
            userid = userid;
        }
        public Guid userid { get; set; }
        [ForeignKey("userid")]
        virtual public RandomUserEntity randomUserEntity { get; set; }
        public string? large { get; set; }
        public string? medium { get; set; }
        public string? thumbnail { get; set; }
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
        [ForeignKey("userid")]
        virtual public RandomUserEntity randomUserEntity { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }

}
