using StudentChatBot.Models;
using StudentChatBot.Models.Matchmaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentChatBot.DAL
{
    public interface IMatchmakingDAL
    {
        StudentMatchmakingSchedule GetStudentSchedule(string studentName);
        CompanyContact GetCompanyContactInfo(string companyName);
    }
}