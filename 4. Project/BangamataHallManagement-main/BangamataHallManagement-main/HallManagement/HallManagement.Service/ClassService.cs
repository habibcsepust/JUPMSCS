using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class ClassService : IClassService
    {

        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public Class GetById(int Id)
        {
            return _classRepository.GetById(Id).Result;
        }

        public IEnumerable<Class> GetAll()
        {
            return _classRepository.GetAll().Result;
        }

        public bool Create(Class classs)
        {
           return  _classRepository.Create(classs);
        }

        public bool Update(Class classs)
        {
           return  _classRepository.Update(classs);
        }

        public bool Delete(int id)
        {
            var classs = _classRepository.GetById(id)?.Result;
            if (classs == null)
                return false;
            return _classRepository.Delete(classs);              
        }
    }
}
