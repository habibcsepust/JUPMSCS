using HallManagement.Core.Interfaces;
using HallManagement.Core.Repositories;
using HallManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Repositories.GenericRepository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BangamataHallContext _appContext;

        private IDepartmentRepository _department;

        private IRoleRepository _role;
        private IUserCredentialRepository _userCredential;
        private IMenuRepository _menu;
        private IMenuRoleRepository _menuRole;
        private IStudentRepository _student;
        private IClassRepository _class;
        private IDesignationRepository _designation;
        private IRoomRepository _room;
        private IRoomSeatRepository _roomSeat;
        private ISectionRepository _section;
        private IEstablishmentChargeRepository _establishmentCharge;
        private IStaffRepository _staff;
        private ISessionRepository _session;
        private INationalityRepository _nationality;
        private IReligionRepository _religion;
        private IBloodGroupRepository _bloodGroup;
        private IBatchRepository _batch;
        private IRoomSeatHistoryRepository _roomSeatHistory;
        private IStudentHistoryRepository _studentHistory;
        private IPasswordResetHistoryRepository _passwordResetHistory;

        public RepositoryWrapper(BangamataHallContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public void Save()
        {
            _appContext.SaveChanges();
        }

        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentRepository(_appContext);
                }
                return _department;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_appContext);
                }
                return _role;
            }
        }

        public IUserCredentialRepository UserCredential
        {
            get
            {
                if (_userCredential == null)
                {
                    _userCredential = new UserCredentialRepository(_appContext);
                }
                return _userCredential;
            }
        }

        public IMenuRepository Menu
        {
            get
            {
                if (_menu == null)
                {
                    _menu = new MenuRepository(_appContext);
                }
                return _menu;
            }
        }

        public IMenuRoleRepository MenuRole
        {
            get
            {
                if (_menuRole == null)
                {
                    _menuRole = new MenuRoleRepository(_appContext);
                }
                return _menuRole;
            }
        }

        public IStudentRepository Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_appContext);
                }
                return _student;
            }
        }

        public IClassRepository Class
        {
            get
            {
                if (_class == null)
                {
                    _class = new ClassRepository(_appContext);
                }
                return _class;
            }
        }

        public IDesignationRepository Designation
        {
            get
            {
                if (_designation == null)
                {
                    _designation = new DesignationRepository(_appContext);
                }
                return _designation;
            }
        }

        public IRoomRepository Room
        {
            get
            {
                if (_room == null)
                {
                    _room = new RoomRepository(_appContext);
                }
                return _room;
            }
        }

        public IRoomSeatRepository RoomSeat
        {
            get
            {
                if (_roomSeat == null)
                {
                    _roomSeat = new RoomSeatRepository(_appContext);
                }
                return _roomSeat;
            }
        }

        public ISectionRepository Section
        {
            get
            {
                if (_section == null)
                {
                    _section = new SectionRepository(_appContext);
                }
                return _section;
            }
        }

        public IEstablishmentChargeRepository EstablishmentCharge
        {
            get
            {
                if (_establishmentCharge == null)
                {
                    _establishmentCharge = new EstablishmentChargeRepository(_appContext);
                }
                return _establishmentCharge;
            }
        }

        public IStaffRepository Staff
        {
            get
            {
                if (_staff == null)
                {
                    _staff = new StaffRepository(_appContext);
                }
                return _staff;
            }
        }

        public ISessionRepository Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new SessionRepository(_appContext);
                }
                return _session;
            }
        }

        public INationalityRepository Nationality
        {
            get
            {
                if (_nationality == null)
                {
                    _nationality = new NationalityRepository(_appContext);
                }
                return _nationality;
            }
        }

        public IReligionRepository Religion
        {
            get
            {
                if (_religion == null)
                {
                    _religion = new ReligionRepository(_appContext);
                }
                return _religion;
            }
        }

        public IBloodGroupRepository BloodGroup
        {
            get
            {
                if (_bloodGroup == null)
                {
                    _bloodGroup = new BloodGroupRepository(_appContext);
                }
                return _bloodGroup;
            }
        }

        public IBatchRepository Batch
        {
            get
            {
                if (_batch == null)
                {
                    _batch = new BatchRepository(_appContext);
                }
                return _batch;
            }
        }

        public IRoomSeatHistoryRepository RoomSeatHistory
        {
            get
            {
                if (_roomSeatHistory == null)
                {
                    _roomSeatHistory = new RoomSeatHistoryRepository(_appContext);
                }
                return _roomSeatHistory;
            }
        }

        public IStudentHistoryRepository StudentHistory
        {
            get
            {
                if (_studentHistory == null)
                {
                    _studentHistory = new StudentHistoryRepository(_appContext);
                }
                return _studentHistory;
            }
        }

        public IPasswordResetHistoryRepository PasswordResetHistory
        {
            get
            {
                if (_passwordResetHistory == null)
                {
                    _passwordResetHistory = new PasswordResetHistoryRepository(_appContext);
                }
                return _passwordResetHistory;
            }
        }        
    }
}
