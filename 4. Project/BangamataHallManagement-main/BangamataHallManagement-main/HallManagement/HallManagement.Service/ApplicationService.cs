using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(
            IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }


        public Application GetById(int Id)
        {
            return _applicationRepository.GetById(Id).Result;
        }


        public IEnumerable<Application> GetAll()
        {
            return _applicationRepository.GetAll().Result;
        }


        public bool Create(Application application)
        {
            application.Status = "Pending";

            if (application.ApplicationDate == default)
            {
                application.ApplicationDate = DateTime.Now;
            }

            application.CreatedDate = DateTime.Now;

            return _applicationRepository.Create(application);
        }


        public bool Update(Application application)
        {
            return _applicationRepository.Update(application);
        }


        public bool Delete(int id)
        {
            var application = _applicationRepository.GetById(id)?.Result;

            if (application == null)
                return false;

            return _applicationRepository.Delete(application);
        }
    }
}