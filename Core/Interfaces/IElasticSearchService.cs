using Core.Entities;

namespace Core.Interfaces
{
	public interface IElasticSearchService
	{
		Task IndexPermissionAsync(PermissionDocument permission);
	}
}
