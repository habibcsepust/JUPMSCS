using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class EstablishmentChargeRepository: RepositoryBase<EstablishmentCharge>, IEstablishmentChargeRepository
    {
        BangamataHallContext _applicationContext;
        public EstablishmentChargeRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
