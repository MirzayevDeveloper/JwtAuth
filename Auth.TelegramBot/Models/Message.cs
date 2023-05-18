using System;

namespace Auth.TelegramBot.Models
{
	public class Message
	{
        public Guid Id { get; set; }
        public string ExceptionMessage { get; set; }
        public ErrorLevel Level { get; set; }
        public string MessageText { get; set; }
        public string MessageTemplate { get; set; }
        public DateTimeOffset RaiseDate { get; set; }
    }
}
