using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class NationalityRepository: RepositoryBase<Nationality>, INationalityRepository
    {
        BangamataHallContext _applicationContext;
        public NationalityRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
