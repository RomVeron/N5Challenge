using Confluent.Kafka;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infraestructure.Services
{
	public class KafkaProducerService : IKafkaProducerService
	{
		private readonly string _bootstrapServers;
		private readonly string _topic;
		private readonly ILogger<KafkaProducerService> _logger;

		public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
		{
			_bootstrapServers = configuration["Kafka:BootstrapServers"];
			_topic = configuration["Kafka:TopicName"];
			_logger = logger;
		}

		public async Task PublishMessageAsync(string operationName)
		{
			var config = new ProducerConfig
			{
				BootstrapServers = _bootstrapServers
			};

			using var producer = new ProducerBuilder<Null, string>(config).Build();

			var message = new KafkaMessageDto
			{
				OperationName = operationName
			};

			var jsonMessage = JsonSerializer.Serialize(message);

			try
			{
				await producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });
				_logger.LogInformation("Mensaje enviado a Kafka: {Message}", jsonMessage);
			}
			catch (ProduceException<Null, string> ex)
			{
				_logger.LogError("Error en Kafka: {Error}", ex.Error.Reason);
			}
		}
	}
}
