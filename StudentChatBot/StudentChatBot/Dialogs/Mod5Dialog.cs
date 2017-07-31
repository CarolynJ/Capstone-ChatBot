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
    public class Mod5Dialog: IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private const string AuthenticationOption = "Authentication";
        private const string AuthorizationOption = "Authorization";
        private const string SqlInjectionOption = "SQL Injection";
        private const string OtherOption = "Other";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Looking for help with Module 3?");

            this.ShowPathwayMenu(context);
        }

        private void ShowPathwayMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterPathwayMenu, new List<string>()
                { AuthenticationOption, AuthorizationOption, SqlInjectionOption, OtherOption, ExitOption },
                "Do you see what you're looking for?",
                "Hmm, your intentions weren't clear, try again.",
                2);
        }

        private async Task ResumeAfterPathwayMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case AuthenticationOption:
                    await context.PostAsync("How can you authenticate users?");
                    string keyword = "authentication";
                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    List<Resource> resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
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

                case AuthorizationOption:
                    await context.PostAsync("Limiting access can also provide security");
                    keyword = "authorization";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
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

                case SqlInjectionOption:
                    await context.PostAsync("Learn about destruction via SQL injection");
                    keyword = "sqlinjection";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

                    if (resources.Count > 0)
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
                    context.Call(new SearchDialog(), this.ResumeAfterModThreeDialog);
                    break;

                case ExitOption:
                    context.Done(true);
                    break;
            }
            
        }

        private async Task ResumeAfterModThreeDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I hope you found a useful resource. I'll return you to the main menu now.");
            context.Done(true);
        }

    }
}