using Core.Entities;
using Core.Interfaces;
using Infraestructure.Commands;
using Infraestructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PermissionsController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<PermissionsController> _logger;
		private readonly IKafkaProducerService _kafkaProducerService;
		private readonly IElasticSearchService _elasticSearchService;

		public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger,
			IKafkaProducerService kafkaProducerService, IElasticSearchService elasticSearchService)
		{
			_mediator = mediator;
			_logger = logger;
			_kafkaProducerService = kafkaProducerService;
			_elasticSearchService = elasticSearchService;
		}

		[HttpPost("Request")]
		public async Task<IActionResult> RequestPermission([FromBody] AddPermissionCommand command)
		{
			_logger.LogInformation("Operación: RequestPermission - {EmployeeForename} {EmployeeSurname}",
				command.EmployeeForename, command.EmployeeSurname);

			var id = await _mediator.Send(command);

			if (id > 0)
			{
				var permissionDocument = new PermissionDocument
				{
					Id = id,
					EmployeeForename = command.EmployeeForename,
					EmployeeSurname = command.EmployeeSurname,
					PermissionType = command.PermissionType,
					PermissionDate = command.PermissionDate
				};

				await _elasticSearchService.IndexPermissionAsync(permissionDocument);
				await _kafkaProducerService.PublishMessageAsync("solicitar");
			}

			return Ok(new { Id = id });
		}

		[HttpPut("Modify/{id}")]
		public async Task<IActionResult> ModifyPermission(int id, [FromBody] ModifyPermissionCommand command)
		{
			if (id != command.Id)
			{
				_logger.LogWarning("ID en la URL no coincide con el ID en el cuerpo: {Id}", id);
				return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
			}

			_logger.LogInformation("Operación: ModifyPermission - Modificando permiso con ID: {Id}", id);

			var result = await _mediator.Send(command);

			if (!result)
			{
				_logger.LogWarning("Permiso con ID {Id} no encontrado", id);
				return NotFound($"Permiso con ID {id} no encontrado.");
			}

			var permissionDocument = new PermissionDocument
			{
				Id = id,
				EmployeeForename = command.EmployeeForename,
				EmployeeSurname = command.EmployeeSurname,
				PermissionType = command.PermissionType,
				PermissionDate = command.PermissionDate
			};

			await _elasticSearchService.IndexPermissionAsync(permissionDocument);
			await _kafkaProducerService.PublishMessageAsync("modificar");

			return Ok($"Permiso con ID {id} modificado correctamente.");
		}

		[HttpGet("Get")]
		public async Task<IActionResult> GetPermissions()
		{
			_logger.LogInformation("Operación: GetPermissions - Obteniendo permisos");

			var permissions = await _mediator.Send(new GetPermissionsQuery());

			await _kafkaProducerService.PublishMessageAsync("obtener");

			return Ok(permissions);
		}
	}
}
