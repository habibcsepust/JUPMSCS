using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;
using Web.Classes;
using Microsoft.Extensions.Options;
using HallManagement.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HallManagement.Web.Controllers
{
    public class RoomSeatController : BaseController
    {
        private readonly IStaffService _staffService;
        private IOptions<_AppSettings> _settings;
        private readonly IReportService _reportService;
        private readonly IStudentService _studentService;
        private readonly IRoomSeatService _roomSeatService;
        private readonly IRoomSeatHistoryService _roomSeatHistoryService;
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public RoomSeatController(ILogger<StudentController> logger, IStudentService studentService, IRoomSeatService roomSeatService, IStaffService staffService, IReportService reportService, IRoomSeatHistoryService roomSeatHistoryService, IOptions<_AppSettings> settings, BangamataHallContext banamataHallContext)
        {
            _studentService = studentService;
            _staffService = staffService;
            _reportService = reportService;
            _roomSeatService = roomSeatService;
            _settings = settings;
            _roomSeatHistoryService = roomSeatHistoryService;
            _logger = logger;
            _banamataHallContext = banamataHallContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult GetPagedData(int pageNumber, int pageSize)
        {
            var records = _banamataHallContext.RoomSeats.ToList();
            var data = records.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new 
                { 
                    SeatId = x.Id, 
                    RoomNo = x?.Room?.RoomNo, 
                    SeatNo = x.SeatNo, 
                    StudentName = x?.Student?.Name, 
                    Email = x?.Student?.Email, 
                    RollNo = x?.Student?.ClassRollNo, 
                    Mobile = x?.Student?.Mobile 
                }).ToList();
            var totalRecords = records.Count();
            return Json(new { data, totalRecords });
        }

        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                var roomSeat = _roomSeatService.GetById((int)id);
                if (roomSeat == null)
                    return NotFound();
                else
                {
                    var seatAllocationObj = new RoomSeatEntryVm();
                    seatAllocationObj.Id = roomSeat.Id;
                    seatAllocationObj.StudentId = roomSeat.StudentId;
                    seatAllocationObj.RoomId = roomSeat.RoomId;
                    seatAllocationObj.StudentsDdl = _studentService.GetAll().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.ClassRollNo}_{x.Name}_{x.Mobile}" }).OrderBy(x => x.Text).ToList();
                    seatAllocationObj.RoomSeatsDdl = _banamataHallContext.RoomSeats.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Room.RoomNo + " " + x.SeatNo }).ToList();
                    return View(seatAllocationObj);
                }
            }
            var seatAllocation = new RoomSeatEntryVm();
            seatAllocation.StudentsDdl = _studentService.GetAll().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.ClassRollNo}_{x.Name}_{x.Mobile}" }).OrderBy(x => x.Text).ToList();
            seatAllocation.RoomSeatsDdl = _banamataHallContext.RoomSeats.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Room.RoomNo + " " + x.SeatNo }).ToList();
            return View(seatAllocation);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomSeatEntryVm seatAllocation)
        {
            if (!ModelState.IsValid)
            {
                seatAllocation.StudentsDdl = _studentService.GetAll().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.ClassRollNo}_{x.Name}_{x.Mobile}" }).OrderBy(x => x.Text).ToList();
                seatAllocation.RoomSeatsDdl = _banamataHallContext.RoomSeats.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Room.RoomNo + " " + x.SeatNo }).ToList();

                return View(seatAllocation);
            }
            var roomSeat = _roomSeatService.GetById(seatAllocation.Id);
            if (roomSeat.StudentId == seatAllocation.StudentId && roomSeat.Id == seatAllocation.Id)
            {
                ModelState.AddModelError(string.Empty, $"Room allocation failed. Room:{roomSeat.Room?.RoomNo} {roomSeat.SeatNo} already allocated to the selected student.");
                seatAllocation.StudentsDdl = _studentService.GetAll().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.ClassRollNo}_{x.Name}_{x.Mobile}" }).OrderBy(x => x.Text).ToList();
                seatAllocation.RoomSeatsDdl = _banamataHallContext.RoomSeats.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Room.RoomNo + " " + x.SeatNo }).ToList();
                return View(seatAllocation);
            }

            try
            {
                var dt = DateTime.Now;
                if (seatAllocation.StudentId != null)
                {
                    var student = _studentService.GetById((int)seatAllocation.StudentId);
                    if (student?.RoomSeat?.Id > 0)
                    {
                        var studentExistingSeat = _roomSeatService.GetById((int)student?.RoomSeat?.Id);
                        studentExistingSeat.StudentId = null;
                        studentExistingSeat.UpdateDate = dt;
                        studentExistingSeat.UpdatedBy = _userId;
                        _roomSeatService.Update(studentExistingSeat);
                    }
                }
                roomSeat.StudentId = seatAllocation.StudentId;
                roomSeat.UpdateDate = dt;
                roomSeat.UpdatedBy = _userId;
                _roomSeatService.Update(roomSeat);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while allocating student seat. Please try again later.");
                _logger.LogInformation("Student seat allocation saving failed. " + ex.ToString());
                seatAllocation.StudentsDdl = _studentService.GetAll().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.ClassRollNo}_{x.Name}_{x.Mobile}" }).OrderBy(x => x.Text).ToList();
                seatAllocation.RoomSeatsDdl = _banamataHallContext.RoomSeats.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Room.RoomNo + " " + x.SeatNo }).ToList();
                return View(seatAllocation);
            }
            return RedirectToAction("Index");
        }

        private void SetDropdownViewData()
        {
            //ViewData["Division"] = new SelectList(_divisionService.GetAll(), "DivisionId", "Name");
            //ViewData["BrachName"] = new SelectList(_reportService.GetBranchInfoList(), "BrCode", "BrName");
        }

        public IActionResult GetRoomSeatBySeatId(int seatId)
        {
            var roomSeat = _roomSeatService.GetById(seatId);
            if (roomSeat.StudentId == null)
            {
                return Json(new
                {
                    data = new List<RoomSeatVm> { }
                });
            }
            return Json(new
            {
                data = new List<RoomSeatVm> {
                new RoomSeatVm
                {
                    Id = roomSeat.Id,
                    RoomInfo = $"Selected student already allocated in Room: {roomSeat.Room.RoomNo}, Seat: {roomSeat.SeatNo}",
                    StudentInfo = $"Selected room already allocated for student Name: {roomSeat?.Student?.Name}, Roll No.: {roomSeat?.Student?.ClassRollNo}, Email: {roomSeat?.Student?.Email}, Mobile: {roomSeat?.Student?.Mobile}",
                    UpdateDate = roomSeat.UpdateDate,
                    UpdatedBy = roomSeat?.UpdatedByNavigation?.Name
                }}
            });
        }

        public IActionResult GetRoomSeatsByStudentId(int studentId)
        {
            var roomSeats = _roomSeatService.GetRoomSeatsByStudentId(studentId);
            return Json(new { data = roomSeats });
        }
    }
}
