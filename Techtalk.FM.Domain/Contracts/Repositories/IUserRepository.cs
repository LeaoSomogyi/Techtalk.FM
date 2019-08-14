using System.Threading.Tasks;
using Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Returns user by e-mail and password
        /// </summary>
        /// <param name="email">User's e-mail</param>
        /// <param name="password">User's cript password</param>
        /// <returns>User</returns>
        Task<User> LoginSearch(string email, string password);
    }
}
