﻿using System.Text.Json.Serialization;

namespace Database.Models
{

    public class RandomUser
    {
        public string gender { get; set; }
        public Name name { get; set; }
        //public Location location { get; set; }
      //  public string email { get; set; }
        public Login login { get; set; }
      //  public Dob dob { get; set; }
        public Registered registered { get; set; }
     //   public string phone { get; set; }
     //   public string cell { get; set; }
     //   public Id id { get; set; }
        public Picture picture { get; set; }
     //   public string nat { get; set; }
    }

    public class Name
    {
        public string? title { get; set; }
        public string? first { get; set; }
        public string? last { get; set; }
    }

    public class Location
    {
        public Street street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public ulong postcode { get; set; }
        public Coordinates coordinates { get; set; }
        public Timezone timezone { get; set; }
    }

    public class Street
    {
        public int number { get; set; }
        public string? name { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Timezone
    {
        public string offset { get; set; }
        public string description { get; set; }
    }

    public class Login
    {
        [JsonPropertyName("uuid")]
        public Guid userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }

    public class Dob
    {
        public DateTime date { get; set; }
        public ushort age { get; set; }
    }

    public class Registered
    {
        public DateTime date { get; set; }
        public ushort age { get; set; }
    }

    public class Id
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Picture
    {
        public string? large { get; set; }
        public string? medium { get; set; }
        public string? thumbnail { get; set; }
    }
}
