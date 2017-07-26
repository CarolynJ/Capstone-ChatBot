using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using StudentChatBot.DAL;
using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await context.PostAsync("If motivation and inspiration is what you want, you have come to the right place! Would you like a random quote?");

            
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if(this.motivations.Count == 0)
            {
                this.FetchMotivationList();
            }

            Random rmd = new Random();

            Motivation motive = motivations[rmd.Next(0, motivations.Count)];

            //HeroCard hero = new HeroCard();
            //hero.Images = "~/Content/Images/" + motive.ImageCode;

            await context.PostAsync(motive.Quote);
            await context.PostAsync(motive.QuoteSource);
        }

        private void FetchMotivationList()
        {
            IMotivationDAL dal = new MotivationSQLDAL(connectionString);

            this.motivations = dal.GetAllMotivations();
        }
    }
}