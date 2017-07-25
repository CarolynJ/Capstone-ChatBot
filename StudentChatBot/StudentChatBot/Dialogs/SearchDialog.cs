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
        private const string TechnicalOption = "Techincal Questions";

        private const string PathwayOption = "Pathway Resources";

        private const string JoshWhiteBoardOption = "Josh White Board Emulator";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to Search!");

            this.ShowOptions(context);

        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { TechnicalOption, PathwayOption, JoshWhiteBoardOption }, "Are you looking to search for Technical info, Pathway resources, or use the Josh Tulchoski Spell Check Emulator?", "Not a valid option", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case TechnicalOption:
                        context.Call(new TechnicalDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case PathwayOption:
                        context.Call(new PathwayDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case JoshWhiteBoardOption:
                        context.Call(new JoshWhiteBoardDialog(), this.ResumeAfterOptionDialog);
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