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
using HallManagement.Service;

namespace HallManagement.Web.Controllers
{
    public class EstablishmentChargeController : BaseController
    {
        private readonly IEstablishmentChargeService _establishmentChargeService;
        private IOptions<_AppSettings> _settings;
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public EstablishmentChargeController(ILogger<StudentController> logger, IStudentService studentService, IOptions<_AppSettings> settings, IEstablishmentChargeService stablishmentChargeService, BangamataHallContext banamataHallContext)
        {
            _studentService = studentService;
            _settings = settings;
            _logger = logger;
            _establishmentChargeService = stablishmentChargeService;
            _banamataHallContext = banamataHallContext; 
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult GetPagedData(int pageNumber, int pageSize)
        {
            var records = _banamataHallContext.EstablishmentCharges.ToList();
            var data = records.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new
                {
                    Id = x.Id,
                    StudentName = x?.Student?.Name,
                    ClassRollNo = x?.Student?.ClassRollNo,
                    DepartmentName = x?.Student?.Department?.Name,
                    ChargeYear = x.Year,
                    PaidAmount = x.PaidAmount,
                }).ToList();
            var totalRecords = records.Count();
            return Json(new { data, totalRecords });
        }

        public IActionResult Create()
        {
            var establishmentChargeVm = new EstablishmentChargeVm();
            establishmentChargeVm.StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList();

            return View(establishmentChargeVm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstablishmentChargeVm establishmentChargeVm)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var stablishmentCharge = new EstablishmentCharge()
                {
                    EntryBy = _userId,
                    PaidAmount = establishmentChargeVm.PaidAmount,
                    StudentId = establishmentChargeVm.StudentId,
                    Year = establishmentChargeVm.Year,
                    EntryDate = DateTime.Now,
                };
                try
                {
                    _establishmentChargeService.Create(stablishmentCharge);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_EstablishmentChargeStudentIdYear"))
                    {
                        ModelState.AddModelError(string.Empty, $"Establishment Charge insert failed. This student already paid the establishment charge for year '{establishmentChargeVm.Year}'.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Establishment Charge insert failed. Please check all input fields.");
                    }
                    _logger.LogError(ex.ToString());
                    establishmentChargeVm.StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList();
                    return View(establishmentChargeVm);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while submitting establishment Charge record. Please try again with correct data.");
                    _logger.LogInformation("Establishment Charge information saving failed. " + ex.ToString());
                }
            }

            establishmentChargeVm.StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList();
            return View(establishmentChargeVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stablishmentCharge = _establishmentChargeService.GetById((int)id);
            if (stablishmentCharge == null)
            {
                return NotFound();
            }
            var studentVm = new EstablishmentChargeVm()
            {
                Year = stablishmentCharge.Year,
                PaidAmount = stablishmentCharge.PaidAmount,
                StudentId = stablishmentCharge.StudentId,
                StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList()
            };
            return View(studentVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Year,PaidAmount")] EstablishmentChargeVm establishmentChargeVm)
        {
            if (id != establishmentChargeVm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                establishmentChargeVm.StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList();
                return View(establishmentChargeVm);
            }
            try
            {
                var stablishmentChargeDb = _establishmentChargeService.GetById(id);
                if (stablishmentChargeDb == null)
                {
                    return NotFound();
                }

                stablishmentChargeDb.Id = (int)establishmentChargeVm.Id;
                stablishmentChargeDb.StudentId = establishmentChargeVm.StudentId;
                stablishmentChargeDb.PaidAmount = establishmentChargeVm.PaidAmount;
                stablishmentChargeDb.Year = establishmentChargeVm.Year;
                stablishmentChargeDb.ModifyBy = _userId;
                stablishmentChargeDb.ModifyDate = DateTime.Now;

                _establishmentChargeService.Update(stablishmentChargeDb);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_EstablishmentChargeStudentIdYear"))
                {
                    ModelState.AddModelError(string.Empty, $"Establishment Charge update failed. This student already paid the establishment charge for year '{establishmentChargeVm.Year}'.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Establishment Charge update failed. Please check all input fields.");
                }
                _logger.LogError(ex.ToString());
                establishmentChargeVm.StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList();
                return View(establishmentChargeVm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating establishment Charge record. Please try again with correct data.");
                _logger.LogInformation("Establishment Charge information updating failed. " + ex.ToString());
                establishmentChargeVm.StudentList = _studentService.GetAll().Select(x => new SelectListItem { Text = x.ClassRollNo, Value = x.Id.ToString() }).ToList();
                return View(establishmentChargeVm);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stablishmentCharge = _establishmentChargeService.GetById((int)id);
            if (stablishmentCharge == null)
            {
                return NotFound();
            }

            var establishmentChargeVm = new EstablishmentChargeDetailVm()
            {
                Id = stablishmentCharge.Id,
                PaidAmount = stablishmentCharge.PaidAmount,
                ClassRollNo = stablishmentCharge.Student?.ClassRollNo,
                Year = stablishmentCharge.Year,
                EntryBy = stablishmentCharge.EntryByNavigation?.Name,
                EntryDate = stablishmentCharge.EntryDate,
                ModifiedBy = stablishmentCharge.ModifyByNavigation?.Name,
                ModifyDate = stablishmentCharge.ModifyDate,
            };

            return View(establishmentChargeVm);
        }

        private void SetDropdownViewData()
        {
            //ViewData["Division"] = new SelectList(_divisionService.GetAll(), "DivisionId", "Name");
            //ViewData["BrachName"] = new SelectList(_reportService.GetBranchInfoList(), "BrCode", "BrName");
        }
    }
}
