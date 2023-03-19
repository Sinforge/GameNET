using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text;

namespace ChatService
{
    public class BadWordsHubFilter : IHubFilter
    {
        private readonly IConfiguration _configuration;
        private readonly string[] _badWords;        
        public BadWordsHubFilter(IConfiguration configuration)
        {
            _configuration = configuration;
            _badWords = _configuration.GetSection("BadWordList").Get<string[]>();
        }
        public async ValueTask<object?> InvokeMethodAsync(
            HubInvocationContext invocationContext,
            Func<HubInvocationContext, ValueTask<object?>> next)
        {
            if (invocationContext.HubMethodArguments.Count == 3 &&
                invocationContext.HubMethodArguments[0] is string message)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach(string m in message.Split())
                {
                    stringBuilder.Append(_badWords.Contains(m) ? "***** ": m+" ");
                }
                message = stringBuilder.ToString();

                
                var arguments = invocationContext.HubMethodArguments.ToArray();
                arguments[0] = message;
                // пересоздаем объект HubInvocationContext
                invocationContext = new HubInvocationContext(invocationContext.Context,
                    invocationContext.ServiceProvider,
                    invocationContext.Hub,
                    invocationContext.HubMethod,
                    arguments);
            }
            // передаем этот объект в вызов последующих фильтров или метода хаба
            return await next(invocationContext);
        }
    }
}
