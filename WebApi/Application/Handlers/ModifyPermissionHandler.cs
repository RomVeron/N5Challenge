using Core.Interfaces;
using Infraestructure.Commands;
using MediatR;

namespace WebApi.Application.Handlers
{
	public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, bool>
	{
		private readonly IUnitOfWork _unitOfWork;

		public ModifyPermissionHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
		{
			// Buscar el permiso en la base de datos
			var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(request.Id);

			if (existingPermission == null)
			{
				// Retornar falso si el permiso no existe
				return false;
			}

			// Modificar los datos del permiso
			existingPermission.EmployeeForename = request.EmployeeForename;
			existingPermission.EmployeeSurname = request.EmployeeSurname;
			existingPermission.PermissionType = request.PermissionType;
			existingPermission.PermissionDate = request.PermissionDate;

			// Guardar cambios
			_unitOfWork.Permissions.Update(existingPermission);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}
