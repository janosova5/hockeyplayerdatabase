using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.ImportDataApp
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();
            var splitedInput = SplitRow(input, ' ');
            string clubsFilePath = "";
            string playersFilePath = "";
            bool clearDatabaseIsOn = false;
            for (int i = 0; i < splitedInput.Length; i++)
            {
                if (splitedInput[i] == "-clubs") clubsFilePath = splitedInput[i + 1];
                if (splitedInput[i] == "-players") playersFilePath = splitedInput[i + 1];
                if (splitedInput[i] == "-clearDatabase") clearDatabaseIsOn = true;
            }
            CreateDatabase(clubsFilePath, playersFilePath, clearDatabaseIsOn);

        }

        static void CreateDatabase(string clubsPath, string playersPath, bool clearDatabaseRequired)
        {
            using (var db = new HockeyContext())
            {
                if (clearDatabaseRequired)
                {
                    ClearDatabase(db);
                    Console.WriteLine("Database cleared");
                }
                db.SaveChanges();
                var clubs = ReadClubsFile(clubsPath);
                foreach (var club in clubs)
                {                   
                    db.Clubs.Add(club);
                }
                db.SaveChanges();
                var players = ReadPlayersFile(playersPath, db.Clubs);

                foreach (var player in players)
                {
                    db.Players.Add(player);
                }
                db.SaveChanges();
            }
        }

        static void ClearDatabase(HockeyContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM [Players]");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Players', RESEED, 0);");
            context.Database.ExecuteSqlCommand("DELETE FROM [Clubs]");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Clubs', RESEED, 0);");
        }

        static Club CreateClub(string row)
        {
            var sentences = SplitRow(row, ';');
            return new Club()
            {
                Name = sentences[0],
                Address = sentences[1],
                Url = sentences[2],
            };
        }

        static Player CreatePlayer(string row, DbSet<Club> clubs)
        {
            var sentences = SplitRow(row, ';');
            var category = ConvertStringToAgeCategory(sentences[6]);
            Console.WriteLine(category);
            var lastNameLowercased = sentences[0].ToLower();
            var lastName = FirstCharToUpper(lastNameLowercased);
            var club = sentences[5];
            var clubId = GetClubId(clubs, club);
            var year = Convert.ToInt32(sentences[3]);
            var krpId = Convert.ToInt32(sentences[4]);
            return new Player()
            {
                LastName = lastName,
                FirstName = sentences[1],
                TitleBefore = sentences[2],
                YearOfBirth = year,
                KrpId = krpId,
                AgeCategory = category,
                ClubId = clubId,
                ClubName = club
            };
        }


        static List<Club> ReadClubsFile(string filePath)
        {
            // Read sample data from CSV file
            using (CsvFileReader reader = new CsvFileReader(filePath))
            {
                CsvFileReader.CsvRow row = new CsvFileReader.CsvRow();
                var clubs = new List<Club>();
                int index = 0;
                while (reader.ReadRow(row))
                {
                    index++;
                    if (index > 1)
                    {
                        var club = CreateClub(row.LineText);
                        clubs.Add(club);
                    }
                }
                return clubs;
            }
        }

        static List<Player> ReadPlayersFile(string filePath, DbSet<Club> clubs)
        {
            // Read sample data from CSV file
            using (CsvFileReader reader = new CsvFileReader(filePath))
            {
                CsvFileReader.CsvRow row = new CsvFileReader.CsvRow();
                var players = new List<Player>();
                int index = 0;
                while (reader.ReadRow(row))
                {
                    index++;
                    if (index > 1)
                    {
                        var player = CreatePlayer(row.LineText, clubs);
                        players.Add(player);
                    }
                }
                return players;
            }
        }


        static string[] SplitRow(string row, char separatorChar)
        {
            string[] result;
            char[] separator = {separatorChar,separatorChar};
            result = row.Split(separator, StringSplitOptions.None);
            return result;

        }
        

        static AgeCategory? ConvertStringToAgeCategory(string category)
        {
            switch (category)
            {
                case "Juniori":
                    return AgeCategory.Junior;
                case "Seniori":
                    return AgeCategory.Senior;
                case "Dorastenci":
                    return AgeCategory.Midgest;
                case "Kadeti":
                    return AgeCategory.Cadet;
                default: return null;
            }
        }

        static int GetClubId(DbSet<Club> clubs, string name)
        {
            foreach (var club in clubs)
            {
                if (club.Name == name)
                {
                    return club.Id;
                }
            }

            return 0;
        }

        //SOURCE - https://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-with-maximum-performance
        static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

    }
}