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
using System.Reflection;
using Microsoft.IdentityModel.Protocols.WsTrust;
using System.Web.Helpers;

namespace HallManagement.Web.Controllers
{
    public class StaffController : BaseController
    {
        private readonly IStaffService _staffService;
        private readonly IDesignationService _designationService;
        private readonly IDepartmentService _departmentService;
        private IOptions<_AppSettings> _settings;
        private readonly ILogger<StaffController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public StaffController(IStaffService staffService, IDesignationService designationService, IDepartmentService departmentService, ILogger<StaffController> logger, IOptions<_AppSettings> settings, BangamataHallContext banamataHallContext)
        {
            _staffService = staffService;
            _departmentService = departmentService;
            _designationService = designationService;
            _settings = settings;
            _logger = logger;
            _banamataHallContext = banamataHallContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public IActionResult GetPagedData(int pageNumber, int pageSize)
        {
            var records = _banamataHallContext.Staff.ToList();
            var data = records.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    DesignationName = x?.Designation?.Name,
                    DepartmentName = x?.Department?.Name,
                    ActingDateFrom = x.ActingDateFrom,
                    ActingDateTo = x.ActingDateTo,
                    BioLink = x.BioLink,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    EntryByName = x?.EntryByNavigation?.Name,
                    EntryDate = x?.EntryDate,
                    ModifyByName = x?.ModifyByNavigation?.Name,
                    ModifyDate = x.ModifyDate,
                    DisplayOrder = x.DisplayOrder,
                    IsActive = x.IsActive == true ? "Yes" : "No",
                }).ToList();
            var totalRecords = records.Count();
            return Json(new { data, totalRecords });
        }

        public IActionResult Create()
        {
            var staffVm = new StaffVm();
            staffVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            staffVm.DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View(staffVm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffVm staffVm)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var staff = new Staff()
                {
                    Name = staffVm.Name,
                    BioLink = staffVm.BioLink,
                    ActingDateFrom = staffVm.ActingDateFrom,
                    ActingDateTo = staffVm.ActingDateTo,
                    DepartmentId = staffVm.DepartmentId,
                    DesignationId = staffVm.DesignationId,
                    Email = staffVm.Email,
                    Mobile = staffVm.Mobile,
                    IsActive = staffVm.IsActive,
                    DisplayOrder = staffVm.DisplayOrder,
                    EntryBy = _userId,
                    EntryDate = DateTime.Now,
                };
                try
                {
                    _staffService.Create(staff);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StaffEmail"))
                    {
                        ModelState.AddModelError(string.Empty, $"Staff insert failed. Same email address already used by other staff.");
                    }
                    else if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StaffMobile"))
                    {
                        ModelState.AddModelError(string.Empty, $"Staff insert failed. Same mobile number already used by other staff.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Staff insert failed. Please check all input fields.");
                    }
                    _logger.LogError(ex.ToString());
                    staffVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    staffVm.DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    return View(staffVm);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while staff record. Please try again with correct data.");
                    _logger.LogInformation("Staff information saving failed. " + ex.ToString());
                }
            }

            staffVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            staffVm.DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(staffVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = _staffService.GetById((int)id);
            if (staff == null)
            {
                return NotFound();
            }
            var staffVm = new StaffVm()
            {
                Name = staff.Name,
                BioLink = staff.BioLink,
                ActingDateFrom = staff.ActingDateFrom,
                ActingDateTo = staff.ActingDateTo,
                DepartmentId = staff.DepartmentId,
                DesignationId = staff.DesignationId,
                Email = staff.Email,
                Mobile = staff.Mobile,
                DisplayOrder = staff.DisplayOrder,
                IsActive = staff.IsActive,
                ModifyBy = staff.ModifyBy,
                ModifyDate = staff.ModifyDate,
                DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };
            return View(staffVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentId,DesignationId,Mobile,Email,BioLink,ActingDateFrom,ActingDateTo,EntryBy,EntryDate,ModifyBy,ModifyDate,DisplayOrder,IsActive")] StaffVm staffVm)
        {
            if (id != staffVm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                staffVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                staffVm.DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(staffVm);
            }
            try
            {
                var staffDb = _staffService.GetById(id);
                if (staffDb == null)
                {
                    return NotFound();
                }

                staffDb.Name = staffVm.Name;
                staffDb.BioLink = staffVm.BioLink;
                staffDb.ActingDateFrom = staffVm.ActingDateFrom;
                staffDb.ActingDateTo = staffVm.ActingDateTo;
                staffDb.DepartmentId = staffVm.DepartmentId;
                staffDb.DesignationId = staffVm.DesignationId;
                staffDb.Email = staffVm.Email;
                staffDb.Mobile = staffVm.Mobile;
                staffDb.DisplayOrder = staffVm.DisplayOrder;
                staffDb.IsActive = staffVm.IsActive;
                staffDb.ModifyBy = _userId;
                staffDb.ModifyDate = DateTime.Now;

                _staffService.Update(staffDb);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StaffEmail"))
                {
                    ModelState.AddModelError(string.Empty, $"Staff update failed. Same email address already used by other staff.");
                }
                else if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_StaffMobile"))
                {
                    ModelState.AddModelError(string.Empty, $"Staff update failed. Same mobile number already used by other staff.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Staff update failed. Please check all input fields.");
                }
                _logger.LogError(ex.ToString());
                staffVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                staffVm.DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(staffVm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating staff record. Please try again with correct data.");
                _logger.LogInformation("Staff information updating failed. " + ex.ToString());
                staffVm.DepartmentList = _departmentService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                staffVm.DesignationList = _designationService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(staffVm);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = _staffService.GetById((int)id);
            if (staff == null)
            {
                return NotFound();
            }

            var staffVm = new StaffDetailVm()
            {
                Name = staff.Name,
                BioLink = staff.BioLink,
                ActingDateFrom = staff.ActingDateFrom,
                ActingDateTo = staff.ActingDateTo,
                DepartmentName = staff?.Department?.Name,
                DesignationName = staff?.Designation?.Name,
                Email = staff.Email,
                Mobile = staff.Mobile,
                EntryBy = staff.EntryByNavigation?.Name,
                EntryDate = staff.EntryDate,
                ModifyBy = staff.ModifyByNavigation?.Name,
                ModifyDate = staff.ModifyDate,
                DisplayOrder = staff.DisplayOrder,
                IsActive = staff.IsActive == true ? "Yes" : "No",
            };

            return View(staffVm);
        }

        private void SetDropdownViewData()
        {
            //ViewData["Division"] = new SelectList(_divisionService.GetAll(), "DivisionId", "Name");
            //ViewData["BrachName"] = new SelectList(_reportService.GetBranchInfoList(), "BrCode", "BrName");
        }
    }
}
