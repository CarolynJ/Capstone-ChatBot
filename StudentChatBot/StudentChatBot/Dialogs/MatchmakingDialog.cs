using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using StudentChatBot.DAL;
using StudentChatBot.Models;
using StudentChatBot.Models.Matchmaking;

namespace StudentChatBot.Dialogs
{
    public class MatchmakingDialog : IDialog<object>
    {
        private const string AllCompaniesOption = "View All Companies";
        private const string ViewStudentsScheduleOption = "View a Student's Schedule";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Yay it's matchmaking time, check your schedule or set a reminder to follow up with an employer.");

            this.ShowMatchmakingMenu(context);
        }

        private void ShowMatchmakingMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterMatchmakingMenu, new List<string>()
                { AllCompaniesOption, ViewStudentsScheduleOption, ExitOption },
                "Do any of these options suit your fancy?",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task ResumeAfterMatchmakingMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case AllCompaniesOption:
                    break;

                case ViewStudentsScheduleOption:
                    await context.PostAsync("Enter a student name:");
                    context.Wait(ViewStudentSchedule);
                    break;

                case ExitOption:
                    context.Done(true);
                    break;
            }

        }

        private async Task ViewStudentSchedule(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var userInput = await result;

            IMatchmakingDAL dal = new MatchmakingSQLDAL();
            StudentMatchmakingSchedule studentSchedule = dal.GetStudentSchedule(userInput.Text);

            if (studentSchedule == null)
            {
                await context.PostAsync("Hmm, I couldn't find that student. Check that you're spelling their name correctly.");
            }

            await context.PostAsync($"Here's {studentSchedule.StudentName}'s Schedule: \n {studentSchedule.ToString()}");

        }
    }
}