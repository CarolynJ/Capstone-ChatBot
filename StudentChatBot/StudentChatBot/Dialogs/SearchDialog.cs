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
    public class SearchDialog : IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private List<Resource> AllResources { get; set; } = new List<Resource>();

        public Task StartAsync(IDialogContext context)
        {
            context.PostAsync("What are you looking for today? (type exit when you're done searching)");
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;

            if (activity.Text.ToLower().Contains("exit"))
            {
                context.Done(true);
            }
            else
            {
                LuisData data = await LuisDecipher(activity.Text);

                if (data.entities.Length == 0)
                {
                    await context.PostAsync("Sorry, I didn't understand that. Try saying, 'Help me with ...'");
                }
                else
                {
                    string keyword = data.entities[0].entity;

                    ISearchByKeyword dal = new SearchByKeywordSQLDAL(connectionString);
                    this.AllResources = dal.GetResources(keyword);


                    if (this.AllResources.Count > 0)
                    {
                        await context.PostAsync($"There are {this.AllResources.Count} resources available. How many would you like to see?");
                        context.Wait(HowManyResults);
                    }
                    else
                    {
                        await context.PostAsync("Sorry that did not return a resource, try again or enter exit to leave search by keyword");
                    }
                }
            }
        }

        private async Task<LuisData> LuisDecipher(string text)
        {
            text = Uri.EscapeDataString(text);
            LuisData data = new LuisData();

            using (HttpClient client = new HttpClient())
            {
                string APIRequest = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/a6222f36-ec66-4c1d-bad1-f579d8016a0d?subscription-key=7e7ec5656b324a50bacf7929278305d9&timezoneOffset=0&verbose=true&q=" + text;
                HttpResponseMessage msg = await client.GetAsync(APIRequest);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonDataResponse = await msg.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<LuisData>(jsonDataResponse);
                }
            }

            return data;
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

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Would you like to search with a different keyword?");
            context.Wait(Redirect);

        }

        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                await StartAsync(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }



        }
    }
}