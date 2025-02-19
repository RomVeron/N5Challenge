using Core.Entities;
using MediatR;

namespace Infraestructure.Queries
{
	public record GetPermissionsQuery : IRequest<IEnumerable<Permission>>;
}
