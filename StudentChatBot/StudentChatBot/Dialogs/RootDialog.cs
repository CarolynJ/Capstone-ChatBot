﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Collections.Generic;
using StudentChatBot.DAL;

namespace StudentChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private const string SearchOption = "Search By Keyword";
        private const string BrowseOption = "Browse";
        //private const string ChatOption = "Chat with Me";
        private const string HelpOption = "Get Help";
        private const string ExitOption = "Exit";
        private const string MotivationOption = "Get Motivated";
        private const string MatchmakingOption = "Matchmaking Schedule";

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "hello" || userInput == "hey" || userInput == "hi" || userInput == "greetings" || userInput.Contains("good") || 
                userInput.Contains("morning") || userInput.Contains("afternoon") ||  userInput.Contains("hi ") || userInput.Contains("howdy")
                || userInput.Contains("hey") || userInput.Contains("evening") || userInput.Contains("bonjour") || userInput.Contains("ciao")
                || userInput.Contains("up") || userInput.Contains("salutations"))
            {
                await context.Forward(new GreetingDialog(), this.ResumeAfterGreetingDialog, activity, CancellationToken.None);
            }
            else if (userInput.Contains("menu"))
            {
                this.ShowOptions(context);
            }
            else if (userInput.Contains("help"))
            {
                context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
            }
            else if (userInput.Contains("search"))
            {
                context.Call(new SearchDialog(), this.ResumeAfterOptionDialog);
            }
            else
            {
                await context.PostAsync("Sorry, I didn't understand that command. Please type 'menu' for more information. Or greet me to start a conversation... :)");
                context.Done(true);
            }
        }

        private async Task ResumeAfterGreetingDialog(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            context.Call(new NameResponseDialog(), this.ResumeAfterNameResponse);
        }
        public async Task ResumeAfterNameResponse(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            this.ShowOptions(context);
        }

        private void ShowOptions(IDialogContext context)
        {
            string header = "Are you looking to search for info, get help, or get motivation?";
            Random r = new Random();
            int num = r.Next(0, 10);
            if(num == 0)
            {
                header = "Choose Wisely";
            }
            if(num == 1)
            {
                header = "Based on your lethargic keystrokes I would suggest motivation";
            }
            if(num == 2)
            {
                header = "Hi! Please choose one of these fine options!";
            }

            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>()
                {  SearchOption, BrowseOption, MotivationOption, MatchmakingOption, HelpOption, ExitOption },
                header,
                "Hmmm, I didn't understand that, try again...",
                2);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case SearchOption:
                        context.Call(new SearchDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case BrowseOption:
                        context.Call(new BrowseDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case MotivationOption:
                        context.Call(new MotivationDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case HelpOption:
                        context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case MatchmakingOption:
                        context.Call(new MatchmakingDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case ExitOption:
                        await context.PostAsync("Alrighty, well is there anything else I can help you with?");
                        context.Wait(Redirect);
                  
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync("Ooops! Too many attemps :(  But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            string asterisk = "*";
            var message = await result;
            await context.PostAsync("Anything else I can help you with?");
            Random r = new Random();
            int num = r.Next(0, 7);
            if(num == 1)
            {
                await context.PostAsync($"{asterisk}Please ask me something, I'm lonely :({asterisk}");
            }
            if(num == 2)
            {
                await context.PostAsync($"{asterisk}I'm really not in the mood to help you much more though...{asterisk}");
            }
            context.Wait(Redirect);

        }
        private async Task Redirect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;
            var userInput = activity.Text.ToString().ToLower();

            if (userInput == "yes" || userInput == "y" || userInput == "ok" || userInput == "menu")
            {
                this.ShowOptions(context);
            }
            else if (userInput.Contains("help"))
            {
                context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
            }
            else if (userInput.Contains("?"))
            {
                await context.PostAsync("Thank you for validating my existence, I really do not feel like answering that question though.");
                this.ShowOptions(context);
            }
            else
            {
                await context.PostAsync("Please come again. Have a nice day!");
                context.Done(true);
            }
          
        }
    }
}

