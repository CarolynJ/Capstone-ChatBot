using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class GoogleCalDialog : IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        public Task StartAsync(IDialogContext context)
        {
            context.PostAsync("How many days' worth of events do you want to see?");
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            bool successfullyParseInt = Int32.TryParse(activity.Text, out int numberOfDays);
            string rfcFormat = "yyyy-MM-dd'T'HH:mm:sszzz";
            var today = DateTime.Now;

            if (!successfullyParseInt)
            {
                await context.PostAsync("Hmm, I didn't understand that. I recommend typing '7' or another integer.");
                context.Wait(MessageReceivedAsync);
            }

            GoogleCalendarApi apiData = await GoogleCalApiCall(today.ToString(rfcFormat));

            await context.PostAsync(apiData.items[0].description);
            
        }
        
        private async Task<GoogleCalendarApi> GoogleCalApiCall(string datetimeInRFC3339)
        {
            GoogleCalendarApi data = new GoogleCalendarApi();

            using (HttpClient client = new HttpClient())
            {
                string APIRequest = "https://www.googleapis.com/calendar/v3/calendars/techelevator.com_23ti34t7igbca7rv9g4kljeihg@group.calendar.google.com/events?timeMax=2017-09-01T14:38:04+00:00&timeMin=2017-08-01T14:38:04+00:00";
                HttpResponseMessage msg = await client.GetAsync(APIRequest);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonDataResponse = await msg.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<GoogleCalendarApi>(jsonDataResponse);
                }
            }

            return data;



        }
    }
}