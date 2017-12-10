using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var commands = activity.Text;
            var replayMessage = string.Empty;

            if (commands.IndexOf("ein number") >= 0 || commands.IndexOf("統編") >= 0 || commands.IndexOf("統一編號") >= 0)
            {
                replayMessage = Resources.Messages.EINNumber;
            }

            if (commands.IndexOf("office address") >= 0 || commands.IndexOf("辦公室地址") >= 0)
            {
                replayMessage = Resources.Messages.OfficeAddress;
            }

            await context.PostAsync($"{replayMessage}");
            context.Wait(MessageReceivedAsync);
        }
    }
}