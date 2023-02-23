using NZWalks.API.Models.Domains;

namespace NZWalks.API.Responsitories
{
    public interface ITokenHandle
    {
        Task<string> CreateTokenAsync(User user);
    }
}
