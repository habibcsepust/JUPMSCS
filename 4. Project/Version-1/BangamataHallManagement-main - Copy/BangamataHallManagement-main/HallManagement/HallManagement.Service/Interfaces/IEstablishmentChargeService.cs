using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IEstablishmentChargeService
    {
        IEnumerable<EstablishmentCharge> GetAll();
        EstablishmentCharge GetById(int Id);
        bool Create(EstablishmentCharge establishmentCharge);
        bool Update(EstablishmentCharge establishmentCharge);
        bool Delete(int id);
    }
}
