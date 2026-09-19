using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class ReligionService : IReligionService
    {

        private readonly IReligionRepository _religionRepository;

        public ReligionService(IReligionRepository religionRepository)
        {
            _religionRepository = religionRepository;
        }

        public Religion GetById(int Id)
        {
            return _religionRepository.GetById(Id).Result;
        }

        public IEnumerable<Religion> GetAll()
        {
            return _religionRepository.GetAll().Result;
        }

        public bool Create(Religion religion)
        {
           return  _religionRepository.Create(religion);
        }

        public bool Update(Religion religion)
        {
           return  _religionRepository.Update(religion);
        }

        public bool Delete(int id)
        {
            var religion = _religionRepository.GetById(id)?.Result;
            if (religion == null)
                return false;
            return _religionRepository.Delete(religion);              
        }
    }
}
