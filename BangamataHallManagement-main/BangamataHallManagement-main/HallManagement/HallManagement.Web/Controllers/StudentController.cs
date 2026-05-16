using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;
using Web.Classes;
using Microsoft.Extensions.Options;
using HallManagement.Model.ViewModels;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

namespace HallManagement.Web.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IBatchService _batchService;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly IClassService _classService;
        private readonly IStaffService _staffService;
        private readonly INationalityService _nationalityService;
        private readonly IReligionService _religionService;
        private readonly ISectionService _sectionService;
        private readonly ISessionService _sessionService;
        private IOptions<_AppSettings> _settings;
        private readonly IReportService _reportService;
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService, IDepartmentService departmentService, IBatchService batchService, IBloodGroupService bloodGroupService, IClassService classService,
            IStaffService stafService, INationalityService nationalityService, IReligionService religionService, ISectionService sectionService, ISessionService sessionService, IReportService reportService, IOptions<_AppSettings> settings, BangamataHallContext bangamataHallContext)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _batchService = batchService;
            _bloodGroupService = bloodGroupService;
            _classService = classService;
            _staffService = stafService;
            _nationalityService = nationalityService;
            _religionService = religionService;
            _sectionService = sectionService;
            _sessionService = sessionService;
            _reportService = reportService;
            _settings = settings;
            _logger = logger;
            _banamataHallContext = bangamataHallContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult GetPagedData(int pageNumber, int pageSize)
        {
            var records = _banamataHallContext.Students.ToList();
            var data = records.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new
                {
                    Id = x.Id,
                    StudentName = x.Name,
                    DepartmentName = x?.Department?.Name,
                    ClassName = x?.Class?.Name,
                    SessionName = x?.Session?.Name,
                    SectionName = x?.Section?.Name,
                    BatchName = x?.Batch?.Name,
                    ClassRollNo = x.ClassRollNo,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    IsArchived = x.IsArchived == true ? "Yes" : "No",
                }).ToList();
            var totalRecords = records.Count();
            return Json(new { data, totalRecords });
        }

        public IActionResult Create()
        {
            var studentVm = new StudentVm();
            studentVm.BatchList = _batchService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.SessionList = _sessionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.SectionList = _sectionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.NationalityList = _nationalityService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x=>x.Value).ToList();
            studentVm.ReligionList = _religionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.BloodGroupList = _bloodGroupService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.StafList = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.ClassList = _classService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View(studentVm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVm studentVm)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    NameInEnglish = studentVm.NameInEnglish,
                    Name = studentVm.Name,
                    FatherName = studentVm.FatherName,
                    MotherName = studentVm.MotherName,
                    BatchId = studentVm.BatchId,
                    BloodGroupId = studentVm.BloodGroupId,
                    ClassId = studentVm.ClassId,
                    ClassRollNo = studentVm.ClassRollNo,
                    DateOfBirth = studentVm.DateOfBirth,
                    DepartmentId = studentVm.DepartmentId,
                    Email = studentVm.Email,
                    EntryDate = DateTime.Now,
                    Mobile = studentVm.Mobile,
                    NationalityId = studentVm.NationalityId,
                    RegistrationNo = studentVm.RegistrationNo,
                    RegistrationYear = studentVm.RegistrationYear,
                    ReligionId = studentVm.ReligionId,
                    SessionId = studentVm.SessionId,
                    SectionId = studentVm.SectionId,
                    Password = CryptoUtility.EncryptText("12345678"),
                    IsPasswordResetDone = false,
                };
                try
                {
                    _studentService.Create(student);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StudentMobile"))
                    {
                        ModelState.AddModelError(string.Empty, "Student insert failed. Another student already exists with same mobile.");
                    }
                    else if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StudentEmail"))
                    {
                        ModelState.AddModelError(string.Empty, "Student insert failed. Another student already exists with same email.");
                    }
                    else if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StudentClassRollNo"))
                    {
                        ModelState.AddModelError(string.Empty, "Student insert failed. Another student already exists with same roll.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Student insert failed. Please check all input fields.");
                    }
                    _logger.LogError(ex.ToString());
                    studentVm.BatchList = _batchService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.SessionList = _sessionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.SectionList = _sectionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.NationalityList = _nationalityService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.ReligionList = _religionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.BloodGroupList = _bloodGroupService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.StafList = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    studentVm.ClassList = _classService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    return View(studentVm);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while submitting student record. Please try again later.");
                    _logger.LogInformation("Student information saving failed. " + ex.ToString());
                }
            }

            studentVm.BatchList = _batchService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.SessionList = _sessionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.SectionList = _sectionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.NationalityList = _nationalityService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.ReligionList = _religionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.BloodGroupList = _bloodGroupService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.StafList = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.ClassList = _classService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(studentVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById((int)id);
            if (student == null)
            {
                return NotFound();
            }
            var studentVm = new StudentVm()
            {
                BatchId = student.BatchId,
                BloodGroupId = student.BloodGroupId,
                ClassId = student.ClassId,
                ClassRollNo = student.ClassRollNo,
                DateOfBirth = student.DateOfBirth,
                DepartmentId = student.DepartmentId,
                Email = student.Email,
                FatherName = student.FatherName,
                Id = student.Id,
                IsArchived = student.IsArchived,
                Mobile = student.Mobile,
                MotherName = student.MotherName,
                NameInEnglish = student.NameInEnglish,
                Name = student.Name,
                NationalityId = student.NationalityId,
                RegistrationNo = student.RegistrationNo,
                RegistrationYear = student.RegistrationYear,
                ReligionId = student.ReligionId,
                SectionId = student.SectionId,
                SessionId = student.SessionId
            };
            studentVm.BatchList = _batchService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.SessionList = _sessionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.SectionList = _sectionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.NationalityList = _nationalityService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.ReligionList = _religionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.BloodGroupList = _bloodGroupService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.StafList = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            studentVm.ClassList = _classService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(studentVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameInEnglish,Name,FatherName,MotherName,ClassRollNo,RegistrationNo,ClassId,DepartmentId,BatchId,SectionId,SessionId,RegistrationYear,DateOfBirth,Mobile,Email,ReligionId,NationalityId,BloodGroupId,Password,EntryDate,EntryBy,ModifiedBy,ModifyDate,IsArchived")] StudentVm studentVm)
        {
            if (id != studentVm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                studentVm.BatchList = _batchService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.SessionList = _sessionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.SectionList = _sectionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.NationalityList = _nationalityService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.ReligionList = _religionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.BloodGroupList = _bloodGroupService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.StafList = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.ClassList = _classService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(studentVm);
            }
            try
            {
                var studentDb = _studentService.GetById(id);
                if (studentDb == null)
                {
                    return NotFound();
                }

                studentDb.Id = (int)studentVm.Id;
                studentDb.BatchId = studentVm.BatchId;
                studentDb.BloodGroupId = studentVm.BloodGroupId;
                studentDb.ClassId = studentVm.ClassId;
                studentDb.ClassRollNo = studentVm.ClassRollNo;
                studentDb.DateOfBirth = studentVm.DateOfBirth;
                studentDb.DepartmentId = studentVm.DepartmentId;
                studentDb.Email = studentVm.Email;
                studentDb.FatherName = studentVm.FatherName;
                studentDb.MotherName = studentVm.MotherName;
                studentDb.IsArchived = studentVm.IsArchived;
                studentDb.Mobile = studentVm.Mobile;
                studentDb.ModifiedBy = _userId;
                studentDb.ModifyDate = DateTime.Now;
                studentDb.NameInEnglish = studentVm.NameInEnglish;
                studentDb.Name = studentVm.Name;
                studentDb.NationalityId = studentVm.NationalityId;
                studentDb.RegistrationNo = studentVm.RegistrationNo;
                studentDb.RegistrationYear = studentVm.RegistrationYear;
                studentDb.ReligionId = studentVm.ReligionId;
                studentDb.SectionId = studentVm.SectionId;
                studentDb.SessionId = studentVm.SessionId;
                _studentService.Update(studentDb);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StudentMobile"))
                {
                    ModelState.AddModelError(string.Empty, "Student update failed. Another student already exists with same mobile.");
                }
                else if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StudentEmail"))
                {
                    ModelState.AddModelError(string.Empty, "Student update failed. Another student already exists with same email.");
                }
                else if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StudentClassRollNo"))
                {
                    ModelState.AddModelError(string.Empty, "Student update failed. Another student already exists with same roll.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Student update failed. Please check all input fields.");
                }
                _logger.LogError(ex.ToString());
                studentVm.BatchList = _batchService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.SessionList = _sessionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.SectionList = _sectionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.NationalityList = _nationalityService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.ReligionList = _religionService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.BloodGroupList = _bloodGroupService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.StafList = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                studentVm.ClassList = _classService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(studentVm);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById((int)id);
            if (student == null)
            {
                return NotFound();
            }

            var studentVm = new StudentDetailVm()
            {
                BatchName = student.Batch?.Name,
                BloodGroupName = student.BloodGroup?.Name,
                ClassName = student.Class?.Name,
                ClassRollNo = student.ClassRollNo,
                DateOfBirth = student.DateOfBirth,
                DepartmentName = student.Department?.Name,
                Email = student.Email,
                FatherName = student.FatherName,
                Id = student.Id,
                IsArchived = student.IsArchived == true ? "Yes" : "No",
                Mobile = student.Mobile,
                MotherName = student.MotherName,
                NameInEnglish = student.NameInEnglish,
                Name = student.Name,
                NationalityName = student.Nationality?.Name,
                RegistrationNo = student.RegistrationNo,
                RegistrationYear = student.RegistrationYear,
                ReligionName = student.Religion?.Name,
                SectionName = student.Section?.Name,
                SessionName = student.Session?.Name,
                EntryBy = student.EntryByNavigation?.Name,
                EntryDate = student.EntryDate,
                ModifiedBy = student.ModifiedByNavigation?.Name,
                ModifyDate = student.ModifyDate
            };

            return View(studentVm);
        }

        public async Task<IActionResult> MyDetails(string id)
        {
            try
            {
                var intId = int.Parse(CryptoUtility.DecryptText(HttpUtility.UrlDecode(id)));
                if (intId == 0)
                {
                    return NotFound();
                }

                var student = _studentService.GetById(intId);
                if (student == null)
                {
                    return NotFound();
                }

                var studentVm = new StudentDetailVm()
                {
                    BatchName = student.Batch?.Name,
                    BloodGroupName = student.BloodGroup?.Name,
                    ClassName = student.Class?.Name,
                    ClassRollNo = student.ClassRollNo,
                    DateOfBirth = student.DateOfBirth,
                    DepartmentName = student.Department?.Name,
                    Email = student.Email,
                    FatherName = student.FatherName,
                    Id = student.Id,
                    IsArchived = student.IsArchived == true ? "Yes" : "No",
                    Mobile = student.Mobile,
                    MotherName = student.MotherName,
                    NameInEnglish = student.NameInEnglish,
                    Name = student.Name,
                    NationalityName = student.Nationality?.Name,
                    RegistrationNo = student.RegistrationNo,
                    RegistrationYear = student.RegistrationYear,
                    ReligionName = student.Religion?.Name,
                    SectionName = student.Section?.Name,
                    SessionName = student.Session?.Name,
                    EntryBy = student.EntryByNavigation?.Name,
                    EntryDate = student.EntryDate,
                    ModifiedBy = student.ModifiedByNavigation?.Name,
                    ModifyDate = student.ModifyDate
                };
                return View("Details", studentVm);
            }
            catch
            {
                return NotFound();
            }
        }

        private void SetDropdownViewData()
        {
            //ViewData["Division"] = new SelectList(_divisionService.GetAll(), "DivisionId", "Name");
            //ViewData["BrachName"] = new SelectList(_reportService.GetBranchInfoList(), "BrCode", "BrName");
        }
    }
}
