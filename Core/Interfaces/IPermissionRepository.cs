using Core.Entities;

namespace Core.Interfaces
{
	public interface IPermissionRepository : IRepository<Permission>
	{
		Task<IEnumerable<Permission>> GetPermissionsByTypeAsync(int permissionType);
	}
}
