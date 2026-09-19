using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class ReligionRepository: RepositoryBase<Religion>, IReligionRepository
    {
        BangamataHallContext _applicationContext;
        public ReligionRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
