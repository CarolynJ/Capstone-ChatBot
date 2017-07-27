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
        private const string Pathway = "Pathway Resources";
        private const string Technical = "Technical Resources";
        private const string Quote = "Motivational Quote";
        private const string Search = "Search";
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
                { Pathway, Technical, Quote, Search, Navigation, ExitOption },
                "What can I help you with",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task HelpMenuActions(IDialogContext context, IAwaitable<string> result)
        {
            var HelpOption = await result;

            switch (HelpOption)
            {
                case Pathway:
                    context.Call(new PathwayDialog(), this.ResumeAfterHelpMenu);
                    break;
                case Technical:
                    context.Call(new TechnicalDialog(), this.ResumeAfterHelpMenu);
                    break;
                case Quote:
                    context.Call(new MotivationDialog(), this.ResumeAfterHelpMenu);
                    break;
                case Search:
                    context.Call(new SearchDialog(), this.ResumeAfterHelpMenu);
                    break;
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