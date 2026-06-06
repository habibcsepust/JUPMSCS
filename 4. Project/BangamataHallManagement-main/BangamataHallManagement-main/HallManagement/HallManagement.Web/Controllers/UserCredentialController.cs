using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;
using Web.Classes;
using Microsoft.Extensions.Options;
using HallManagement.Model.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace HallManagement.Web.Controllers
{
    public class UserCredentialController : BaseController
    {
        private readonly IUserCredentialService _userCredentialService;
        private readonly IRoleService _roleService;
        private readonly IStaffService _staffService;
        private IOptions<_AppSettings> _settings;
        private object student;
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public UserCredentialController(IUserCredentialService userCredentialService, IRoleService roleService, ILogger<StudentController> logger, IStaffService stafService, IOptions<_AppSettings> settings, BangamataHallContext banamataHallContext)
        {
            _userCredentialService = userCredentialService;
            _roleService = roleService;
            _staffService = stafService;
            _settings = settings;
            _logger = logger;
            _banamataHallContext = banamataHallContext;
        }

        public IActionResult Index()
        {
            //var userCredentials = _userCredentialService.GetAll().ToList();
            return View();
        }

        public IActionResult GetPagedData(int pageNumber, int pageSize)
        {
            var records = _banamataHallContext.UserCredentials.ToList();
            var data = records.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new
                {
                    Id = x.Id,
                    Password = x.Password != null ? CryptoUtility.DecryptText(x.Password) : "",
                    StaffName = x?.Staff?.Name,
                    Role = x?.Role?.Name,
                    EntryBy = x?.EntryByNavigation?.Name,
                    EntryDate = x.EntryDate,
                    ModifyBy = x?.ModifyByNavigation?.Name,
                    ModifyDate = x?.ModifyDate,
                    IsEnabled = x?.IsEnabled == true ? "Yes" : "No",
                }).ToList();
            var totalRecords = records.Count();
            return Json(new { data, totalRecords });
        }

        public IActionResult Create(int? id)
        {
            var userCredential = new UserCredentialVm();
            if (id != null)
            {
                var userCredentialDb = _userCredentialService.GetById((int)id);
                if (userCredentialDb == null)
                {
                    return NotFound();
                }
                userCredential.Id = userCredentialDb.Id;
                //userCredential.UserName = userCredentialDb.UserName;
                //userCredential.Password = userCredentialDb.Password;
                userCredential.StaffId = userCredentialDb.StaffId;
                userCredential.RoleId = userCredentialDb.RoleId;
                userCredential.IsEnabled = userCredentialDb.IsEnabled;
            }
            userCredential.Staffs = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Mobile, Value = x.Id.ToString() }).ToList();
            userCredential.Roles = _roleService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View(userCredential);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCredentialVm userCredentialVm)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var userCredential = new UserCredential()
                {
                    //UserName = userCredentialVm.UserName,
                    Password = CryptoUtility.EncryptText("12345678"),
                    RoleId = userCredentialVm.RoleId,
                    StaffId = userCredentialVm.StaffId,
                    EntryBy = _userId,
                    EntryDate = DateTime.Now,
                    IsEnabled = (bool)userCredentialVm.IsEnabled,
                };
                try
                {
                    _userCredentialService.Create(userCredential);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_UserCredentialStaffId"))
                    {
                        ModelState.AddModelError(string.Empty, $"User credential insert failed. User credentials alredy exists for this staft.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User credential insert failed. Please check all input fields.");
                    }
                    _logger.LogError(ex.ToString());
                    userCredentialVm.Staffs = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Mobile, Value = x.Id.ToString() }).ToList();
                    userCredentialVm.Roles = _roleService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    return View(userCredentialVm);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while submitting user credential record. Please try again later.");
                    _logger.LogInformation("User credential saving failed. " + ex.ToString());
                }
            }

            userCredentialVm.Staffs = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Mobile, Value = x.Id.ToString() }).ToList();
            userCredentialVm.Roles = _roleService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(userCredentialVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCredential = _userCredentialService.GetById((int)id);
            if (userCredential == null)
            {
                return NotFound();
            }
            var userCredentialVm = new UserCredentialVm()
            {
                ModifyByStaffName = userCredential.ModifyByNavigation?.Name,
                //UserName = userCredential.UserName,
                StaffId = userCredential.StaffId,
                RoleId = userCredential.RoleId,
                EntryBy = userCredential.EntryBy,
                EntryByStaffName = userCredential.EntryByNavigation?.Name,
                EntryDate = userCredential.EntryDate,
                IsEnabled = userCredential.IsEnabled,
                Id = userCredential.Id,
                ModifyBy = userCredential.ModifyBy,
                ModifyDate = userCredential.ModifyDate,
                //Password = CryptoUtility.DecryptText(userCredential.Password),
            };
            userCredentialVm.Staffs = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Mobile, Value = x.Id.ToString() }).ToList();
            userCredentialVm.Roles = _roleService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View(userCredentialVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, UserName, Password, StaffId, RoleId, EntryBy, EntryDate, ModifyBy, ModifyDate, IsEnabled")] UserCredentialVm userCredentialVm)
        {
            if (id != userCredentialVm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                userCredentialVm.Staffs = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Mobile, Value = x.Id.ToString() }).ToList();
                userCredentialVm.Roles = _roleService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(userCredentialVm);
            }
            try
            {
                var userCredential = _userCredentialService.GetById(id);
                if (userCredential == null)
                {
                    return NotFound();
                }

                userCredential.Id = userCredentialVm.Id;
                userCredential.IsEnabled = (bool)userCredentialVm.IsEnabled;
                userCredential.StaffId = userCredentialVm.StaffId;
                userCredential.RoleId = userCredentialVm.RoleId;
                userCredential.ModifyBy = _userId;
                userCredential.ModifyDate = DateTime.Now;
                //userCredential.Password = CryptoUtility.EncryptText(userCredentialVm.Password);
                //userCredential.UserName = userCredentialVm.UserName;

                _userCredentialService.Update(userCredential);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_UserCredentialStaffId"))
                {
                    ModelState.AddModelError(string.Empty, $"User credential update failed. User credentials alredy exists for this staft.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User credential update failed. Please check all input fields.");
                }
                _logger.LogError(ex.ToString());
                userCredentialVm.Staffs = _staffService.GetAll().Select(x => new SelectListItem { Text = x.Mobile, Value = x.Id.ToString() }).ToList();
                userCredentialVm.Roles = _roleService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(userCredentialVm);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCredential = _userCredentialService.GetById((int)id);
            if (student == null)
            {
                return NotFound();
            }

            var userCredentialVm = new UserCredentialVm()
            {
                //UserName = userCredential.UserName,
                //Password = userCredential.Password,
                EntryByStaffName = userCredential.EntryByNavigation?.Name,
                EntryDate = userCredential.EntryDate,
                Id = userCredential.Id,
                IsEnabled = userCredential.IsEnabled,
                ModifyByStaffName = userCredential.ModifyByNavigation?.Name,
                ModifyDate = userCredential.ModifyDate,
                RoleId = userCredential.RoleId,
                StaffName = userCredential.Staff?.Name,
                RoleName = userCredential.Role?.Name,
            };

            return View(userCredentialVm);
        }
    }
}
