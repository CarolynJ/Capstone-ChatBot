﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using StudentChatBot.DAL;
using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;


namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class PathwayDialog : IDialog<object>
    {

        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private const string PWResumeOption = "Résumé";
        private const string PWElevatorPitchOption = "Elevator Pitch";
        private const string PWInterviewOption = "Interviewing";
        private const string PWLinkedInOption = "LinkedIn";
        private const string PWUpcomingEventsOption = "Upcoming Pathway Events";
        private const string OtherOption = "Other";
        private const string ExitOption = "Exit";

        private List<Resource> AllResources { get; set; } = new List<Resource>();


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Get your Pathway Resources here!");

            this.ShowPathwayMenu(context);
        }

        private void ShowPathwayMenu(IDialogContext context)
        {
            string header = "The Pathway Program is extremely important to helping you get a good job.";
            Random r = new Random();
            int num = r.Next(0, 6);
            if(num == 1)
            {
                header = "The path of the righteous man is beset on all sides by the inequities of the selfish and the tyranny of evil men. Just checking to see if you are actually reading this.";
            }
            PromptDialog.Choice(context, this.ResumeAfterPathwayMenu, new List<string>()
                { PWResumeOption, PWElevatorPitchOption, PWInterviewOption, PWLinkedInOption, PWUpcomingEventsOption, OtherOption, ExitOption },
                header,
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task ResumeAfterPathwayMenu(IDialogContext context, IAwaitable<object> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case PWResumeOption:
                    //await context.PostAsync("resume option selected");
                    string keyword = "resume";
                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        if (AllResources.Count == 1)
                        {
                            await AllResults(context, result);
                        }
                        else
                        {
                            await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                            context.Wait(HowManyResults);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case PWElevatorPitchOption:
                    //await context.PostAsync("elevator pitch selected");
                    keyword = "elevator";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        if (AllResources.Count == 1)
                        {
                            await AllResults(context, result);
                        }
                        else
                        {
                            await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                            context.Wait(HowManyResults);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case PWInterviewOption:
                    context.Call(new InterviewDialog(), this.ResumeAfterPathwayDialog);
                    break;

                case PWLinkedInOption:
                    //await context.PostAsync("you need help with linkedin");
                    keyword = "linkedin";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        if (AllResources.Count == 1)
                        {
                            await AllResults(context, result);
                        }
                        else
                        {
                            await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                            context.Wait(HowManyResults);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case PWUpcomingEventsOption:
                    //await context.PostAsync("view upcoming pathway events");
                    keyword = "events";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        if (AllResources.Count == 1)
                        {
                            await AllResults(context, result);
                        }
                        else
                        {
                            await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                            context.Wait(HowManyResults);
                        }

                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case OtherOption:
                    context.Call(new SearchDialog(), this.ResumeAfterPathwayDialog);
                    break;

                case ExitOption:
                    context.Done(true);
                    break;
            }
        }

        public async Task HowManyResults(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            var userInput = activity.Text.ToString();
            int userCount = 0;
            Int32.TryParse(userInput, out userCount);
            if (userCount <= 0 || userCount > this.AllResources.Count)
            {
                await context.PostAsync("That was not a valid response, all available resources will be shown");
                Thread.Sleep(4000);
                await AllResults(context, result);
            }
            else
            {
                for (int i = 0; i < userCount; i++)
                {
                    string title = AllResources[i].ResourceTitle.ToString();
                    string content = AllResources[i].ResourceContent.ToString();
                    var markdownContent = $"[{title}]({content})";

                    await context.PostAsync(markdownContent);
                }
                await ResumeAfterOptionDialog(context, result);
            }
        }

        public async Task AllResults(IDialogContext context, IAwaitable<object> result)
        {
            foreach (Resource r in this.AllResources)
            {
                string title = r.ResourceTitle.ToString();
                string content = r.ResourceContent.ToString();
                var markdownContent = $"[{title}]({content})";

                await context.PostAsync(markdownContent);
            }
            await ResumeAfterOptionDialog(context, result);
        }


        public async Task ResumeAfterPathwayDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I hope you found a useful resource to improve your job search.");
            context.Done(true);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Would you like to browse for another pathway resource?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowPathwayMenu(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }

        }
    }
}
