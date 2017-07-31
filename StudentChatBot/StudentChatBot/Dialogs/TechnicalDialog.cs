using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;


namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class TechnicalDialog : IDialog<object>
    {
        private const string Mod1 = "Module1 C#";
        private const string Mod2 = "Module2 SQL";
        private const string Mod3 = "Module3 ASP.Net MVC";
        private const string Mod4 = "Module4 JQuery";
        private const string Mod5 = "Module5 Security";
        private const string ExitOption = "Exit";

        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("Sharpen your technical skills here!");

            this.ShowTechnicalMenu(context);
        }

        private void ShowTechnicalMenu(IDialogContext context)
        {
            PromptDialog.Choice(context, this.ResumeAfterTechnicalMenu, new List<string>()
                { Mod1, Mod2, Mod3, Mod4, Mod5, ExitOption },
                "Which module do you need help with?",
                "Hmm, I didn't understand that, try again.",
                2);
        }

        private async Task ResumeAfterTechnicalMenu(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch (optionSelected)
            {
                case Mod1:
                    await context.PostAsync("learn more about C# and .Net fundementals");
                    context.Call(new Mod1Dialog(), this.ResumeAfterLookup);
                    break;
                case Mod2:
                    await context.PostAsync("Check out these SQL resources");
                    context.Call(new Mod2Dialog(), this.ResumeAfterLookup);
                    break;
                case Mod3:
                    await context.PostAsync("ASP.Net MVC resources");
                    context.Call(new Mod3Dialog(), this.ResumeAfterLookup);
                    break;
                case Mod4:
                    await context.PostAsync("here are some reources for JQuery");
                    context.Call(new Mod4Dialog(), this.ResumeAfterLookup);
                    break;
                case Mod5:
                    await context.PostAsync("Security resources");
                    context.Call(new Mod5Dialog(), this.ResumeAfterLookup);
                    break;
                case ExitOption:
                    context.Done(true);
                    break;
            }
        }
        public async Task ResumeAfterLookup(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("back in technicalDialog");
            var BrowseOption = await result;
            //context.Done(true);
        }
    }
}