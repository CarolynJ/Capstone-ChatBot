using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using StudentChatBot;
using System.Security.AccessControl;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System.Threading.Tasks;

namespace StudentChatBot
{
    public enum MainMenuOptions { PathwayResources, TechnicalResources, MotivationalQuotes };
    public enum ContactMethod { IGNORE, Telephone, SMS, Email };

    public class ContextConstants
    {
        public const string UserNameKey = "UserName";

    }

    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    public class UserInfo
    {
        [Prompt("Hello. What is your name?")]
        public string Name { get; set; }
        [Prompt("What's your number?")]
        public string CellPhoneNumber { get; set; }
        [Prompt("Please give me your e-mail address that you use for Tech Elevator communications.")]
        public string Email { get; set; }
        [Prompt("How do you prefer to be contacted?")]
        public ContactMethod PreferredContactMethod { get; set; }
        public MainMenuOptions Options { get; set; }
        public object ContextConstants { get; private set; }

        public static IForm<UserInfo> BuildForm()
        {
            OnCompletionAsyncDelegate<UserInfo> nextConversation = async (context, state) =>
            {
                await context.PostAsync($"Thanks {context}! ");
            };

            return new FormBuilder<UserInfo>()
          .Message("I'd love for this message to go get our greeting code")
          .Build();
        }

   
        // code copied from github State API Bot sample
        //private async Task ResumeAfterPrompt(IDialogContext context, IAwaitable<string> result)
        //{
        //    try
        //    {
        //        var userName = await result;
        //        this.Name = userName;

        //        await context.PostAsync($"Welcome {userName}!");

        //        context.UserData.SetValue(string UserNameKey, UserInfo. );
        //    }
        //    catch (TooManyAttemptsException)
        //    {
        //    }

        //    context.Wait(this.MessageReceivedAsync);
        //}

        //internal static IDialog<UserInfo> RootDialog()
        //{
        //   return Chain.From(() => FormDialog.FromForm(UserInfo.BuildForm))
        //        .Do(async (context, formResult) =>
        //        {
        //            var completed = await formResult;
        //            //completed should have form data
        //        })


        //}


    }
}


  