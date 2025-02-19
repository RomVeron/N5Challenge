using MediatR;

namespace Infraestructure.Commands
{
	public record ModifyPermissionCommand(int Id, string EmployeeForename, string EmployeeSurname, int PermissionType, DateTime PermissionDate) : IRequest<bool>;
}
