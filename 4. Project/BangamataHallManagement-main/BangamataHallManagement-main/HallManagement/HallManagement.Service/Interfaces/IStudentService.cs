using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        Student GetById(int Id);
        bool Create(Student student);
        bool Update(Student student);
        bool Delete(int id);
    }
}
