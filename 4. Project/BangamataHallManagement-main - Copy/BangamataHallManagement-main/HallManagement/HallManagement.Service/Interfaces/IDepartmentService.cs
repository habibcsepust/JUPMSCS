using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        Department GetById(int Id);
        
        //Department GetByEmailOrPhone(string emilOrPhoneExist);
        //bool IsReferenceNoExists(string referenceNo, int? id);
        bool Create(Department department);
        bool Update(Department department);
        bool Delete(int id);
    }
}
