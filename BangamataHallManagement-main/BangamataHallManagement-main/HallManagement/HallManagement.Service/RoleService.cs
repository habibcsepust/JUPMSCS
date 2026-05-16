using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role GetById(int Id)
        {
            return _roleRepository.GetById(Id).Result;
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll().Result;
        }

        public bool Create(Role role)
        {
           return  _roleRepository.Create(role);
        }

        public bool Update(Role role)
        {
           return  _roleRepository.Update(role);
        }

        public bool Delete(int id)
        {
            var role = _roleRepository.GetById(id)?.Result;
            if (role == null)
                return false;
            return _roleRepository.Delete(role);              
        }
    }
}
