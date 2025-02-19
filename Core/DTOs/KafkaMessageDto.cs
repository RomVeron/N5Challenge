namespace Core.DTOs
{
	public class KafkaMessageDto
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string OperationName { get; set; } = string.Empty; // "solicitar", "modificar" u "obtener"
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}
