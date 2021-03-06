﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using StudentChatBot.DAL;
using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;


namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class InterviewDialog : IDialog<object>
    {

        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private const string PracticeQuestionsOption = "Practice Questions";
        private const string PhoneOption = "Phone Interviews";
        private const string BehavioralOption = "Behavioral Interview Preparation";
        private const string FollowUpOption = "Interview Follow Up";
        private const string PreparationOption = "Preparation";
        private const string ExitOption = "Exit";


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("We have a lot of resources on interviewing.");

            this.ShowInterviewMenu(context);
        }

        private void ShowInterviewMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterInterviewMenu, new List<string>()
                { PracticeQuestionsOption, PhoneOption, BehavioralOption, FollowUpOption, PreparationOption, ExitOption },
                " ",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task ResumeAfterInterviewMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case PracticeQuestionsOption:
                    await context.PostAsync("You can get a lot of practice with these questions");
                    string keyword = "questions";
                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    List<Resource> resources = dal.GetResources(keyword);
                   

                    if (resources.Count > 0)
                    {
                        foreach (Resource r in resources)
                        {
                            string title = r.ResourceTitle.ToString();
                            string content = r.ResourceContent.ToString();
                            var markdownContent = $"[{title}]({content})";

                            await context.PostAsync(markdownContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case PhoneOption:
                    await context.PostAsync("Here are some tips to prepare for phone interviews");
                    keyword = "phone";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
                    {
                        foreach (Resource r in resources)
                        {
                            string title = r.ResourceTitle.ToString();
                            string content = r.ResourceContent.ToString();
                            var markdownContent = $"[{title}]({content})";

                            await context.PostAsync(markdownContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case BehavioralOption:
                    await context.PostAsync("Use the STAR method to prepare stellar answers!");
                    keyword = "behavioral";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
                    {
                        foreach (Resource r in resources)
                        {
                            string title = r.ResourceTitle.ToString();
                            string content = r.ResourceContent.ToString();
                            var markdownContent = $"[{title}]({content})";

                            await context.PostAsync(markdownContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case FollowUpOption:
                    await context.PostAsync("Remember to follow up");
                    keyword = "follow up";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
                    {
                        foreach (Resource r in resources)
                        {
                            string title = r.ResourceTitle.ToString();
                            string content = r.ResourceContent.ToString();
                            var markdownContent = $"[{title}]({content})";

                            await context.PostAsync(markdownContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case PreparationOption:
                    await context.PostAsync("Interview Preparation");
                    keyword = "preparation";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
                    {
                        foreach (Resource r in resources)
                        {
                            string title = r.ResourceTitle.ToString();
                            string content = r.ResourceContent.ToString();
                            var markdownContent = $"[{title}]({content})";

                            await context.PostAsync(markdownContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }

                    await ResumeAfterOptionDialog(context, result);

                    break;


                case ExitOption:
                    context.Done(true);
                    break;
            }
        }

        public async Task ResumeAfterInterviewDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I hope you found a useful resource to improve your job search.");
            context.Done(true);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Would you like to browse for another resource on interviewing?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowInterviewMenu(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }

        }
    }
}
