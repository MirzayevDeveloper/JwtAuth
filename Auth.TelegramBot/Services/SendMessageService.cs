using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Auth.TelegramBot.Models;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace Auth.TelegramBot.Services
{
	public class SendMessageService : ISendMessageService
	{
		private readonly long _chatId;
		private readonly ITelegramBotClient _client;

		public SendMessageService(IConfiguration configuration)
		{
			var httpClientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
				{
					return true;
				},
			};

			var httpClient = new HttpClient(httpClientHandler);

			_client = new TelegramBotClient(
				configuration["TelegramBotToken"], httpClient: httpClient);

			long.TryParse(
				configuration["TelegramChatId"], out _chatId);
		}

		public async ValueTask<Message> SendMessageAsync(Message message)
		{
			await _client.SendTextMessageAsync(
				chatId: _chatId,
				text: $"Message id: *{message.Id}*\n" +
						$"Exception message: {message.ExceptionMessage}\n" +
						$"Error level: {message.Level}\n" +
						$"Message text: {message.MessageText}\n" +
						$"Message template: {message.MessageTemplate}\n" +
						$"Raise Date: {message.RaiseDate}",
				parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);

			return message;
		}
	}
}
