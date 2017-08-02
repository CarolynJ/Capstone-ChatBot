using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentChatBot.Models;
using StudentChatBot.Models.Matchmaking;
using System.IO;

namespace StudentChatBot.DAL
{
    public class MatchmakingSQLDAL : IMatchmakingDAL
    {
        private enum DaysOfTheWeek { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        public CompanyContact GetCompanyContactInfo(string companyName)
        {
            throw new NotImplementedException();
        }

        public StudentMatchmakingSchedule GetStudentSchedule(string studentName)
        {
            string fileName = "Content/Summer2017MatchmakingScheduleByStudent.tsv";
            List<string[]> allLinesFromFile = this.ReadFromFile(fileName);

            int indexOfStudentNameInFile = Array.FindIndex(allLinesFromFile[0], x => x.Contains(studentName));

            List<string[]> onlyScheduleLines = new List<string[]>(allLinesFromFile); // make a deep copy w/ different references to memory
            onlyScheduleLines.RemoveAt(0); // remove top line of names
            onlyScheduleLines.RemoveAt(onlyScheduleLines.Count - 1); // remove bottom line with interview count

            StudentMatchmakingSchedule studentSchedule = new StudentMatchmakingSchedule();
            studentSchedule.StudentName = studentName;
            
            List<ScheduleItem> allScheduleItems = new List<ScheduleItem>();
            List<string[]> allStudentScheduleStrings = new List<string[]>();

            foreach (string[] str in onlyScheduleLines)
            {
                allStudentScheduleStrings.Add(new string[] { str[0], str[1], str[indexOfStudentNameInFile] });
            }

            int indexOfStartOfSecondDay = allStudentScheduleStrings.FindIndex(1, x => x[0] != "");

            List<string[]> firstHalfOfSchedule = new List<string[]>(allStudentScheduleStrings.GetRange(0, indexOfStartOfSecondDay));
            List<string[]> secondHalfOfSchedule = new List<string[]>(allStudentScheduleStrings.GetRange(indexOfStartOfSecondDay, allStudentScheduleStrings.Count - firstHalfOfSchedule.Count));
            
            foreach (string[] str in firstHalfOfSchedule)
            {
                studentSchedule.AllInterviewsOnDayOne.Add(new ScheduleItem()
                {
                    StartTime = str[1].Substring(0, indexOfStartOfSecondDay),
                    EndTime = str[1].Substring(indexOfStartOfSecondDay + 1),
                    CompanyName = str[indexOfStudentNameInFile]
                });
            }

            foreach (string[] str in secondHalfOfSchedule)
            {
                studentSchedule.AllInterviewsOnDayTwo.Add(new ScheduleItem()
                {
                    StartTime = str[1].Substring(0, indexOfStartOfSecondDay),
                    EndTime = str[1].Substring(indexOfStartOfSecondDay + 1),
                    CompanyName = str[indexOfStudentNameInFile]
                });
            }
            
            return studentSchedule;
        }

        private List<string[]> ReadFromFile(string fileName)
        {
            string directory = Environment.CurrentDirectory;
            string fullPath = Path.Combine(directory, fileName);

            List<string[]> allWords = new List<string[]>();

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] words = sr.ReadLine().Split(',');
                        allWords.Add(words);
                    }
                }
            }
            catch
            {
                throw;
            }

            return allWords;
        }
    }
}