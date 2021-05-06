using AutoMapper;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, Responses.CpuMetricDto>();
            CreateMap<DotNetMetric, Responses.DotNetMetricDto>();
            CreateMap<HddMetric, Responses.HddMetricDto>();
            CreateMap<NetWorkMetric, Responses.NetWorkMetricDto>();
            CreateMap<RamMetric, Responses.RamMetricDto>();
        }
    }
}
