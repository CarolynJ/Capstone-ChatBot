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

        private const string SearchOption = "Search for Information";
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
            else if (userInput.Contains("help"))
            {
                this.ShowOptions(context);
            }
            else
            {
                await context.PostAsync("Sorry, I didn't understand that command. Please type help for more information.");
                context.Done(true);
            }
        }

        private async Task ResumeAfterGreetingDialog(IDialogContext context, IAwaitable<object> result)
        {
            Thread.Sleep(1000);
            await context.PostAsync("So, what can I help you with today?");

            context.Done(true);
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>()
                { HelpOption, SearchOption, MotivationOption, ChatOption, ExitOption }, 
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

                    case ChatOption:
                        context.Call(new ChatDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case HelpOption:
                        context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case MotivationOption:
                        context.Call(new MotivationDialog(), this.ResumeAfterOptionDialog);
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
            try
            {
                var message = await result;

                await context.PostAsync("Anything else I can help you with?");
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }



    }
}