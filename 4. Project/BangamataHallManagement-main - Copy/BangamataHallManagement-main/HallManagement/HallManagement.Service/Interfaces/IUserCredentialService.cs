using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;

namespace HallManagement.Service.Interfaces
{
    public interface IUserCredentialService
    {
        IEnumerable<UserCredential> GetAll();
        UserCredential GetById(int Id);
        bool Create(UserCredential userCredential);
        bool Update(UserCredential userCredential);
        bool Delete(int id);
        LoggedUserVm? IsUserExists(string userName, string password, bool isStudentLogin);
    }
}
