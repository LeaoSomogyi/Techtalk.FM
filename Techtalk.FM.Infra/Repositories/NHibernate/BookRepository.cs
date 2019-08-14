using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.Infra.Repositories.NHibernate
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
