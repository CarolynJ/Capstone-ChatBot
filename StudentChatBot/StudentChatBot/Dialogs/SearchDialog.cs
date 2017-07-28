using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
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
        //private const string KeywordOption = "Search by Keyword";
        //private const string Pathway = "Pathway Resources";
        //private const string Technical = "Technical Resources";
        //private const string ExitOption = "Go Back to Previous Menu";

        public Task StartAsync(IDialogContext context)
        {
            context.PostAsync("What are you looking for today?");
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            await context.PostAsync("you said: " + activity.Text.ToString());
            var thing = LuisDecipher(activity.Text);

            //await context.PostAsync(luisResponse.Text);
        }

        private static async Task<string> LuisDecipher(string text)
        {
            text = Uri.EscapeDataString(text);

            using (HttpClient client = new HttpClient())
            {
                string APIRequest = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/a6222f36-ec66-4c1d-bad1-f579d8016a0d?subscription-key=7e7ec5656b324a50bacf7929278305d9&timezoneOffset=0&verbose=true&q=" + text;
                HttpResponseMessage msg = await client.GetAsync(APIRequest);

                if(msg.IsSuccessStatusCode)
                {
                    var jsonDataResponse = await msg.Content.ReadAsStringAsync();
                    return jsonDataResponse;
                }

                return string.Empty;
                

            }
        }
    }
}