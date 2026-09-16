using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IDesignationService
    {
        IEnumerable<Designation> GetAll();
        Designation GetById(int Id);
        bool Create(Designation designation);
        bool Update(Designation designation);
        bool Delete(int id);
    }
}
