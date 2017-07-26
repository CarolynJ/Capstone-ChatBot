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
        private IMotivationDAL motivationDal;

        private const string SearchOption = "Search";

        private const string ChatOption = "Chat";

        private const string HelpOption = "Help";

        private const string MotivationOption = "Motivation";

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
                //context.Call(new GreetingDialog(), this.ResumeAfterGreetingDialog);
                await context.Forward(new GreetingDialog(), this.ResumeAfterGreetingDialog, activity, CancellationToken.None);

            }
            else
            {
                this.ShowOptions(context);
                
            }

            //context.Wait(MessageReceivedAsync);
        }

        private void GiveGreeting(IDialogContext context)
        {
            throw new NotImplementedException();
        }

        private async Task ResumeAfterGreetingDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(this.MessageReceivedAsync);
            Thread.Sleep(1000);
            await context.PostAsync("Hello, What is your name?");
            
            context.Done(true);
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { SearchOption, ChatOption, HelpOption, MotivationOption }, "Are you looking to search for info, get help, chat, or get motivation?", "Not a valid option", 3);
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


                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
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