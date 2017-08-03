using StudentChatBot.Models.Matchmaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentChatBot.Models
{
    public class StudentMatchmakingSchedule
    {
        public string StudentName { get; set; }
        public List<ScheduleItem> AllInterviewsOnDayOne { get; set; }
        public List<ScheduleItem> AllInterviewsOnDayTwo { get; set; }

        public override string ToString()
        {
            string schedule = "";

            if (AllInterviewsOnDayOne != null)
            {
                schedule += "\n\n--\n\n";
                schedule += "\n\n\t\n\nDay One\n\n";

                foreach (ScheduleItem item in AllInterviewsOnDayOne)
                {
                    schedule += $"{item.StartTime} - {item.EndTime} -- {item.CompanyName}\n\n";
                }
            }
            else
            {
                schedule += "\nNothing schedule on Day One\n";
            }
            
            if (AllInterviewsOnDayTwo != null)
            {
                schedule += "\n\n--\n\n";
                schedule += "\n\nDay Two\n\n";
                foreach (ScheduleItem item in AllInterviewsOnDayTwo)
                {
                    schedule += $"{item.StartTime} - {item.EndTime} -- {item.CompanyName}\n\n";
                }
            }
            else
            {
                schedule += "\nNothing scheduled on Day Two\n";
            }
            
            return schedule;
        }
    }
}