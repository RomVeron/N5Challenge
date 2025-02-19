using Core.Entities;

namespace Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IPermissionRepository Permissions { get; }
		IRepository<PermissionType> PermissionTypes { get; }
		Task<int> CommitAsync(); // Para guardar los cambios
	}
}
