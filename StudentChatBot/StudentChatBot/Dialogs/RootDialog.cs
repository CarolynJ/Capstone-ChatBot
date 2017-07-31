using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Collections.Generic;
using StudentChatBot.DAL;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private const string SearchOption = "Search By Keyword";
        private const string BrowseOption = "Browse";
        private const string ChatOption = "Chat with Me";
        private const string HelpOption = "Get Help";
        private const string ExitOption = "Exit";
        private const string MotivationOption = "Get Motivated";

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "hello" || userInput == "hey" || userInput == "hi")
            {
                await context.Forward(new GreetingDialog(), this.ResumeAfterGreetingDialog, activity, CancellationToken.None);
            }
            else if (userInput.Contains("menu"))
            {
                this.ShowOptions(context);
            }
            else if (userInput.Contains("search"))
            {
                await context.Forward(new SearchDialog(), this.ResumeAfterGreetingDialog, activity, CancellationToken.None);
            }
            else
            {
                await context.PostAsync("Sorry, I didn't understand that command. Please type 'menu' for more information.");
                context.Done(true);
            }
        }

        private async Task ResumeAfterGreetingDialog(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            context.Call(new NameResponseDialog(), this.ResumeAfterNameResponse);
        }
        private async Task ResumeAfterNameResponse(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            this.ShowOptions(context);
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>()
                {  SearchOption, BrowseOption, MotivationOption, ChatOption,  HelpOption, ExitOption },
                "Are you looking to search for info, get help, or just chat?",
                "Hmmm, I didn't understand that, try again...",
                2);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case SearchOption:
                        context.Call(new SearchDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case BrowseOption:
                        context.Call(new BrowseDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case MotivationOption:
                        context.Call(new MotivationDialog(), this.ResumeAfterOptionDialog);
                        break;
                    case ChatOption:
                        context.Call(new ChatDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case HelpOption:
                        context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case ExitOption:
                        await context.PostAsync("Alrighty, well is there anything else I can help you with?");
                        context.Done(true);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync("Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Anything else I can help you with?");
            context.Wait(Redirect);
            //context.Done(true);

        }
        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok")
            {
                this.ShowOptions(context);
            }
            else if (userInput.Contains("help"))
            {
                context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
            }
            else if (userInput.Contains("no") || userInput.Contains("bye"))
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }
          
        }
    }
}

