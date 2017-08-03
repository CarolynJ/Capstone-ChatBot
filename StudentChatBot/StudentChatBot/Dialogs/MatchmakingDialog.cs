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
using System.Threading;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class MatchmakingDialog : IDialog<object>
    {
        private const string AllCompaniesOption = "View All Companies";
        private const string ViewStudentsScheduleOption = "View a Student's Schedule";
        private const string ViewCompanyContactInfoOption = "Get a Company's Contact Info";
        private const string ExitOption = "Exit";


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Yay it's matchmaking time, view all companies coming to matchmaking or check your schedule.");

            this.ShowMatchmakingMenu(context);
        }

        private void ShowMatchmakingMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterMatchmakingMenu, new List<string>()
                { ViewStudentsScheduleOption, AllCompaniesOption, ViewCompanyContactInfoOption, ExitOption },
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
                    Thread.Sleep(1000);
                    ShowMatchmakingMenu(context);
                    break;

                case ViewCompanyContactInfoOption:
                    await context.PostAsync("What company do you need contact information for?");
                    context.Wait(GetCompanyContactInfo);
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

        private async Task GetCompanyContactInfo(IDialogContext context, IAwaitable<object> result)
        {
            var userInput = (await result as Activity).Text;

            if (userInput == "exit")
            {
                ShowMatchmakingMenu(context);
                return;
            }

            IMatchmakingDAL dal = new MatchmakingSQLDAL();
            CompanyContact companyContactInfo = dal.GetCompanyContactInfo(userInput);

            if (companyContactInfo.CompanyName == null)
            {
                await context.PostAsync("Hmm, I didn't understand that. Check your spelling or try another company.");
            }
            else
            {
                await context.PostAsync(companyContactInfo.ToString());
                Thread.Sleep(1000);
                await context.PostAsync("If you want another company's contact information, go ahead and type the company name. Otherwise you can say 'exit' and I'll return you to the main menu.");
            }
        }

        private async Task ViewStudentSchedule(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var userInput = (await result as Activity).Text;

            if (userInput == "exit")
            {
                ShowMatchmakingMenu(context);
                return;
            }

            IMatchmakingDAL dal = new MatchmakingSQLDAL();
            StudentMatchmakingSchedule studentSchedule = dal.GetStudentSchedule(userInput);

            if (studentSchedule == null)
            {
                await context.PostAsync("Hmm, I couldn't find that student. Check that you're spelling their name correctly.");
            }

            await context.PostAsync($"Here's {studentSchedule.StudentName}'s Schedule: \n {studentSchedule.ToString()}");
            Thread.Sleep(1000);
            await context.PostAsync("Enter another student's name if you want to view their schedule. Otherwise you can say 'exit' to go back to the Matchmaking Menu.");
        }

 
    }
}