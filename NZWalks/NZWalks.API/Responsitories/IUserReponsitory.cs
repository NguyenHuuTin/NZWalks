using NZWalks.API.Models.Domains;

namespace NZWalks.API.Responsitories
{
    public interface IUserReponsitory
    {
        Task<User> AuthenticationAsync(string useName, string pass);
    }
}
