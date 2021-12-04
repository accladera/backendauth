namespace Core.Messaging.Send.Models
{
	public class Response<T>
	{
		public bool Status { get; set; }

		public T Data { get; set; }

		public string Error { get; set; }
	}
}
