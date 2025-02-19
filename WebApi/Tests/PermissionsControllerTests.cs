using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Infraestructure.Commands;
using MediatR;
using WebApi.Controllers;
using Core.Interfaces;
using Core.Entities;
using Infraestructure.Queries;

public class PermissionsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<ILogger<PermissionsController>> _mockLogger;
    private readonly Mock<IKafkaProducerService> _mockKafkaProducerService;
    private readonly Mock<IElasticSearchService> _mockElasticSearchService;
    private readonly PermissionsController _controller;

	public PermissionsControllerTests()
	{
		_mockMediator = new Mock<IMediator>();
		_mockLogger = new Mock<ILogger<PermissionsController>>();
		_mockKafkaProducerService = new Mock<IKafkaProducerService>();
		_mockElasticSearchService = new Mock<IElasticSearchService>();

		_controller = new PermissionsController(
			_mockMediator.Object,
			_mockLogger.Object,
			_mockKafkaProducerService.Object,
			_mockElasticSearchService.Object
		);
	}

	[Fact]
	public async Task RequestPermission_ShouldReturnOk_WithValidData()
	{
		var command = new AddPermissionCommand("Roman", "Veron", 1, DateTime.UtcNow);
		_mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(1);

		var result = await _controller.RequestPermission(command);

		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.NotNull(okResult.Value);
	}

	[Fact]
	public async Task ModifyPermission_ShouldReturnOk_WhenPermissionExists()
	{
		var command = new ModifyPermissionCommand(1, "Roman", "Veron", 2, DateTime.UtcNow);
		_mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(true);

		var result = await _controller.ModifyPermission(1, command);

		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Contains("successfully", okResult.Value.ToString());
	}

	[Fact]
	public async Task GetPermissions_ShouldReturnOk_WithPermissionsList()
	{
		var permissions = new List<Permission>
		{
			new Permission { Id = 1, EmployeeForename = "Roman", EmployeeSurname = "Veron", PermissionType = 1, PermissionDate = DateTime.UtcNow }
		};
		_mockMediator.Setup(m => m.Send(It.IsAny<GetPermissionsQuery>(), default)).ReturnsAsync(permissions);

		var result = await _controller.GetPermissions();

		var okResult = Assert.IsType<OkObjectResult>(result);
		var returnedPermissions = Assert.IsType<List<PermissionDocument>>(okResult.Value);
		Assert.Single(returnedPermissions);
	}
}
