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
    public class SearchDialog : IDialog<object>
    {
        private const string KeywordOption = "Search by Keyword";
        private const string Pathway = "Pathway Resources";
        private const string Technical = "Technical Resources";
        private const string ExitOption = "Go Back to Previous Menu";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to Search!");

            this.ShowOptions(context);
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>()
                { KeywordOption, Pathway, Technical, ExitOption }, 
                "", 
                "Hmm, I didn't understand that, try again.", 
                2);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case Pathway:
                        context.Call(new PathwayDialog(), this.ResumeAfterOptionDialog);
                        break;
                    case Technical:
                        context.Call(new TechnicalDialog(), this.ResumeAfterOptionDialog);
                        break;
                    case ExitOption:
                        context.Done(true);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Done(true);
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
                context.Done(true);
            }
        }



    }
}