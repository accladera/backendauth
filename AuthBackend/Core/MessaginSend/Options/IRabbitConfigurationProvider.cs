namespace Core.Messaging.Send.Options
{
	public interface IRabbitConfigurationProvider 
	{
		RabbitMqConfiguration GetConfiguration();
	}
}
