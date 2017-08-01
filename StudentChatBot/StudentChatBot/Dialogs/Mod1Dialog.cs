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
    public class Mod1Dialog: IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private const string GitOption = "Git";
        private const string VariablesOption = "Variables";
        private const string ObjectsOption = "OOP";
        private const string ClassesOption = "Classes";
        private const string TestingOption = "Testing";
        private const string OtherOption = "Other";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Looking for resources to study Module 1 topics?");

            this.ShowModOneMenu(context);
        }

        private void ShowModOneMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterModOneMenu, new List<string>()
                { GitOption, VariablesOption, ObjectsOption, ClassesOption, TestingOption, OtherOption, ExitOption },
                "Do you see what you're looking for?",
                "Hmm, your intentions weren't clear, try again.",
                2);
        }

        private async Task ResumeAfterModOneMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case GitOption:
                    await context.PostAsync("You can read up on git");
                    string keyword = "git";
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

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case VariablesOption:
                    await context.PostAsync("Need some help with variables?");
                    keyword = "variables";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

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

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case ObjectsOption:
                    await context.PostAsync("Object Oriented programming (OOP)");
                    keyword = "oop";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

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

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case ClassesOption:
                    await context.PostAsync("Classes in C# are key to understand");
                    keyword = "classes";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

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

                    await ResumeAfterOptionDialog(context, result);

                    break;

                case TestingOption:
                    await context.PostAsync("Learn to test your own code.");
                    keyword = "testing";
                    dal = new SearchByKeywordSQLDAL(connectionString);
                    resources = dal.GetResources(keyword);

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
            await context.PostAsync("Would you like to browse for another module 1 resource?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowModOneMenu(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }

        }
    }
}
