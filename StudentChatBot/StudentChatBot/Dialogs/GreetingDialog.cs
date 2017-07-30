using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }
        
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var utcNow = DateTime.UtcNow;
            var est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var currentTimeOfDay = TimeZoneInfo.ConvertTime(utcNow, est).Hour;

            var greeting = "";
            
            if (currentTimeOfDay >= 0 && currentTimeOfDay < 5)
            {
                greeting = "What are you doing up at this hour??";
            }
            else if (currentTimeOfDay >= 5 && currentTimeOfDay < 12)
            {
                greeting = "Good morning! Have you had your coffee yet?";
                var rnd = new Random();
                int randomResult = rnd.Next(0, 3);

                if (randomResult == 1)
                {
                    greeting += " I mean, unless you don't like coffee... then, well, whatever... tea? water? milk???";
                }
            }
            else if (currentTimeOfDay >= 12 && currentTimeOfDay < 17)
            {
                greeting = "Good afternoon!";

                var rnd = new Random();
                int randomResult = rnd.Next(0, 3);

                if (randomResult == 1)
                {
                    greeting += " Hope you've been productive, or at least almost productive.";
                }
            }
            else if (currentTimeOfDay >= 17)
            {
                greeting = "Good evening!";

                var rnd = new Random();
                int randomResult = rnd.Next(0, 3);

                if (randomResult == 1)
                {
                    greeting += " At least, I mean, I hope it's a good evening...";
                }
            }

            await context.PostAsync(greeting);
            
            context.Done(true);
        }

    }
}

