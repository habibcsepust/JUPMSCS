using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Web.Classes;
using Web.Models;
using Microsoft.AspNetCore.Http;
using HallManagement.Web.Controllers;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public DashboardController(ILogger<StudentController> logger, BangamataHallContext bangamataHallContext)
        {
            _logger = logger;
            _banamataHallContext = bangamataHallContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetStasData()
        {
            try
            {
                var students = _banamataHallContext.Students;
                var rooms = _banamataHallContext.Rooms;
                var roomSeats = _banamataHallContext.RoomSeats;

                var data = new
                {
                    TotalStudents = students.Count(),
                    ActiveStudents = students.Where(x => x.IsArchived != true).Count(),
                    InactiveStudents = students.Where(x => x.IsArchived == true).Count(),
                    TotalRooms = rooms.Count(),
                    TotalSeats = roomSeats.Count(),
                    AllocatedSeats = roomSeats.Where(x => x.StudentId != null).Count(),
                    AvailableSeats = roomSeats.Where(x => x.StudentId == null).Count(),
                    EstablishmentChargePaidThisYear = _banamataHallContext.EstablishmentCharges.Where(x => x.Year == DateTime.Now.Year).Count(),
                    TotalActiveStaffs = _banamataHallContext.Staff.Where(x => x.IsActive == true).Count(),
                };
                return Json(new { data, IsSuccess = true });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in stats data. " + ex.ToString());
            }
            return Json(new { data = "", IsSuccess = false });
        }
    }
}