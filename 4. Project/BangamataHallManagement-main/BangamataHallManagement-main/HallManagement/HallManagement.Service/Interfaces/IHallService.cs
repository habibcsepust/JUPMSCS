using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IHallService
    {
        IEnumerable<Hall> GetAll();
        Hall GetById(int Id);
        
        //Department GetByEmailOrPhone(string emilOrPhoneExist);
        //bool IsReferenceNoExists(string referenceNo, int? id);
        bool Create(Hall hall);
        bool Update(Hall hall);
        bool Delete(int id);
    }
}
