using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.DataAccessLayer.Repository
{
    public interface IAgentsRepository
    {
        public void RegisterAgent(Agent newAgent);
        public void RemoveAgent(Agent oldAgent);
        public IList<Agent> GetAllAgentsList();
        public string GetAddressForAgentId(int id);
    }
}
