using HallManagement.Model;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Service;
using HallManagement.Service.Interfaces;
using HallManagement.Web.Classes;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web.Helpers;
using System.Xml.Linq;
using Web.Classes;

namespace HallManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserCredentialService _userCredentialService;
        private IMenuRoleService _menuRoleService;
        private IPasswordResetHistoryService _passwordResetHistoryService;
        private IOptions<_AppSettings> _settings;
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;

        public AccountController(IUserCredentialService userCredentialService, IMenuRoleService menuRoleService, IOptions<_AppSettings> settings, ILogger<StudentController> logger, BangamataHallContext banamataHallContext, IPasswordResetHistoryService passwordResetHistoryService)
        {
            _userCredentialService = userCredentialService;
            _menuRoleService = menuRoleService;
            _settings = settings;
            _logger = logger;
            _banamataHallContext = banamataHallContext;
            _passwordResetHistoryService = passwordResetHistoryService;
        }

        public async Task<IActionResult> Index()
        {
            var sms = new SmsApiClient(_settings.Value.SmsApiKey, _settings.Value.SmsApiKey, _settings.Value.SmsApiUrl);
            var smsResponse = await sms.SendSms("01531529204", "Hello");
            var result = smsResponse;
            return View("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login", new LoginViewModel() { IsStudentLogin = false });
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userCred = new LoggedUserVm();
                if (IsValidUser(model.UserName, model.Password, model.IsStudentLogin, out userCred))
                {
                    HttpContext.Session.SetInt32("Id", (int)userCred.Id);
                    HttpContext.Session.SetString("Name", userCred.Name);
                    HttpContext.Session.SetInt32("RoleId", (int)userCred.RoleId);
                    HttpContext.Session.SetString("RoleName", userCred.RoleName);
                    if (userCred.IsPasswordResetDone != true)
                        return View("PasswordReset", new ResetPasswordViewModel { Id = model.IsStudentLogin ? userCred.Id : userCred.UserCredentialId, IsStudentLogin = model.IsStudentLogin });

                    var menus = _menuRoleService.GetMenuItems((int)userCred.RoleId);
                    HttpContext.Session.SetString("MenuAccess", Newtonsoft.Json.JsonConvert.SerializeObject(menus));
                    if (model.IsStudentLogin)
                        return RedirectToAction("MyDetails", "Student", new { id = CryptoUtility.EncryptText(userCred.Id.ToString()) });

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    // Authentication failed, add a model error
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay the form
            return View(model);
        }

        private bool IsValidUser(string username, string password, bool isStudentLogin, out LoggedUserVm userCredential)
        {
            userCredential = _userCredentialService.IsUserExists(username, CryptoUtility.EncryptText(password), isStudentLogin);
            return userCredential != null;
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            int userId;
            if (!ModelState.IsValid)
                return View(model);
            var userMessage = "If you have an account with us, we have sent an SMS in your registered mobile number with the instructions to reset your password. Please check your SMS.";
            if (model.IsStudentLogin)
            {
                var studentObj = _banamataHallContext.Students.FirstOrDefault(x => x.Mobile == model.Mobile);
                if (studentObj == null)
                    return RedirectToAction("ForgotConfirm", new { message = userMessage });
                userId = studentObj.Id;
            }
            else
            {
                var staffObj = _banamataHallContext.Staff.FirstOrDefault(x => x.Mobile == model.Mobile);
                if (staffObj == null)
                    return RedirectToAction("ForgotConfirm", new { message = userMessage });
                userId = staffObj.Id;
            }
            var link = GenerateLink(model.Mobile, model.IsStudentLogin);
            var response = new SmsApiClient(_settings.Value.SmsApiKey, _settings.Value.SmsApiSecret, _settings.Value.BaseUrl).SendSms(model.Mobile, $"Reset password through link {link} within {_settings.Value.ForgotLinkExpiryTimeout / 60} minutes");
            if (!response.Result.IsSuccess)
            {
                userMessage = response.Result.Error;
                _logger.LogError(response.Result.Error);
            }
            try
            {
                _passwordResetHistoryService.Create(new PasswordResetHistory { HashedPasswordResetLink = link, CreateDate = DateTime.Now, ExpiryDateTime = DateTime.Now.AddSeconds(_settings.Value.ForgotLinkExpiryTimeout), IsStudent = model.IsStudentLogin, UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create {ex.Message}");
            }
            return RedirectToAction("ForgotConfirm", new { message = userMessage });
        }

        public ActionResult ForgotConfirm(string message)
        {
            ViewBag.UserMessage = message;
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("RoleId");
            HttpContext.Session.Remove("MenuAccess");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string GenerateLink(string mobileNumber, bool isStudentLogin)
        {
            var userType = isStudentLogin ? LoginAs.Student.ToString() : "";
            var encryptedPart = CryptoUtility.EncryptText($"m={mobileNumber}&t={userType}&ex={DateTime.Now.AddSeconds(_settings.Value.ForgotLinkExpiryTimeout)}");
            var link = $"{_settings.Value.BaseUrl}account/verify?p={encryptedPart}";
            return link;
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
                return View("PasswordReset", resetPassword);
            if (resetPassword.NewPassword != resetPassword.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "New passord & confirm password doesn't match.");
                return View("PasswordReset", resetPassword);
            }

            if (resetPassword.IsStudentLogin)
            {
                var student = _banamataHallContext.Students.Where(x => x.Id == resetPassword.Id).FirstOrDefault();
                if (student == null)
                {
                    return NotFound();
                }

                student.Password = CryptoUtility.EncryptText(resetPassword.NewPassword);
                student.IsPasswordResetDone = true;
            }
            else
            {
                var userCredential = _banamataHallContext.UserCredentials.Where(x => x.Id == resetPassword.Id).FirstOrDefault();
                if (userCredential == null)
                {
                    return NotFound();
                }

                userCredential.Password = CryptoUtility.EncryptText(resetPassword.NewPassword);
                userCredential.IsPasswordResetDone = true;
            }
            _banamataHallContext.SaveChanges();
            var userMessage = "Password has been reset successfully. Please cleck <a class=\"text-primary\" href=\"/Account/Login\">here</a> to login with the new password";
            return RedirectToAction("ForgotConfirm", new { message = userMessage });
        }

        [HttpGet]
        public ActionResult Verify(string p)
        {
            string userMessage = "";
            try
            {
                var decText = CryptoUtility.DecryptText(p);
                var parts = decText.Split("&"); // m=01814292999&t=&ex=2024-03-17 11:10:14 AM
                string mobile = parts[0].Split("=")[1];
                string userType = parts[1].Split("=")[1];
                DateTime expiry = DateTime.Parse(parts[2].Split("=")[1]);
                if (DateTime.Now > expiry)
                {
                    userMessage = "Password reset link already expired. Please try again in time.";
                    return RedirectToAction("ForgotConfirm", new { message = userMessage });
                }
                var resetPasswordVm = new ResetPasswordViewModel();

                if (userType == LoginAs.Student.ToString())
                {
                    var studentObj = _banamataHallContext.Students.FirstOrDefault(x => x.Mobile == mobile && x.IsArchived != true);
                    if (studentObj == null)
                    {
                        userMessage = "No user match or user alreay archived. Please contact with your system adminstrator.";
                        return RedirectToAction("ForgotConfirm", new { message = userMessage });
                    }

                    resetPasswordVm.IsStudentLogin = true;
                    resetPasswordVm.Id = studentObj.Id;
                }
                else
                {
                    var userCredential = _banamataHallContext.UserCredentials.Where(x => x.Staff.Mobile == mobile && x.IsEnabled == true).FirstOrDefault();
                    if (userCredential == null)
                    {
                        userMessage = "No user match or user credential already disabled. Please contact with your system adminstrator.";
                        return RedirectToAction("ForgotConfirm", new { message = userMessage });
                    }

                    resetPasswordVm.IsStudentLogin = false;
                    resetPasswordVm.Id = userCredential.Id;
                }
                return View("PasswordReset", resetPasswordVm);
            }
            catch (Exception ex)
            {
                _logger.LogError("Link verify failed." + ex.ToString());
                userMessage = "Link verify failed. Please try with exact reset link that we have sent in your SMS.";
                return RedirectToAction("ForgotConfirm", new { message = userMessage });
            }
        }
    }
}
