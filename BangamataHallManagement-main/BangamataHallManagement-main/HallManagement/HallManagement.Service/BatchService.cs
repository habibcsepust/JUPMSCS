using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class BatchService : IBatchService
    {

        private readonly IBatchRepository _batchRepository;

        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public Batch GetById(int Id)
        {
            return _batchRepository.GetById(Id).Result;
        }

        public IEnumerable<Batch> GetAll()
        {
            return _batchRepository.GetAll().Result;
        }

        public bool Create(Batch batch)
        {
           return  _batchRepository.Create(batch);
        }

        public bool Update(Batch batch)
        {
           return  _batchRepository.Update(batch);
        }

        public bool Delete(int id)
        {
            var batch = _batchRepository.GetById(id)?.Result;
            if (batch == null)
                return false;
            return _batchRepository.Delete(batch);              
        }
    }
}
