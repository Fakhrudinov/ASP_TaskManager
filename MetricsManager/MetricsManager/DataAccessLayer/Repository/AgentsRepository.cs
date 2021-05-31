using Dapper;
using MetricsManager.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;


namespace MetricsManager.DataAccessLayer.Repository
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly ILogger<AgentsRepository> _logger;

        public AgentsRepository(ILogger<AgentsRepository> logger)
        {
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
            _logger = logger;
        }

        public string GetAddressForAgentId(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var response = connection.QuerySingle<string>("SELECT AgentAddress FROM agents WHERE AgentId=@agent_id",
                        new
                        {
                            agent_id = id
                        }).ToList();

                    return response.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public IList<Agent> GetAllAgentsList()
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<Agent>("SELECT AgentId, AgentAddress FROM agents", null).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public void RegisterAgent(Agent newAgent)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    connection.Execute("INSERT INTO agents(AgentAddress) VALUES(@AgentAddress)",
                        new
                        {
                            AgentAddress = newAgent.AgentAddress.ToString()
                        });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void RemoveAgent(Agent oldAgent)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    connection.Execute($"DELETE FROM agents WHERE (AgentAddress=@AgentAddress)",
                        new
                        {
                            AgentAddress = oldAgent.AgentAddress.ToString()
                        });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
