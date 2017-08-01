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
    public class Mod3Dialog : IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private const string HttpOption = "Http";
        private const string CSSOption = "CSS";
        // private const string MVCOption = "MVC";
        private const string OtherOption = "Other";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Looking for help with Module 3?");

            this.ShowModThreeMenu(context);
        }

        private void ShowModThreeMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterModThreeMenu, new List<string>()
                { HttpOption, CSSOption, OtherOption, ExitOption },
                "Do you see what you're looking for?",
                "Hmm, your intentions weren't clear, try again.",
                2);
        }

        private async Task ResumeAfterModThreeMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case HttpOption:
                    await context.PostAsync("Http internet basics");
                    string keyword = "http";
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

                case CSSOption:
                    await context.PostAsync("In the end you want a good user experience");
                    keyword = "css";
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
            await context.PostAsync("Would you like to browse for another module 3 resource?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowModThreeMenu(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }


        }
    }
}