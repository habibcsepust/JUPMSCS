using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class StaffService : IStaffService
    {

        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public Staff GetById(int Id)
        {
            return _staffRepository.GetById(Id).Result;
        }

        public IEnumerable<Staff> GetAll()
        {
            return _staffRepository.GetAll().Result;
        }

        public bool Create(Staff staff)
        {
           return  _staffRepository.Create(staff);
        }

        public bool Update(Staff staff)
        {
           return  _staffRepository.Update(staff);
        }

        public bool Delete(int id)
        {
            var staff = _staffRepository.GetById(id)?.Result;
            if (staff == null)
                return false;
            return _staffRepository.Delete(staff);              
        }
    }
}
