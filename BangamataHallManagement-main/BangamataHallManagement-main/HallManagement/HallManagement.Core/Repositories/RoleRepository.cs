using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class RoleRepository: RepositoryBase<Role>, IRoleRepository
    {
        BangamataHallContext _applicationContext;
        public RoleRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
