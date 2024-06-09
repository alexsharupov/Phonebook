namespace Phonebook.Models
{
    public class WebUser
    {
        public Guid userid { get; set; }
        public string gender { get; set; }
        public string password { get; set; }
        public string thumbnail { get; set; }
        public string fullName { get; set; }
        public int age { get; set; }
        public DateTime registeredDate { get; set; }

    }
}
