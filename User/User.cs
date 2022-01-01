
namespace UserLib
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return FirstName + ";" + LastName + ";" + Email + ";" + Phone;
        }
    }
}
