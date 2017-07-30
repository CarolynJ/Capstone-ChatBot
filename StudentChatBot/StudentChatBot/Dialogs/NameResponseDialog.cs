using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class NameResponseDialog: IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("What is your name?");
            context.Wait(NameCollected); 

        }

        private async Task NameCollected(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var userName = activity.Text.ToString().ToLower();
            userName = userName.Substring(0, 1).ToUpper() + userName.Substring(1);

            var response = "It's nice to meet you " + userName;
            await context.PostAsync(response);
           
            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            userData.SetProperty<string>("UserName", userName);

            context.Done(true);
        }

    }
}