using System.Threading.Tasks;
using Auth.TelegramBot.Models;

namespace Auth.TelegramBot.Services
{
	public interface ISendMessageService
	{
		ValueTask<Message> SendMessageAsync(Message message);
	}
}
