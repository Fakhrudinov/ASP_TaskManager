using Quartz;
using Quartz.Impl;
using System;
using System.Windows;

namespace WpfApp_MetricsVisualisation
{
    public partial class MainWindow : Window
    {
        public int DotsOnChart { get; set; }
        public string[] UrlParts { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            //sample api request    http://localhost:5080/api/networkmetrics/agent/1/from/2021-05-29T06:15:56.0000000+00:00/to/2021-05-29T06:17:06.7984366+00:00

            //get settings from App.config 
            UrlParts = new string[6];
            UrlParts[0] = Properties.Settings.Default.urlPart1; // http://localhost:5080
            UrlParts[1] = Properties.Settings.Default.urlPart2; // api
            UrlParts[2] = Properties.Settings.Default.urlPart3; // metrics/agent
            UrlParts[3] = Properties.Settings.Default.agentID;  // agent Id
            UrlParts[4] = Properties.Settings.Default.urlPart4; // from
            UrlParts[5] = Properties.Settings.Default.urlPart5; // to

            agentName.Text = UrlParts[0];
            bool success = Int32.TryParse(UrlParts[2], out int number);
            if (success && number > 0)
            {
                upDownControlAgentID.Value = number;
            }
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            buttonStart.IsEnabled = false;

            //actualize urlParts
            UrlParts[0] = agentName.Text;
            UrlParts[3] = upDownControlAgentID.Value.ToString();
            //save settings to App.config
            Properties.Settings.Default.urlPart1 = agentName.Text;
            Properties.Settings.Default.agentID = upDownControlAgentID.Value.ToString();
            Properties.Settings.Default.Save();

            var chartCpu = CpuChart;
            var chartRam = RamChart;
            var chartDotNet = DotNetChart;
            var chartNetWork = NetWorkChart;

            string address = agentName.Text;

            // construct a scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            //define the job and tie it to our class
           IJobDetail jobCPU = JobBuilder.Create<JobGetNewMetricsCpu>()
                .WithIdentity("myJob", "group1")
                .Build();
            jobCPU.JobDataMap.Put("myChart", chartCpu);
            jobCPU.JobDataMap.Put("myUrl", UrlParts);           

            IJobDetail jobRam = JobBuilder.Create<JobGetNewMetricsRam>()
                .WithIdentity("myJob", "group2")
                .Build();
            jobRam.JobDataMap.Put("myChart", chartRam);
            jobRam.JobDataMap.Put("myUrl", UrlParts);

            IJobDetail jobDotNet = JobBuilder.Create<JobGetNewMetricsDotNet>()
                .WithIdentity("myJob", "group3")
                .Build();
            jobDotNet.JobDataMap.Put("myChart", chartDotNet);
            jobDotNet.JobDataMap.Put("myUrl", UrlParts);

            IJobDetail jobNetWork = JobBuilder.Create<JobGetNewMetricsNetWork>()
                .WithIdentity("myJob", "group4")
                .Build();
            jobNetWork.JobDataMap.Put("myChart", chartNetWork);
            jobNetWork.JobDataMap.Put("myUrl", UrlParts);

            // Trigger the job to run now, and then every xx seconds
            ITrigger triggerCPU = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
                .Build();

            ITrigger triggerRam = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
                .Build();

            // Trigger the job to run now, and then every xx seconds
            ITrigger triggerDotNet = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group3")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
                .Build();

            ITrigger triggerNetWork = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group4")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobCPU, triggerCPU);
            await scheduler.ScheduleJob(jobRam, triggerRam);
            await scheduler.ScheduleJob(jobDotNet, triggerDotNet);
            await scheduler.ScheduleJob(jobNetWork, triggerNetWork);
        }
    }
}
