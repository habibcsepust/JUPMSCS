using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class HallRepository : RepositoryBase<Hall>, IHallRepository
    {
        BangamataHallContext _applicationContext;
        public HallRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        //public Department? GetByEmailOrPhone(string emailOrPhone)
        //{
        //    return _applicationContext.Departments.Where(x => (x.Email == emailOrPhone || x.MobileNo == emailOrPhone) && x.StatusId == (int)RemittanceTypeEnum.SentToMaker).OrderByDescending(x => x.CreateDate).FirstOrDefault();
        //}

        
        //public bool IsReferenceNoExists(string referenceNo, int? id)
        //{
        //    return _applicationContext.Departments.Any(x => (id != null && x.Id != id) && x.TransactionNo == referenceNo);
        //}
    }
}
