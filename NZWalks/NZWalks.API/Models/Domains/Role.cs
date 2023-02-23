namespace NZWalks.API.Models.Domains
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        // Navigate property
        public List<User_Role> UserRoles { get; set; }
    }
}
