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
        private const string XSSOption = "Cross site scripting xss";
        private const string OtherOption = "Other";
        private const string ExitOption = "Exit";

        private List<Resource> AllResources { get; set; } = new List<Resource>();


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Looking for help with Module 5?");

            this.ShowModFiveMenu(context);
        }

        private void ShowModFiveMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterModFiveMenu, new List<string>()
                { AuthenticationOption, AuthorizationOption, SqlInjectionOption, XSSOption, OtherOption, ExitOption },
                "Do you see what you're looking for?",
                "Hmm, your intentions weren't clear, try again.",
                2);
        }

        private async Task ResumeAfterModFiveMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case AuthenticationOption:
                    await context.PostAsync("How can you authenticate users?");
                    string keyword = "authentication";
                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                        context.Wait(HowManyResults);
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case AuthorizationOption:
                    await context.PostAsync("Limiting access can also provide security");
                    keyword = "authorization";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                        context.Wait(HowManyResults);
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case SqlInjectionOption:
                    await context.PostAsync("Learn about destruction via SQL injection");
                    keyword = "sqlinjection";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                        context.Wait(HowManyResults);
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case XSSOption:
                    await context.PostAsync("Learn about cross site scripting");
                    keyword = "xss";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    AllResources = dal.GetResources(keyword);

                    if (AllResources.Count > 0)
                    {
                        await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                        context.Wait(HowManyResults);
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource");
                        await ResumeAfterOptionDialog(context, result);
                    }

                    break;

                case OtherOption:
                    context.Call(new SearchDialog(), this.ResumeAfterOtherOptionDialog);
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

        public async Task AllResults(IDialogContext context, IAwaitable<IMessageActivity> result)
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


        private async Task ResumeAfterOtherOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I hope you found a useful resource. I'll return you to the main menu now.");
            context.Done(true);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Would you like to browse for another module 5 resource?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowModFiveMenu(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }

        }


    }
}