using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using StudentChatBot.DAL;
using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class MotivationDialog : IDialog<object>
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tehelper"].ConnectionString;

        private List<Motivation> motivations = new List<Motivation>();

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("If motivation and inspiration is what you want, you have come to the right place!");

            await this.MessageReceivedAsync(context);
        }

        private async Task MessageReceivedAsync(IDialogContext context)
        {
           
            if(this.motivations.Count == 0)
            {
                this.FetchMotivationList();
            }

            Random rmd = new Random();

            Motivation motive = motivations[rmd.Next(0, motivations.Count)];

            Attachment attachment = new Attachment();
            attachment.ContentType = "image/png";
            attachment.ContentUrl = motive.ImageCode;
            var message = context.MakeMessage();
            message.Text = motive.Quote;
            
            message.Attachments.Add(attachment);

            await context.PostAsync(message);
            Thread.Sleep(2000);
            await context.PostAsync("That quote was by " + motive.QuoteSource + ". Would you like another?");

            context.Wait(Continue);

        }

        private async Task Continue(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var nextActivity = await result;

            var response = nextActivity.Text.ToString().ToLower();

            if (response == "yes" || response == "y" || response == "sure" || response == "fine")
            {
              //  IAwaitable<IMessageActivity> forward = result;

                await MessageReceivedAsync(context);
            }
            else
            {
                context.Done(true);
            }

        }

        private void FetchMotivationList()
        {
            IMotivationDAL dal = new MotivationSQLDAL(connectionString);

            this.motivations = dal.GetAllMotivations();
        }
    }
}