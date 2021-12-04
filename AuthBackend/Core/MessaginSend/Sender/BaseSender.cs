using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Messaging.Send.Models;
using Core.Messaging.Send.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging.Send.Sender
{
    public class BaseSender
    {
        protected string _queueName;
        
        private IConnection _connection;
        private readonly RabbitMqConfiguration _config;
        
        public BaseSender(string queueName, IRabbitConfigurationProvider options)
        {
            _config = options.GetConfiguration();
            _queueName = queueName;
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();

            return _connection != null;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory() 
                {
                    HostName = _config.Hostname,
                    UserName = _config.UserName,
                    Password = _config.Password,
                    Port = _config.Port
                };
                _connection = factory.CreateConnection();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"No se creo una conexion RabbitMq ${ex.Message}");
            }
        }

        public async Task<Response<T>> ExecuteAsync<T>(object request)
        {
            Response<T> response = default(Response<T>);
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    var replyQueueName = channel.QueueDeclare().QueueName;
                    var consumer = new EventingBasicConsumer(channel);
                    ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
                    consumer.Received += (model, ea) =>
                    {
                        if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out TaskCompletionSource<string> tcs))
                            return;
                        var body = ea.Body.ToArray();
                        var response = Encoding.UTF8.GetString(body);
                        tcs.TrySetResult(response);
                    };

                    CancellationToken cancellationToken = default(CancellationToken);
                    IBasicProperties props = channel.CreateBasicProperties();
                    var correlationId = Guid.NewGuid().ToString();
                    props.CorrelationId = correlationId;
                    props.ReplyTo = replyQueueName;
                    var messageBytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                    var tcs = new TaskCompletionSource<string>();
                    callbackMapper.TryAdd(correlationId, tcs);
            
                    channel.BasicPublish(
                            exchange: "",
                            routingKey: _queueName,
                            basicProperties: props,
                            body: messageBytes);

                    channel.BasicConsume(
                            consumer: consumer,
                            queue: replyQueueName,
                            autoAck: true);
            
                    cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out var tmp));
                    var responseRaw = await tcs.Task;
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<Response<T>>(responseRaw);
                
                    if (!response.Status)
                    {
                        if (response.Error != null)
                        {
                            throw new InvalidOperationException(response.Error);
                        }
                        else
                        {
                            throw new Exception("Error en el sender " + _queueName);
                        }
                    }
                }
            }
            return response;
        }
    }
}
