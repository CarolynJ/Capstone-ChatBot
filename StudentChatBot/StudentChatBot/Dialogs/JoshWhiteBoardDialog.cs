using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class JoshWhiteBoardDialog: IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to Josh's White Board Simulator! Enter a sentence or a question and see how Josh would write it on the board.");

            context.Wait(MessageReceivedAsync);

        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var userInput = activity.Text.ToString();
            string[] words = userInput.Split(' ').ToArray();
            List<string> Output = new List<string>();
            Random rmd = new Random();
            int randomNum;

            foreach (String word in words)
            {
                var newWord = word;
                if (word.Length > 3)
                {
                    randomNum = rmd.Next(0, word.Length - 1);
                    newWord = word.Substring(0, randomNum) + word.Substring(randomNum + 1);

                }
                Output.Add(newWord);
            }
            await context.PostAsync(string.Join(" ", Output));
            await context.PostAsync("Would you like to try it again?");

            context.Wait(Continue);

        }

        private async Task Continue(IDialogContext context, IAwaitable<object> result)
        {

            var nextActivity = await result as Activity;

            var response = nextActivity.Text.ToString();

            if (response == "yes" || response == "y")
            {

                context.Wait(MessageReceivedAsync);
            }

            context.Done(true);
        }

    }
}