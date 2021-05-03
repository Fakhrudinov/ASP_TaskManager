using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class UnitTestCPU
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsController>> logger;

        public UnitTestCPU()
        {
            mock = new Mock<ICpuMetricsRepository>();
            logger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(logger.Object, mock.Object);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            // Arrange
            var returnList = new List<CpuMetric>();

            // необязательно. задается ниже в ассерте repository.GetFromTimeToTime(10, 20)
            //returnList.Add(new CpuMetric
            //{
            //    Id = 0,
            //    Value = 111,
            //    Time = DateTimeOffset.FromUnixTimeSeconds(10)
            //});

            //returnList.Add(new CpuMetric
            //{
            //    Id = 1,
            //    Value = 222,
            //    Time = DateTimeOffset.FromUnixTimeSeconds(20)
            //});

            mock.Setup(repository => repository.GetFromTimeToTime(
                It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(), 
                It.IsAny<DateTimeOffset>().ToUnixTimeSeconds()))
                .Returns(returnList);

            //// Act
            IActionResult result = controller.GetFromTimeToTime(
                DateTimeOffset.FromUnixTimeSeconds(10),
                DateTimeOffset.FromUnixTimeSeconds(20));

            // Assert
            mock.Verify(repository => repository.GetFromTimeToTime(10, 20), Times.AtLeastOnce());
        }


        //[Fact]
        //public void Create_ShouldCall_Create_From_Repository()
        //{
        //    // устанавливаем параметр заглушки
        //    // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
        //    mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

        //    // выполняем действие на контроллере
        //    var result = controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

        //    // проверяем заглушку на то, что пока работал контроллер
        //    // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
        //    mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        //}

        //[Fact]
        //public void GetMetricsFromTimeToTime_ReturnsOk()
        //{
        //    //Arrange
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);

        //    //Act
        //    var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}

        //[Fact]
        //public void GetMetricsFromTimeToTimeByPercentile_ReturnsOk()
        //{
        //    //Arrange
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);
        //    var percentile = MetricsManager.Percentile.P95;

        //    //Act
        //    var result = controller.GetMetricsFromTimeToTimeByPercentile(fromTime, toTime, percentile);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
    }
}
