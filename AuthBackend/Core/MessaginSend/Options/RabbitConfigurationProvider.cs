using System;

namespace Core.Messaging.Send.Options
{
	public class RabbitConfigurationProvider: IRabbitConfigurationProvider
	{
		private RabbitMqConfiguration _current;

		public RabbitMqConfiguration GetConfiguration()
		{
			if (_current == null)
			{
				_current = Parse();
			}
			return _current;
		}

		private RabbitMqConfiguration Parse()
		{
				var response = new RabbitMqConfiguration();
				var connection = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTION");
				response.Enabled = false;
				
				if (connection != null)
				{
						var list = connection.Split(";");
						foreach (var row in list)
						{
								var tmp = row.Split("=");
								switch (tmp[0])
								{
										case "server":
												response.Hostname = tmp[1];
												break;
										case "port":
												response.Port = Int32.Parse(tmp[1]);
												break;
										case "user":
												response.UserName = tmp[1];
												break;
										case "password":
												response.Password = tmp[1];
												break;
								}
						}
						response.Enabled = true;
				}

				return response;
		}
	}
}
