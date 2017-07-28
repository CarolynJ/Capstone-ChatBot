﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
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
                    await context.PostAsync("Sorry, I didn't understand that. Try saying, 'Help me with OOP'");
                }
                else
                {
                    await context.PostAsync(data.entities[0].entity);
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

                if(msg.IsSuccessStatusCode)
                {
                    var jsonDataResponse = await msg.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<LuisData>(jsonDataResponse);
                }         
            }

            return data;
        }
    }
}