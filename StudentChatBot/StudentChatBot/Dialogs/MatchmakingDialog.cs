using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace StudentChatBot.Dialogs
{
    public class MatchmakingDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Yay it's matchmaking time, check your schedule or set a reminder to follow up with an employer.");

            //this.ShowMatchmakingMenu(context);
        }

    }
}