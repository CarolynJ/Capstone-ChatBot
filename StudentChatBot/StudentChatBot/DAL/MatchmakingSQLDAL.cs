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
        private const string companiesTSVFileName = "Content/Summer2017MatchmakingCompanies.tsv";
        private const string matchmakingTSVFileName = "Content/Summer2017MatchmakingScheduleByStudent.tsv";
        
        public CompanyContact GetCompanyContactInfo(string companyName)
        {
            List<string[]> allLinesFromFile = this.ReadFromFile(companiesTSVFileName);

            int indexOfCompanyInFile = Array.FindIndex(allLinesFromFile[0], x => x.ToLower().Contains(companyName.ToLower()));

            if (indexOfCompanyInFile == -1)
            {
                return new CompanyContact(); // return empty which will be caught in MatchmakingDialog
            }

            // get list<string> of interviewers and add to companycontact
            List<string> companyInterviewersAndMainContact = new List<string>();

            if (allLinesFromFile[1][indexOfCompanyInFile].Length > 0) // only add interviewers to companyContact if there's something in the field (not empty)
            {
                string[] arrayOfInterviewers = allLinesFromFile[1][indexOfCompanyInFile].Split(',');
                companyInterviewersAndMainContact.Add(String.Join(", ", arrayOfInterviewers));
            }

            companyInterviewersAndMainContact.Add(allLinesFromFile[2][indexOfCompanyInFile]);

            Dictionary<string, string> mainContactInfo = new Dictionary<string, string>();
            mainContactInfo.Add(allLinesFromFile[2][indexOfCompanyInFile], allLinesFromFile[3][indexOfCompanyInFile]);
            
            // populate the companycontact object to return
            CompanyContact companyContact = new CompanyContact()
            {
                CompanyName = allLinesFromFile[0][indexOfCompanyInFile],
                Interviewers = companyInterviewersAndMainContact,
                MainContactInfo = mainContactInfo
            };

            return companyContact;
        }

        public StudentMatchmakingSchedule GetStudentSchedule(string studentName)
        {
            List<string[]> allLinesFromFile = this.ReadFromFile(matchmakingTSVFileName);

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

        public List<string> GetListOfAllAttendingCompanies()
        {
            List<string[]> allLinesFromFile = this.ReadFromFile(companiesTSVFileName);

            List<string> allCompanyNames = allLinesFromFile[0].ToList();
            allCompanyNames.RemoveAt(0); // remove "company name" from list
            allCompanyNames.Sort();

            return allCompanyNames;
        }
    }
}