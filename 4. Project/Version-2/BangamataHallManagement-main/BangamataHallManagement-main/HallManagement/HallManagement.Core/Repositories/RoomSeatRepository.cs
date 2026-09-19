using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class RoomSeatRepository: RepositoryBase<RoomSeat>, IRoomSeatRepository
    {
        BangamataHallContext _applicationContext;
        public RoomSeatRepository(BangamataHallContext applicationContext): base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IEnumerable<RoomSeatVm> GetRoomSeatsByStudentId(int studentId)
        {
            return _applicationContext.RoomSeats.Where(x => x.StudentId == studentId).OrderByDescending(x=>x.UpdateDate).Select(x=> new RoomSeatVm
            {
                Id = x.Id,
                RoomInfo = $"Selected student already allocated in Room: {x.Room.RoomNo}, Seat: {x.SeatNo}",
                StudentInfo = $"Selected room already allocated for student Name: {x.Student.Name}, Roll No.: {x.Student.ClassRollNo}, Email: {x.Student.Email}, Mobile: {x.Student.Mobile}",
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.UpdatedByNavigation.Name
            });
        }
    }
}
