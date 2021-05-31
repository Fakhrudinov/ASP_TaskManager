﻿using Quartz;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using WpfApp_MetricsVisualisation.ChartControls;

namespace WpfApp_MetricsVisualisation
{
    public class JobGetNewMetricsCpu : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            string[] myUrl = (string[])dataMap["myUrl"];
            var myChart = (ChartCPU)dataMap["myChart"];
            
            string fromTime = DateTimeOffset.UtcNow.AddMinutes(-2).ToString("O");
            if (myChart.LineSeriesValues[0].Values.Count > 0)
            {
                //get last time and add 1 second to Avoid data duplication
                var lastTime = myChart.Labels.GetValue(myChart.LineSeriesValues[0].Values.Count - 1).ToString();
                var toDTO = DateTimeOffset.Parse(lastTime);
                fromTime = toDTO.AddSeconds(1).ToString("O");
            }

            for (int i = 0; i < myUrl.Length; i++)
            {
                myUrl[i] = Actions.RemoveSlash(myUrl[i]);
            }

            string address = Actions.GetConnectionAddress("cpu", myUrl, fromTime);

            GetRequestAsync(address, myChart, context);
        }

        private async Task GetRequestAsync(string requestAddress, ChartCPU myChart, IJobExecutionContext context)
        {
            CpuMetricsResponse cpuMetrics = new CpuMetricsResponse();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(requestAddress))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            cpuMetrics = JsonConvert.DeserializeObject<CpuMetricsResponse>(data);

                            foreach (CpuMetricDto metric in cpuMetrics.Metrics)
                            {
                                myChart.LineSeriesValues[0].Values.Add(metric.Value);

                                if (myChart.LineSeriesValues[0].Values.Count > myChart.valuesCount)
                                {
                                    myChart.LineSeriesValues[0].Values.RemoveAt(0);

                                    // move time labels
                                    for (int i = 1; i < myChart.valuesCount; i++)
                                    {
                                        myChart.Labels.SetValue(myChart.Labels.GetValue(i), i - 1);
                                    }
                                }

                                myChart.Labels.SetValue(metric.Time.ToString(), myChart.LineSeriesValues[0].Values.Count - 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRequestAsync exception " + ex.Message);

                await context.Scheduler.PauseJob(context.JobDetail.Key);
            }
        }
    }
}