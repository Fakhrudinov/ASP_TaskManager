using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetricsManager.DTO
{
    public class Agent
    {
        public int AgentId { get; set; }

        [Required]
        [DefaultValue("http://localhost:5070")]
        public string AgentAddress { get; set; }
    }
}