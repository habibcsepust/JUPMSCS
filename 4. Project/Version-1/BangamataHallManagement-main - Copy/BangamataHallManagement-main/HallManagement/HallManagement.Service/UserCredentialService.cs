using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class UserCredentialService : IUserCredentialService
    {

        private readonly IUserCredentialRepository _userCredentialRepository;

        public UserCredentialService(IUserCredentialRepository userCredentialRepository)
        {
            _userCredentialRepository = userCredentialRepository;
        }

        public UserCredential GetById(int Id)
        {
            return _userCredentialRepository.GetById(Id).Result;
        }

        public IEnumerable<UserCredential> GetAll()
        {
            return _userCredentialRepository.GetAll().Result;
        }

        public bool Create(UserCredential userCredential)
        {
           return  _userCredentialRepository.Create(userCredential);
        }

        public bool Update(UserCredential userCredential)
        {
           return  _userCredentialRepository.Update(userCredential);
        }

        public bool Delete(int id)
        {
            var userCredential = _userCredentialRepository.GetById(id)?.Result;
            if (userCredential == null)
                return false;
            return _userCredentialRepository.Delete(userCredential);              
        }

        public LoggedUserVm? IsUserExists(string userName, string password, bool isStudentLogin)
        {
            return _userCredentialRepository.IsUserExists(userName, password, isStudentLogin);
        }
    }
}
