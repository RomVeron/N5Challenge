using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Infraestructure.Data;

namespace Infraestructure.Repositories
{
	public class PermissionRepository : Repository<Permission>, IPermissionRepository
	{
		public PermissionRepository(AppDbContext context) : base(context) { }

		public async Task<IEnumerable<Permission>> GetPermissionsByTypeAsync(int permissionType)
		{
			return await _dbSet.Where(p => p.PermissionType == permissionType).ToListAsync();
		}
	}
}
