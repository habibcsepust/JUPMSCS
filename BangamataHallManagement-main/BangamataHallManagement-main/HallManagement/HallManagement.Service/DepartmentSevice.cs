using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Department GetById(int Id)
        {
            return _departmentRepository.GetById(Id).Result;
        }
        //public Department GetByEmailOrPhone(string emailOrPhone)
        //{
        //    return _departmentRepository.GetByEmailOrPhone(emailOrPhone);
        //}

        //public bool IsReferenceNoExists(string referenceNo, int? id)
        //{
        //    return _departmentRepository.IsReferenceNoExists(referenceNo, id);
        //}

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll().Result;
        }

        public bool Create(Department department)
        {
           return  _departmentRepository.Create(department);
        }

        public bool Update(Department department)
        {
           return  _departmentRepository.Update(department);
        }

        public bool Delete(int id)
        {
            var department = _departmentRepository.GetById(id)?.Result;
            if (department == null)
                return false;
            return _departmentRepository.Delete(department);              
        }
    }
}
