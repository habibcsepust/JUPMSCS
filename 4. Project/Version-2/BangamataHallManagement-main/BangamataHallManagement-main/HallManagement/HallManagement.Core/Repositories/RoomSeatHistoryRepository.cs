using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class RoomSeatHistoryRepository : RepositoryBase<RoomSeatHistory>, IRoomSeatHistoryRepository
    {
        BangamataHallContext _applicationContext;
        public RoomSeatHistoryRepository(BangamataHallContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryBySeatId(int seatId)
        {
            var roomSeatHisotry = _applicationContext.RoomSeatHistories.Where(x => x.Id == seatId).OrderByDescending(x => x.LogId).Select(x => new RoomSeatHistoryVm
            {
                RoomNo = x.IdNavigation.Room.RoomNo,
                SeatNo = x.SeatNo,
                StudentInfo = x.IdNavigation.Student.Name,
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.IdNavigation.UpdatedByNavigation.Name
            }).ToList();
            return roomSeatHisotry;
        }

        public IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryByStudentId(int studentId)
        {
            var roomSeatHisotry = _applicationContext.RoomSeatHistories.Where(x => x.StudentId == studentId).OrderByDescending(x => x.LogId).Select(x => new RoomSeatHistoryVm
            {
                RoomNo = x.IdNavigation.Room.RoomNo,
                SeatNo = x.SeatNo,
                StudentInfo = $"Student Name:{x.IdNavigation.Student.Name} | Roll No.:{x.IdNavigation.Student.ClassRollNo} | Email:{x.IdNavigation.Student.Email} | Mobile:{x.IdNavigation.Student.Mobile}",
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.IdNavigation.UpdatedByNavigation.Name
            }).ToList();
            return roomSeatHisotry;
        }
    }
}
