using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IBloodGroupService
    {
        IEnumerable<BloodGroup> GetAll();
        BloodGroup GetById(int Id);
        bool Create(BloodGroup bloodGroup);
        bool Update(BloodGroup bloodGroup);
        bool Delete(int id);
    }
}
