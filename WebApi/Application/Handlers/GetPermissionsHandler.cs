using Core.Entities;
using Core.Interfaces;
using Infraestructure.Queries;
using MediatR;

namespace WebApi.Application.Handlers
{
	public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<Permission>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetPermissionsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Permission>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Permissions.GetAllAsync();
		}
	}
}
