using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class PathwayDialog : IDialog<object>
    {
        private const string PWResumeOption = "Résumé";
        private const string PWElevatorPitchOption = "Elevator Pitch";
        private const string PWInterviewOption = "Interviewing";
        private const string PWLinkedInOption = "LinkedIn";
        private const string PWUpcomingEventsOption = "Upcoming Pathway Events";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Get your Pathway Resources here!");

            this.ShowPathwayMenu(context);
        }

        private void ShowPathwayMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterPathwayMenu, new List<string>()
                { PWResumeOption, PWElevatorPitchOption, PWInterviewOption, PWLinkedInOption, PWUpcomingEventsOption, ExitOption },
                "Do any of these options suit your fancy?",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task ResumeAfterPathwayMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case PWResumeOption:
                    await context.PostAsync("resume option selected");
                    break;
                case PWElevatorPitchOption:
                    await context.PostAsync("elevator pitch selected");
                    break;
                case PWInterviewOption:
                    await context.PostAsync("interviewing help selected");
                    break;
                case PWLinkedInOption:
                    await context.PostAsync("you need help with linkedin");
                    break;
                case PWUpcomingEventsOption:
                    await context.PostAsync("view upcoming pathway events");
                    break;
                case ExitOption:
                    context.Done(true);
                    break;
            }

            context.Done(true);
        }
    }
}