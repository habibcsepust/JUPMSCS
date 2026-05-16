using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student GetById(int Id)
        {
            return _studentRepository.GetById(Id).Result;
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll().Result;
        }

        public bool Create(Student student)
        {
           return  _studentRepository.Create(student);
        }

        public bool Update(Student student)
        {
           return  _studentRepository.Update(student);
        }

        public bool Delete(int id)
        {
            var student = _studentRepository.GetById(id)?.Result;
            if (student == null)
                return false;
            return _studentRepository.Delete(student);              
        }
    }
}
