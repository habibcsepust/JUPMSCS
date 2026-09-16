using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class SectionService : ISectionService
    {

        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public Section GetById(int Id)
        {
            return _sectionRepository.GetById(Id).Result;
        }

        public IEnumerable<Section> GetAll()
        {
            return _sectionRepository.GetAll().Result;
        }

        public bool Create(Section section)
        {
           return  _sectionRepository.Create(section);
        }

        public bool Update(Section section)
        {
           return  _sectionRepository.Update(section);
        }

        public bool Delete(int id)
        {
            var section = _sectionRepository.GetById(id)?.Result;
            if (section == null)
                return false;
            return _sectionRepository.Delete(section);              
        }
    }
}
