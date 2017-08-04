using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using StudentChatBot.DAL;
using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class HelpDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("teHelper Bot is here to help! " +
                "At anytime you can type 'menu' or 'exit' to return to the main menu or to start over.  " +
                "If you are looking for something in particular, enter 'search' and I can help you find whatever you are looking for.  " +
                "If you want to chat with Josh, type 'Josh' (he's always available to chat).");

            context.Wait(MessageReceivedAsync);

        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput.Contains("josh"))
            {
                context.Call(new JoshWhiteBoardDialog(), this.ResumeAfterHelpDialog);
            }

            else if (userInput.Contains("search"))
            {
                context.Call(new SearchDialog(), this.ResumeAfterHelpDialog);
            }

            else if (userInput.Contains("exit") || userInput.Contains("menu"))
            {
                context.Done(true);
            }
            else
            {
                await context.PostAsync("Sorry, I didn't understand that command.");
                context.Done(true);
            }

        }
        public async Task ResumeAfterHelpDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I hope that was helpful.");
            context.Done(true);
        }
    }
}