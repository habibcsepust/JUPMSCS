using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class PasswordResetHistoryRepository: RepositoryBase<PasswordResetHistory>, IPasswordResetHistoryRepository
    {
        BangamataHallContext _applicationContext;
        public PasswordResetHistoryRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
