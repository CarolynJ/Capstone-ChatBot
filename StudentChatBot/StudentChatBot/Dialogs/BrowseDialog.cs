using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class BrowseDialog : IDialog<object>
    {
        private const string Pathway = "Pathway Resources";
        private const string Technical = "Technical Resources";
        private const string MainMenu = "Main Menu";
        

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Which type of resources would you like to browse?");
            this.ShowBrowseOptions(context);
        }

        private void ShowBrowseOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.BrowseOptions, new List<string>()
            { Pathway, Technical, MainMenu},
            " ",
                "Hmm, I didn't understand that, try again.",
                2);
        }
        private async Task BrowseOptions(IDialogContext context, IAwaitable<string> result)
        {
            
            var BrowseOption = await result;
           
            switch (BrowseOption)
            {
                case Pathway:
                    context.Call(new PathwayDialog(), this.ResumeAfterBrowse);
                    break;
                case Technical:
                    context.Call(new TechnicalDialog(), this.ResumeAfterBrowse);
                    break;
                //case MainMenu:
                //    context.Done(true);

            }
        }
        public async Task ResumeAfterBrowse(IDialogContext context, IAwaitable<object> result)
        {
            var BrowseOption = await result;
            context.Done(true);
        }

    }
}