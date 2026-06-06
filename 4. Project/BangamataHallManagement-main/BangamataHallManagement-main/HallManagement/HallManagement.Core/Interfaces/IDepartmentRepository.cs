using HallManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Core.Interfaces
{
     public interface IDepartmentRepository : IRepositoryBase<Department>
     {
        //Department? GetByEmailOrPhone(string emailOrPhone);
        //bool IsReferenceNoExists(string referenceNo, int? id);
     }
}
