using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domains
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        // Navigate property
        public List<User_Role> UserRoles { get; set; }
    }
}
