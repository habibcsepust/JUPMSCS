using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class DesignationRepository: RepositoryBase<Designation>, IDesignationRepository
    {
        BangamataHallContext _applicationContext;
        public DesignationRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
