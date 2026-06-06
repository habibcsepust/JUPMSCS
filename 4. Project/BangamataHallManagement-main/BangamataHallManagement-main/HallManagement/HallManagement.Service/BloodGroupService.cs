using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class BloodGroupService : IBloodGroupService
    {

        private readonly IBloodGroupRepository _bloodGroupRepository;

        public BloodGroupService(IBloodGroupRepository bloodGroupRepository)
        {
            _bloodGroupRepository = bloodGroupRepository;
        }

        public BloodGroup GetById(int Id)
        {
            return _bloodGroupRepository.GetById(Id).Result;
        }

        public IEnumerable<BloodGroup> GetAll()
        {
            return _bloodGroupRepository.GetAll().Result;
        }

        public bool Create(BloodGroup bloodGroup)
        {
           return  _bloodGroupRepository.Create(bloodGroup);
        }

        public bool Update(BloodGroup bloodGroup)
        {
           return  _bloodGroupRepository.Update(bloodGroup);
        }

        public bool Delete(int id)
        {
            var bloodGroup = _bloodGroupRepository.GetById(id)?.Result;
            if (bloodGroup == null)
                return false;
            return _bloodGroupRepository.Delete(bloodGroup);              
        }
    }
}
