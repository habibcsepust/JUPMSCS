using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model
{
    public enum LoginAs : int
    {
        Admin = 1,
        SuperAdmin = 2,
        Student = 3,
        Developer = 4
    }
}
