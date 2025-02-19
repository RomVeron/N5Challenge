using Core.Entities;
using Core.Interfaces;
using Infraestructure.Commands;
using MediatR;

namespace WebApi.Application.Handlers
{
	public class AddPermissionHandler : IRequestHandler<AddPermissionCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;

		public AddPermissionHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
		{
			var permission = new Permission
			{
				EmployeeForename = request.EmployeeForename,
				EmployeeSurname = request.EmployeeSurname,
				PermissionType = request.PermissionType,
				PermissionDate = request.PermissionDate
			};

			await _unitOfWork.Permissions.AddAsync(permission);
			await _unitOfWork.CommitAsync();

			return permission.Id;
		}
	}
}
