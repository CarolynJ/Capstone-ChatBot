using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using StudentChatBot;
using System.Security.AccessControl;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow.Advanced;

namespace StudentChatBot
{
    //public enum MainMenuOptions { PathwayResources, TechnicalResources, MotivationalQuotes };
    public enum SearchTerms { CSharp, Git, Line, Bots, Frameworks };
    public enum TimeOfDay { Morning, afternoon, evening, night };
    public enum BotPersonality { Robotic, Sassy, KeepMeGuessing };
    public enum KindOfResource { Book, video, website };
    public enum SubMenu { Interview, Letter, Resume, LinkedIn };
}

[Serializable]
[Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
public class MainMenu
{
    [Prompt("Which of these {&} items do you need help with? {||}", ChoiceFormat = "{0}")]
    public MainMenuOptions? Menu;
    public SearchTerms? KeyWord;
    public TimeOfDay? TimeOfDay;
    public BotPersonality? PersonalityForBot;
    public KindOfResource? Media;
    public SubMenu? PathwayTopics;
    public List<SearchTerms> Search;

    public static IForm<UserInfo> BuildForm()
    {
        return new FormBuilder<UserInfo>()
      .Message("Welcome to the Tech elevator Student Bot. I am here to help you learn.")
         .Build();
    }


    OnCompletionAsyncDelegate<UserInfo> nextConversation = async (context, state) =>
    {
        await context.PostAsync("All done");
    };


}

        //return new FormBuilder<MainMenu>()


  
    //.Field(nameof(KeyWord))
    //.Field(nameof(TimeOfDay))
    //.Field(nameof(PersonalityForBot))
    //.Field(nameof(Media))
    //.Field(new FieldReflector<MainMenu>(nameof(SubMenu))
    //    .SetType(null)
    //    .SetActive((state) => state.Menu == MainMenuOptions.PathwayResources)
    //    .SetDefine(async (state, field) =>
    //    {
    //field
    //.AddDescription("letters")
    //.AddTerms("letter", "letters");



