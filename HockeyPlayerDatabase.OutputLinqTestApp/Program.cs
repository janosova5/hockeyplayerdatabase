using System;
using System.Collections.Generic;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.OutputLinqTestApp
{
    class Program
    {
        static void Main()
        {
            
            using (var db = new HockeyContext())
            {
                int clubId = 0;
                ReportResult report = null;
                //db.GetClubs();
                /* db.GetPlayers();
                 db.GetSortedClubs(5);
                 db.GetSortedPlayers(10);
                 db.GetPlayerAverageAge();
                 db.GetYoungestPlayer();
                 db.GetOldestPlayer();
                 db.GetClubPlayerAges();
                 db.GetPlayersByAge(33, 35);
                 */
                clubId = 1;
                report = db.GetReportByClub(1);
                WriteReportByClub(report, clubId);
                clubId = 2;
                report = db.GetReportByClub(2);
                WriteReportByClub(report, clubId);
                var dictionary = db.GetReportByAgeCategory();
                WriteReportByAgeCategory(dictionary);
                db.SaveToXml("Data.xml");
            }
        }

        static void WriteReportByClub(ReportResult report, int clubId)
        {
            Console.WriteLine($"GetReportByClub({clubId})");
            WriteReport(report);
        }

        static void WriteReportByAgeCategory(IDictionary<AgeCategory, ReportResult> dictionary)
        {
            Console.WriteLine($"GetReportByAgeCategory({dictionary.Count} results)");
            Console.WriteLine($"{AgeCategory.Cadet}:");
            WriteReport(dictionary[AgeCategory.Cadet]);
            Console.WriteLine($"{AgeCategory.Midgest}:");
            WriteReport(dictionary[AgeCategory.Midgest]);
            Console.WriteLine($"{AgeCategory.Junior}:");
            WriteReport(dictionary[AgeCategory.Junior]);
            Console.WriteLine($"{AgeCategory.Senior}:");
            WriteReport(dictionary[AgeCategory.Senior]);
        }

        static void WriteReport(ReportResult report)
        {
            Console.WriteLine($"TotalPlayerCount: {report.TotalPlayerCount}, AveragePlayerAge: " +
                              $"{report.AveragePlayerAge}, YoungestPlayerFullName: " +
                              $"{report.YoungestPlayerFullName}, OldestPlayerFullName: " +
                              $"{report.OldestPlayerFullName}, YoungestPlayerAge:" +
                              $" {report.YoungestPlayerAge}, OldestPlayerAge: {report.OldestPlayerAge}");
        }
    }
}
