using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IPasswordResetHistoryService
    {
        IEnumerable<PasswordResetHistory> GetAll();
        PasswordResetHistory GetById(int Id);
        bool Create(PasswordResetHistory passwordResetHistory);
        bool Update(PasswordResetHistory passwordResetHistory);
        bool Delete(int id);
    }
}
