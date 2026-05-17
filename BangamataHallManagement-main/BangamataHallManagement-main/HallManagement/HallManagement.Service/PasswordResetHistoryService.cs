using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class PasswordResetHistoryService : IPasswordResetHistoryService
    {

        private readonly IPasswordResetHistoryRepository _passwordResetHistoryRepository;

        public PasswordResetHistoryService(IPasswordResetHistoryRepository passwordResetHistoryRepository)
        {
            _passwordResetHistoryRepository = passwordResetHistoryRepository;
        }

        public PasswordResetHistory GetById(int Id)
        {
            return _passwordResetHistoryRepository.GetById(Id).Result;
        }

        public IEnumerable<PasswordResetHistory> GetAll()
        {
            return _passwordResetHistoryRepository.GetAll().Result;
        }

        public bool Create(PasswordResetHistory passwordResetHistory)
        {
           return  _passwordResetHistoryRepository.Create(passwordResetHistory);
        }

        public bool Update(PasswordResetHistory passwordResetHistory)
        {
           return  _passwordResetHistoryRepository.Update(passwordResetHistory);
        }

        public bool Delete(int id)
        {
            var passwordResetHistory = _passwordResetHistoryRepository.GetById(id)?.Result;
            if (passwordResetHistory == null)
                return false;
            return _passwordResetHistoryRepository.Delete(passwordResetHistory);              
        }
    }
}
