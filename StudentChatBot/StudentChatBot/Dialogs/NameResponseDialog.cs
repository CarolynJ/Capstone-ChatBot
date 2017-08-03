using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        private async Task NameCollected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            var userName = activity.Text.ToString().ToLower();
            userName = userName.Substring(0, 1).ToUpper() + userName.Substring(1);

            var response = "";
            if(userName == "Josh")
            {
                Attachment attachment = new Attachment();
                attachment.ContentType = "image/png";
                attachment.ContentUrl = "https://pbs.twimg.com/profile_images/658652805360689153/lRuRLtnh.jpg";
                var message = context.MakeMessage();
                message.Text = "Josh, huh.....any chance this is Josh Tucholski?";

                message.Attachments.Add(attachment);

                await context.PostAsync(message).ConfigureAwait(false);
                context.Wait(JoshResponse);
            }
            else
            {
                response = "It's nice to meet you " + userName;
                await context.PostAsync(response);
                Thread.Sleep(2500);

                StateClient stateClient = activity.GetStateClient();
                BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                userData.SetProperty<string>("UserName", userName);

                context.Done(true);
            }

        }

        public async Task JoshResponse(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;

            if(activity.Text.ToLower() == "yes" || activity.Text.ToLower() == "y")
            {
                string title = "check out this link buddy";
                string content = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                var markdownContent = $"[{title}]({content})";

                await context.PostAsync(markdownContent);

                Thread.Sleep(3000);
            }
            else
            {
                await context.PostAsync("Oh ok nevermind, how can I help you today?");
            }
            RootDialog dialog = new RootDialog();
            await dialog.ResumeAfterNameResponse(context, result);
        }

    }
}