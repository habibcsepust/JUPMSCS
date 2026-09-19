using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class SessionRepository: RepositoryBase<Session>, ISessionRepository
    {
        BangamataHallContext _applicationContext;
        public SessionRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
