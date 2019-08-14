using System.Threading.Tasks;
using Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.Domain.Contracts.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Authenticate user. If successfully, generate a JWT Token
        /// </summary>
        /// <param name="user">User to authenticate</param>
        /// <returns>Access Token</returns>
        Task<DTOs.Token> Authenticate(User user);
    }
}
