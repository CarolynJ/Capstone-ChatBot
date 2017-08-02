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
    }
}