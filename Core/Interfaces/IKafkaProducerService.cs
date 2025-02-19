namespace Core.Interfaces
{
	public interface IKafkaProducerService
	{
		Task PublishMessageAsync(string operationName);
	}
}
