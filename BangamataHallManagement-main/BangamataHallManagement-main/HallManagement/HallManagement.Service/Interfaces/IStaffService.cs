using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IStaffService
    {
        IEnumerable<Staff> GetAll();
        Staff GetById(int Id);
        bool Create(Staff staff);
        bool Update(Staff staff);
        bool Delete(int id);
    }
}
