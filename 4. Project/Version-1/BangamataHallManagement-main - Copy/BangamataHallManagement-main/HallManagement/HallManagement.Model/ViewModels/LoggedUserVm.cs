using HallManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class LoggedUserVm
    {
        public int Id { get; set; }
        public int UserCredentialId { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsPasswordResetDone { get; set; }
    }
}
