using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class SessionService : ISessionService
    {

        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Session GetById(int Id)
        {
            return _sessionRepository.GetById(Id).Result;
        }

        public IEnumerable<Session> GetAll()
        {
            return _sessionRepository.GetAll().Result;
        }

        public bool Create(Session session)
        {
           return  _sessionRepository.Create(session);
        }

        public bool Update(Session session)
        {
           return  _sessionRepository.Update(session);
        }

        public bool Delete(int id)
        {
            var session = _sessionRepository.GetById(id)?.Result;
            if (session == null)
                return false;
            return _sessionRepository.Delete(session);              
        }
    }
}
