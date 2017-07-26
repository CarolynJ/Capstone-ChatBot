using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Collections.Generic;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string SearchOption = "Search";

        private const string ChatOption = "Chat";

        private const string HelpOption = "Help";

        public Task StartAsync(IDialogContext context)
        {
            // context is an object of all chat information specific to this instance of the chat
            // based on THIS context, we call the MessageReceivedAsync method, which handles logic/chat functionality
            context.Wait(MessageReceivedAsync);

            // when that's all done, let's tell the controller that we're done. this task object includes info about success/failure/etc
            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;

            // the user's message is sent back to us in the activity object (aka IAwaitable result)
            // we save the text of the message as a string all in lowercase to a var
            var userInput = activity.Text.ToString().ToLower();

            // if the user's message contains blah blah blah
            if (userInput == "hello" || userInput == "hey" || userInput == "hi")
            {
                // take this context and add a new dialog (the greeting dialog). when it's done, call the ResumeAfterGreetingDialog method to continue

                context.Call(new GreetingDialog(), this.ResumeAfterGreetingDialog);
                
            }
            else
            {
                this.ShowOptions(context);

                // calculate something for us to return
                //int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            }

            // wait 
            //context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterGreetingDialog(IDialogContext context, IAwaitable<object> result)
        {
            //var userInput = await result;

            //await context.PostAsync("done with the greeting command");
            context.Wait(this.MessageReceivedAsync);
            Thread.Sleep(1000);
            await context.PostAsync("Hello, What is your name?");

            // working on getting a response from a user
            //var activity = PromptDialog.Text
            
            //await context.Forward(new NameResponseDialog(), this.ResumeAfterGreetingDialog, activity, CancellationToken.None);

            context.Done(true);
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { SearchOption, ChatOption, HelpOption }, "Are you looking to search for info, get help, or just chat?", "Not a valid option", 3);
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