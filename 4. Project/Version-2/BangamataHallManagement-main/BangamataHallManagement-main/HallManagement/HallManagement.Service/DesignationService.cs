using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class DesignationService : IDesignationService
    {

        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public Designation GetById(int Id)
        {
            return _designationRepository.GetById(Id).Result;
        }

        public IEnumerable<Designation> GetAll()
        {
            return _designationRepository.GetAll().Result;
        }

        public bool Create(Designation designation)
        {
           return  _designationRepository.Create(designation);
        }

        public bool Update(Designation designation)
        {
           return  _designationRepository.Update(designation);
        }

        public bool Delete(int id)
        {
            var designation = _designationRepository.GetById(id)?.Result;
            if (designation == null)
                return false;
            return _designationRepository.Delete(designation);              
        }
    }
}
