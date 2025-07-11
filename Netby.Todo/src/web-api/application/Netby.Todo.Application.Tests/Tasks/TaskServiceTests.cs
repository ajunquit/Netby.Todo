﻿using Microsoft.Extensions.Logging;
using Moq;
using Netby.Todo.Application.Services.Repositories;
using Netby.Todo.Application.Services.Tasks;
using Netby.Todo.Application.Services.Tasks.Create;
using Netby.Todo.Domain.Entities;

namespace Netby.Todo.Application.Tests.Tasks
{
    public class TaskServiceTests
    {
        private Mock<ITaskRepository> _mockRepo;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ILogger<TaskService>> _mockILogger;
        private TaskService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ITaskRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockILogger = new Mock<ILogger<TaskService>>();
            _service = new TaskService(_mockUnitOfWork.Object, _mockILogger.Object);
        }

        [Test]
        public async Task CreateTaskAsync_ShouldCreateTask_AndReturnDto()
        {
            // Arrange
            var request = new CreateTaskRequest
            {
                Title = "Prueba 1",
                Description = "Prueba 1",
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };

            _mockRepo.Setup(r => r.Insert(It.IsAny<TaskItem>())).Returns(true);
            _mockUnitOfWork.Setup(x => x.Tasks).Returns(_mockRepo.Object);
            _mockUnitOfWork.Setup(u => u.Save()).Returns(1);

            // Act
            var result = await _service.CreateTaskAsync(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(request.Title, Is.EqualTo(result.Title));
            Assert.That(request.Description, Is.EqualTo(result.Description));

            // Verify
            _mockRepo.Verify(r => r.Insert(It.IsAny<TaskItem>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}
