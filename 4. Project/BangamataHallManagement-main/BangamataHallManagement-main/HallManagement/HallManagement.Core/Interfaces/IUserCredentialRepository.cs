using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Core.Interfaces
{
    public interface IUserCredentialRepository : IRepositoryBase<UserCredential>
    {
        LoggedUserVm? IsUserExists(string userName, string password, bool isStudentLogin);
    }
}
