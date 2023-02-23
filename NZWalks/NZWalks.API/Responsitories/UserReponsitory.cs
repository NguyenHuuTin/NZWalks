using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Responsitories
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public UserReponsitory(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<User> AuthenticationAsync(string useName, string pass)
        {
            var user = await nZWalksDbContext.Users
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == useName.ToLower() && x.Password == pass);

            if (user == null) { return null; }

            var userRoles = await nZWalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await nZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);

                    user.Roles.Add(role.Name);
                }
            }

            user.Password = null;

            return user;
        }
    }
}
