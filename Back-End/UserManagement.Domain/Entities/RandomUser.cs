namespace UserManagement.Domain.Entities
{
    public class RandomUser
    {
        public Name Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public Location Location { get; set; }
        public Picture Picture { get; set; }
        public string Phone { get; set; }        
        public string Cell { get; set; }  
    }

    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Location
    {
        public Street Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }      
        public string Country { get; set; }
        public int PostCode { get; set; }  
    }

    public class Street
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }
    public class Picture
    {
        public string Large { get; set; }
    }
}
