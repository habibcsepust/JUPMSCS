using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
    {
        BangamataHallContext _applicationContext;

        public ApplicationRepository(BangamataHallContext applicationContext)
            : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        // Custom methods can be added here if required later.
    }
}