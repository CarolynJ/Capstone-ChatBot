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

            int indexOfStudentNameInFile = Array.FindIndex(allLinesFromFile[0], x => x.ToLower().Contains(studentName.ToLower()));

            if (indexOfStudentNameInFile == -1) // doesn't contain the name they're looking for
            {
                return new StudentMatchmakingSchedule(); // return empty which will be caught in MatchmakingDialog
            }

            List<string[]> onlyScheduleLines = new List<string[]>(allLinesFromFile); // make a deep copy w/ different references to memory
            onlyScheduleLines.RemoveAt(0); // remove top line of names
            onlyScheduleLines.RemoveAt(onlyScheduleLines.Count - 1); // remove bottom line with interview count
            onlyScheduleLines.RemoveAt(onlyScheduleLines.Count - 1); // remove another empty bottom line

            StudentMatchmakingSchedule studentSchedule = new StudentMatchmakingSchedule();
            studentSchedule.StudentName = allLinesFromFile[0][indexOfStudentNameInFile];
            
            List<ScheduleItem> allScheduleItems = new List<ScheduleItem>();
            List<string[]> allStudentScheduleStrings = new List<string[]>();

            foreach (string[] str in onlyScheduleLines)
            {
                allStudentScheduleStrings.Add(new string[] { str[0], str[1], str[indexOfStudentNameInFile] });
            }

            int indexOfStartOfSecondDay = allStudentScheduleStrings.FindIndex(1, x => x[0] != "");

            List<string[]> firstHalfOfSchedule = new List<string[]>(allStudentScheduleStrings.GetRange(0, indexOfStartOfSecondDay));
            List<string[]> secondHalfOfSchedule = new List<string[]>(allStudentScheduleStrings.GetRange(indexOfStartOfSecondDay, allStudentScheduleStrings.Count - firstHalfOfSchedule.Count));
            List<ScheduleItem> firstDayScheduleItems = new List<ScheduleItem>();
            List<ScheduleItem> secondDayScheduleItems = new List<ScheduleItem>();

            foreach (string[] str in firstHalfOfSchedule)
            {
                if (str[2] != "")
                {
                    firstDayScheduleItems.Add(new ScheduleItem()
                    {
                        StartTime = str[1].Substring(0, 5),
                        EndTime = str[1].Substring(7),
                        CompanyName = str[2]
                    });
                }
            }

            foreach (string[] str in secondHalfOfSchedule)
            {
                if (str[2] != "")
                {
                    secondDayScheduleItems.Add(new ScheduleItem()
                    {
                        StartTime = str[1].Substring(0, 5),
                        EndTime = str[1].Substring(7),
                        CompanyName = str[2]
                    });
                }
            }

            studentSchedule.AllInterviewsOnDayOne = firstDayScheduleItems;
            studentSchedule.AllInterviewsOnDayTwo = secondDayScheduleItems;

            return studentSchedule;
        }

        private List<string[]> ReadFromFile(string fileName)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(directory, fileName);

            List<string[]> allWords = new List<string[]>();

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] words = sr.ReadLine().Split('\t');
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