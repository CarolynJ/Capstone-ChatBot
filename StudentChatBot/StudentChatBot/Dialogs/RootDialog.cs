using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            // context is an object of all chat information specific to this instance of the chat
            // based on THIS context, we call the MessageReceivedAsync method, which handles logic/chat functionality
            context.Wait(MessageReceivedAsync);

            // when that's all done, let's tell the controller that we're done. this task object includes info about success/failure/etc
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {

            var activity = await result as Activity;

            // the user's message is sent back to us in the activity object (aka IAwaitable result)
            // we save the text of the message as a string all in lowercase to a var
            var userInput = activity.Text.ToString().ToLower();

            // if the user's message contains blah blah blah
            if (userInput.Contains("hello") || userInput.Contains("hey") || userInput.Contains("hi"))
            {
                // take this context and add a new dialog (the greeting dialog). when it's done, call the ResumeAfterGreetingDialog method to continue
                await context.Forward(new GreetingDialog(), this.ResumeAfterGreetingDialog, activity, CancellationToken.None);
            }
            else
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            }
            
            // wait 
            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterGreetingDialog(IDialogContext context, IAwaitable<object> result)
        {
            //var userInput = await result;

            await context.PostAsync("done with the greeting command");

            context.Wait(this.MessageReceivedAsync);
        }
    }
}