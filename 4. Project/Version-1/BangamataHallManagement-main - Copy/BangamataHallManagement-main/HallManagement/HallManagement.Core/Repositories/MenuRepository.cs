using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class MenuRepository: RepositoryBase<Menu>, IMenuRepository
    {
        BangamataHallContext _applicationContext;
        public MenuRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
