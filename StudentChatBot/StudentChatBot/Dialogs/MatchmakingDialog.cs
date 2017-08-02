using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace StudentChatBot.Dialogs
{
    public class MatchmakingDialog : IDialog<object>
    {
        private const string Schedule = "Matchmaking Schedule";
        private const string Reminder = "Get a Reminder";
        private const string Exit = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Yay it's matchmaking time, check your schedule or set a reminder to follow up with an employer.");

            this.ShowMatchmakingOptions(context);
        }

        private void ShowMatchmakingOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.BrowseOptions, new List<string>()
            { Schedule, Reminder, Exit},
            " ",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task BrowseOptions(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                //case Schedule

                case Exit:
                    context.Done(true);
                    break;

            }
        }



    }
}