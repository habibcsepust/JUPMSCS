using HallManagement.Core.Interfaces;
using HallManagement.Model;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class UserCredentialRepository : RepositoryBase<UserCredential>, IUserCredentialRepository
    {
        BangamataHallContext _applicationContext;
        public UserCredentialRepository(BangamataHallContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public LoggedUserVm? IsUserExists(string userName, string password, bool isStudentLogin)
        {
            if (!isStudentLogin)
                return _applicationContext.UserCredentials
                    .Where(x => (x.Staff.Email == userName || x.Staff.Mobile == userName) && x.Password == password && x.IsEnabled == true)
                    .Select(x => new LoggedUserVm { UserCredentialId = x.Id, Id = x.StaffId, Name = x.Staff.Name, RoleId = x.RoleId, RoleName = x.Role.Name, IsPasswordResetDone = x.IsPasswordResetDone })
                    .FirstOrDefault();
            else if (isStudentLogin)
                return _applicationContext.Students
                    .Where(x => (x.Email == userName || x.Mobile == userName) && x.Password == password && x.IsArchived != true)
                    .Select(x => new LoggedUserVm { Id = x.Id, Name = x.Name, RoleId = (int)LoginAs.Student, RoleName = LoginAs.Student.ToString(), IsPasswordResetDone = x.IsPasswordResetDone })
                    .FirstOrDefault();
            else return null;
        }
    }
}
