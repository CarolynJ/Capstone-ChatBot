﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Web.Http.Description;
using System.Diagnostics;
using Microsoft.Bot.Builder.FormFlow;

namespace StudentChatBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        internal static IDialog<UserInfo> RootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(UserInfo.BuildForm));
        }
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity !=null)
            {
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, RootDialog);
                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }

            return null;

            }

        }


        ///// <summary>
        ///// POST: api/Messages
        ///// Receive a message from a user and reply to it
        ///// </summary>
        //public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        //{
        //    // checking if the user's message is indeed a message (as opposed to a command to delete, update, etc a record)
        //    if (activity.Type == ActivityTypes.Message)
        //    {
        //        // a new message triggers the root dialog, let's go there next...
        //        await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());    
        //    }
        //    else
        //    {
        //        // handle delete, update, etc for a record
        //        HandleSystemMessage(activity);
        //    }
            
        //    // this handles the status code in the bottom right corner of the chat emulator
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    return response;
        // }

        //private Activity HandleSystemMessage(Activity message)
        //{
        //    if (message.Type == ActivityTypes.DeleteUserData)
        //    {
        //        // Implement user deletion here
        //        // If we handle user deletion, return a real message
        //    }
        //    else if (message.Type == ActivityTypes.ConversationUpdate)
        //    {
        //        // Handle conversation state changes, like members being added and removed
        //        // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
        //        // Not available in all channels
        //    }
        //    else if (message.Type == ActivityTypes.ContactRelationUpdate)
        //    {
        //        // Handle add/remove from contact lists
        //        // Activity.From + Activity.Action represent what happened
        //    }
        //    else if (message.Type == ActivityTypes.Typing)
        //    {
        //        // Handle knowing tha the user is typing
        //    }
        //    else if (message.Type == ActivityTypes.Ping)
        //    {
        //    }

        //    return null;
        //}
    }
