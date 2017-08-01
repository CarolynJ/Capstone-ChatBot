using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class GoogleCalDialog
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        public Task StartAsync(IDialogContext context)
        {
            context.PostAsync("How many days' worth of events do you want to see?");
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            bool successfullyParseInt = Int32.TryParse(activity.Text, out int numberOfDays);
            string rfcFormat = "yyyy-MM-dd'T'HH:mm:ss.fffKzzz";
            var today = DateTime.UtcNow;

            if (!successfullyParseInt)
            {
                await context.PostAsync("Hmm, I didn't understand that. I recommend typing '7' or another integer.");
                context.Wait(MessageReceivedAsync);
            }

            await context.PostAsync(today.ToString(rfcFormat));



        }
    }
}