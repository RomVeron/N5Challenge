using Core.Entities;
using Core.Interfaces;

namespace Infraestructure.Services
{
	public class PermissionService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PermissionService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
		{
			return await _unitOfWork.Permissions.GetAllAsync();
		}

		public async Task AddPermissionAsync(Permission permission)
		{
			await _unitOfWork.Permissions.AddAsync(permission);
			await _unitOfWork.CommitAsync();
		}
	}
}
