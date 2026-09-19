using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class BloodGroupRepository: RepositoryBase<BloodGroup>, IBloodGroupRepository
    {
        BangamataHallContext _applicationContext;
        public BloodGroupRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
