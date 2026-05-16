using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<Section> GetAll();
        Section GetById(int Id);
        bool Create(Section section);
        bool Update(Section section);
        bool Delete(int id);
    }
}
