using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Nest;

namespace Infraestructure.Services
{
	public class ElasticSearchService : IElasticSearchService
	{
		private readonly IElasticClient _elasticClient;
		private readonly ILogger<ElasticSearchService> _logger;

		public ElasticSearchService(IElasticClient elasticClient, ILogger<ElasticSearchService> logger)
		{
			_elasticClient = elasticClient;
			_logger = logger;
		}

		public async Task IndexPermissionAsync(PermissionDocument permission)
		{
			var response = await _elasticClient.IndexDocumentAsync(permission);

			if (!response.IsValid)
			{
				_logger.LogError("Error indexando en Elasticsearch: {Error}", response.OriginalException?.Message);
			}
			else
			{
				_logger.LogInformation("Permiso indexado en Elasticsearch con ID: {Id}", permission.Id);
			}
		}
	}
}
