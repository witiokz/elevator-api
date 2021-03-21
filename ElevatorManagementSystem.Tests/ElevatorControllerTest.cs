using ElevatorManagementSystem.Controllers;
using ElevatorManagementSystem.Domain.Dto;
using ElevatorManagementSystem.Services;
using System.Collections.Generic;
using Xunit;

namespace ElevatorManagementSystem.Tests
{
    public class ElevatorControllerTest
    {
        readonly ElevatorController _controller;
        readonly IElevatorService _service;
        public ElevatorControllerTest()
        {
            _service = new ElevatorServiceFake();
            _controller = new ElevatorController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllElevatorLogItems()
        {
            var result = _controller.GetElevatorLogs();
            var items = Assert.IsType<List<ElevatorLogDto>>(result.Result);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOneElevatorResult()
        {
            var result = _controller.GetElevatorStatus(1);
            Assert.IsType<ElevatorDto>(result.Result);
        }
    }
}
