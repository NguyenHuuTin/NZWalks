using NZWalks.API.Models.Domains;

namespace NZWalks.API.Responsitories
{
    public class StaticUserReponsitory : IUserReponsitory
    {
        private List<User> Users = new List<User>()
        {
            /*new User()
            {
                Id = Guid.NewGuid(),
                UserName = "username1",
                Password = "username@1",
                Email = "use1@gmail.com",
                Address = "address1",
                FirstName = "firstname1",
                LastName = "lastname1",
                Roles = new List<string> { "read"}
            },

            new User()
            {
                Id = Guid.NewGuid(),
                UserName = "username2",
                Password = "username@2",
                Email = "use2@gmail.com",
                Address = "address2",
                FirstName = "firstname2",
                LastName = "lastname2",
                Roles = new List<string> { "read", "write" }
            }*/
        };

        public async Task<User> AuthenticationAsync(string useName, string pass)
        {
            var user = Users.Find(x => x.UserName.Equals(useName, StringComparison.InvariantCultureIgnoreCase) && x.Password == pass);
            return user;
        }
    }
}
