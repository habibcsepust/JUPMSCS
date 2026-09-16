using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IBatchService
    {
        IEnumerable<Batch> GetAll();
        Batch GetById(int Id);
        bool Create(Batch batch);
        bool Update(Batch batch);
        bool Delete(int id);
    }
}
