using Core.Entities;
using Core.Interfaces;
using WebApi.Infraestructure.Data;

namespace Infraestructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		private IPermissionRepository? _permissions;
		private IRepository<PermissionType>? _permissionTypes;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(_context);

		public IRepository<PermissionType> PermissionTypes => _permissionTypes ??= new Repository<PermissionType>(_context);

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
