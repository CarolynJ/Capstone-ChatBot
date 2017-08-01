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
    public class Mod4Dialog : IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private const string JavaScriptOption = "JavaScript";
        private const string JQueryOption = "JQuery";
        private const string APIOption = "API";
        private const string OtherOption = "Other";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Looking for help with Module 4?");

            this.ShowModFourMenu(context);
        }

        private void ShowModFourMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterModFourMenu, new List<string>()
                { JavaScriptOption, JQueryOption, APIOption, OtherOption, ExitOption },
                "Do you see what you're looking for?",
                "Hmm, your intentions weren't clear, try again.",
                2);
        }

        private async Task ResumeAfterModFourMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case JavaScriptOption:
                    await context.PostAsync("JavaScript");
                    string keyword = "javascript";
                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    List<Resource> resources = dal.GetResources(keyword);

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
                        await context.PostAsync("Sorry JavaScript don't care");
                    }

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case JQueryOption:
                    await context.PostAsync("Learn to animate your website");
                    keyword = "JQuery";
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

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case APIOption:
                    await context.PostAsync("See what you can do with APIs");
                    keyword = "api";
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

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case OtherOption:
                    context.Call(new SearchDialog(), this.ResumeAfterOtherOptionDialog);
                    break;

                case ExitOption:
                    context.Done(true);
                    break;
            }

        }

        private async Task ResumeAfterOtherOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I hope you found a useful resource. I'll return you to the main menu now.");
            context.Done(true);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Would you like to browse for another module 4 resource?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowModFourMenu(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }


        }
    }
}