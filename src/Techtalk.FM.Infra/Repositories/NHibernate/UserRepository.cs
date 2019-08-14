using System.Threading.Tasks;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.Infra.Repositories.NHibernate
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<User> LoginSearch(string email, string password)
        {
            return await Session.QueryOver<User>()
                .Where(x => x.Email == email)
                .And(x => x.Password == password)
                .SingleOrDefaultAsync();
        }
    }
}
