using HallManagement.Core.Interfaces;
using HallManagement.Core.Repositories;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class HallService : IHallService
    {

        private readonly IHallRepository _hallRepository;

        public HallService(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public Hall GetById(int Id)
        {
            return _hallRepository.GetById(Id).Result;
        }
        //public Department GetByEmailOrPhone(string emailOrPhone)
        //{
        //    return _departmentRepository.GetByEmailOrPhone(emailOrPhone);
        //}

        //public bool IsReferenceNoExists(string referenceNo, int? id)
        //{
        //    return _departmentRepository.IsReferenceNoExists(referenceNo, id);
        //}

        public IEnumerable<Hall> GetAll()
        {
            return _hallRepository.GetAll().Result;
        }

        public bool Create(Hall hall)
        {
           return _hallRepository.Create(hall);
        }

        public bool Update(Hall hall)
        {
           return _hallRepository.Update(hall);
        }

        public bool Delete(int id)
        {
            var hall = _hallRepository.GetById(id)?.Result;
            if (hall == null)
                return false;
            return _hallRepository.Delete(hall);              
        }
        

    }
}
