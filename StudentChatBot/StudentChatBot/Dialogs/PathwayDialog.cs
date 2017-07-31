using Microsoft.Bot.Builder.Dialogs;
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
        

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Get your Pathway Resources here!");

            this.ShowPathwayMenu(context);
        }

        private void ShowPathwayMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterPathwayMenu, new List<string>()
                { PWResumeOption, PWElevatorPitchOption, PWInterviewOption, PWLinkedInOption, PWUpcomingEventsOption, OtherOption, ExitOption },
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
                    string keyword = "resume";
                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    List<Resource> resources = dal.GetResources(keyword);

                    if (resources != null)
                    {
                        foreach(Resource r in resources)
                        {
                            await context.PostAsync(r.ResourceTitle);
                            await context.PostAsync(r.ResourceContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }

                    break;

                case PWElevatorPitchOption:
                    await context.PostAsync("elevator pitch selected");
                    keyword = "elevator";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources != null)
                    {
                        foreach (Resource r in resources)
                        {
                            await context.PostAsync(r.ResourceTitle);
                            await context.PostAsync(r.ResourceContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }
                    break;

                case PWInterviewOption:
                    await context.PostAsync("interviewing help selected");
                    keyword = "interview";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources != null)
                    {
                        foreach (Resource r in resources)
                        {
                            await context.PostAsync(r.ResourceTitle);
                            await context.PostAsync(r.ResourceContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }
                    break;

                case PWLinkedInOption:
                    await context.PostAsync("you need help with linkedin");
                    keyword = "linkedin";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources != null)
                    {
                        foreach (Resource r in resources)
                        {
                            await context.PostAsync(r.ResourceTitle);
                            await context.PostAsync(r.ResourceContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                    }
                    break;
                case PWUpcomingEventsOption:
                    await context.PostAsync("view upcoming pathway events");
                    keyword = "events";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources != null)
                    {
                        foreach (Resource r in resources)
                        {

                            await context.PostAsync(r.ResourceTitle);
                            await context.PostAsync(r.ResourceContent);
                        }
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
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

            public async Task ResumeAfterPathwayDialog(IDialogContext context, IAwaitable<object> result)
            {
                await context.PostAsync("I hope you found a useful resource to improve your job search.");
                context.Done(true);
            }
        }
    }
