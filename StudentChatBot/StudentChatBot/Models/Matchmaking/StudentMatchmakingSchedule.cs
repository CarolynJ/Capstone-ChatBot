﻿using StudentChatBot.Models.Matchmaking;
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

            foreach (ScheduleItem item in AllInterviewsOnDayOne)
            {
                schedule += "Day One\n";
                schedule += $"{item.StartTime} - {item.EndTime} -- {item.CompanyName}\n\n";
            }

            foreach (ScheduleItem item in AllInterviewsOnDayTwo)
            {
                schedule += "Day Two\n";
                schedule += $"{item.StartTime} - {item.EndTime} -- {item.CompanyName}\n\n";
            }

            return schedule;
        }
    }
}