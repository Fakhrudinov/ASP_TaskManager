using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerTest
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsController>> logger;
        private readonly IMapper mapper;

        public CpuMetricsControllerTest()
        {
            mock = new Mock<ICpuMetricsRepository>();
            logger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetFromTimeToTime(5, 10)).Returns(new List<CpuMetric>()).Verifiable();

            //Act
            var result = controller.GetFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetFromTimeToTime(5, 10), Times.AtMostOnce());
            logger.Verify();
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
