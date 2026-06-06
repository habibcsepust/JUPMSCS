using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class NationalityService : INationalityService
    {

        private readonly INationalityRepository _nationalityRepository;

        public NationalityService(INationalityRepository nationalityRepository)
        {
            _nationalityRepository = nationalityRepository;
        }

        public Nationality GetById(int Id)
        {
            return _nationalityRepository.GetById(Id).Result;
        }

        public IEnumerable<Nationality> GetAll()
        {
            return _nationalityRepository.GetAll().Result;
        }

        public bool Create(Nationality nationality)
        {
           return  _nationalityRepository.Create(nationality);
        }

        public bool Update(Nationality nationality)
        {
           return  _nationalityRepository.Update(nationality);
        }

        public bool Delete(int id)
        {
            var nationality = _nationalityRepository.GetById(id)?.Result;
            if (nationality == null)
                return false;
            return _nationalityRepository.Delete(nationality);              
        }
    }
}
