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
    [Serializable]
    public class MatchmakingDialog : IDialog<object>
    {
        private const string AllCompaniesOption = "View All Companies";
        private const string ViewStudentsScheduleOption = "View a Student's Schedule";
        private const string ExitOption = "Exit";


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Yay it's matchmaking time, view all companies coming to matchmaking or check your schedule.");

            this.ShowMatchmakingMenu(context);
        }

        private void ShowMatchmakingMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterMatchmakingMenu, new List<string>()
                { ViewStudentsScheduleOption, AllCompaniesOption, ExitOption },
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
                    MatchmakingSQLDAL dal = new MatchmakingSQLDAL();
                    List<string> allCompanies = dal.GetListOfAllAttendingCompanies();
                    string output = "";

                    if (allCompanies.Count > 0)
                    {
                        foreach(string str in allCompanies)
                        {
                            output += str + "\n\n";
                        }
                    }
                    else
                    {
                        await context.PostAsync("Hmm, something went wrong and I couldn't find any companies...");
                    }

                    await context.PostAsync(output);
                    ShowMatchmakingMenu(context);

                    break;

                case ViewStudentsScheduleOption:
                    await context.PostAsync("Which student's schedule are you interested in?");
                    context.Wait(ViewStudentSchedule);
                    break;

                case ExitOption:
                    context.Done(true);
                    break;
            }
        }

        private void ViewAllCompanies(IDialogContext context)
        {
            throw new NotImplementedException();
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