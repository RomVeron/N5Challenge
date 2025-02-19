using MediatR;

namespace Infraestructure.Commands
{
	public record AddPermissionCommand(string EmployeeForename, string EmployeeSurname, int PermissionType, DateTime PermissionDate) : IRequest<int>;
}
