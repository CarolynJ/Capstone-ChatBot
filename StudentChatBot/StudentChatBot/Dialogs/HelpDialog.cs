using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class HelpDialog : IDialog<object>
    {
 
        private const string Navigation = "How to get around";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("What do you need help with?");

            this.ShowHelpMenu(context);
        }
        private void ShowHelpMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.HelpMenuActions, new List<string>()
                { Navigation, ExitOption },
                "What can I help you with",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task HelpMenuActions(IDialogContext context, IAwaitable<string> result)
        {
            var HelpOption = await result;

            switch (HelpOption)
            {
                case Navigation:
                    context.Call(new NavigationDialog(), this.ResumeAfterHelpMenu);
                    break;
                case ExitOption:
                    context.Done(true);
                    break;
            }
        }

        public async Task ResumeAfterHelpMenu(IDialogContext context, IAwaitable<object> result)
        {
            var HelpOption = await result;

            context.Done(true);
        }
    }
}