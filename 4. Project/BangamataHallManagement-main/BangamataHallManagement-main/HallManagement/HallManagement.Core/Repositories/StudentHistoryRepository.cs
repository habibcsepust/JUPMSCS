using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class StudentHistoryRepository : RepositoryBase<StudentHistory>, IStudentHistoryRepository
    {
        BangamataHallContext _applicationContext;
        public StudentHistoryRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
