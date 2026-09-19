using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class EstablishmentChargeService : IEstablishmentChargeService
    {

        private readonly IEstablishmentChargeRepository _establishmentChargeRepository;

        public EstablishmentChargeService(IEstablishmentChargeRepository establishmentChargeRepository)
        {
            _establishmentChargeRepository = establishmentChargeRepository;
        }

        public EstablishmentCharge GetById(int Id)
        {
            return _establishmentChargeRepository.GetById(Id).Result;
        }

        public IEnumerable<EstablishmentCharge> GetAll()
        {
            return _establishmentChargeRepository.GetAll().Result;
        }

        public bool Create(EstablishmentCharge stablishmentCharge)
        {
           return  _establishmentChargeRepository.Create(stablishmentCharge);
        }

        public bool Update(EstablishmentCharge stablishmentCharge)
        {
           return  _establishmentChargeRepository.Update(stablishmentCharge);
        }

        public bool Delete(int id)
        {
            var stablishmentCharge = _establishmentChargeRepository.GetById(id)?.Result;
            if (stablishmentCharge == null)
                return false;
            return _establishmentChargeRepository.Delete(stablishmentCharge);              
        }
    }
}
