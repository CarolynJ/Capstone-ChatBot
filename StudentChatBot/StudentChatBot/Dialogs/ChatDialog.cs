using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class ChatDialog : IDialog<object>
    {
        private const string JoshWhiteBoardOption = "Chat with Josh's Echo";
        private const string ChatOption = "Chat with teHelperBot";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to chat!");

            this.ShowOptions(context);
        }
        
        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>()
                { ChatOption, JoshWhiteBoardOption, ExitOption },
                "Who do you want to chat with?",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch(optionSelected)
            {
                case ChatOption:
                    await context.PostAsync("Let's talk buddy!");
                    context.Wait(MessageReceivedAsync);
                    break;
                case JoshWhiteBoardOption:
                    context.Call(new JoshWhiteBoardDialog(), this.DoneWithChat);
                    break;
                case ExitOption:
                    await context.PostAsync("Later!");
                    context.Done(true);
                    break;
            }
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I can't believe you said that. I think we're done chatting.");
            context.Wait(DoneWithChat);
        }

        private async Task DoneWithChat(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I'll return you to the main menu now.");
            context.Done(true);
        }

    }
}