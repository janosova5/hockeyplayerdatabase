using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.Model
{
    public class HockeyContext: DbContext, IHockeyReport<Club, Player>
    {
        public HockeyContext() : base("name=HockeyPlayerDatabase")
        {
            
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }

        public List<Player> FilteredPlayers { get; set; }
         

        public void InitializeFilteredPlayers()
        {
            FilteredPlayers = Players.ToList();
        }

        public bool CheckKrpId(int krpId)
        {
            foreach (var player in Players)
            {
                if (player.KrpId == krpId)
                {
                    return true;
                }
            }
            return false;
        }

        public Player GetPlayerById(int id)
        {
            foreach (var player in Players)
            {
                if (player.Id == id) return player;
            }
            return null;
        }

        public void FilterPlayers(PlayerSearchModel searchModel)
        {
            var result = Players.Select(players => players);
            if (searchModel.Krp.HasValue)
            {
                result = result.Where(player => player.KrpId == searchModel.Krp);
            }
            if (!string.IsNullOrEmpty(searchModel.FirstName))
            {
                result = result.Where(player => player.FirstName.Contains(searchModel.FirstName));
            }
            if (!string.IsNullOrEmpty(searchModel.LastName))
            {
                result = result.Where(player => player.LastName.Contains(searchModel.LastName));
            }
            if (searchModel.YearFrom.HasValue)
            {
                result = result.Where(player => player.YearOfBirth >= searchModel.YearFrom);
            }
            if (searchModel.YearTo.HasValue)
            {
                result = result.Where(player => player.YearOfBirth <= searchModel.YearTo);
            }
            if (searchModel.AgeCategory.HasValue)
            {
                result = result.Where(player => player.AgeCategory == searchModel.AgeCategory);
            }
            if (!string.IsNullOrEmpty(searchModel.Club))
            {
                result = result.Where(player => player.ClubName.Contains(searchModel.Club));
            }
            FilteredPlayers = result.ToList();

        }

        public IQueryable<Club> GetClubs()
        {
            var result = Clubs.Select(clubs => clubs);
            Console.WriteLine($@"GetClubs: ({result.Count()} results)");
            foreach (var club in result)
            {
                Console.WriteLine(club.ToString());
            }
            return result;
        }

        public IQueryable<Player> GetPlayers()
        {
            var result = Players.Select(players => players);

            Console.WriteLine($@"GetPlayers: ({result.Count()} results)");
            foreach (var player in result)
            {
                Console.WriteLine(player.ToString());
            }
            return result;
        }

        public IEnumerable<Club> GetSortedClubs(int maxResultCount)
        {
            var result = Clubs.OrderBy(clubs => clubs.Name).Take(maxResultCount).Select(clubs => clubs);
            Console.WriteLine($@"GetClubs: ({result.Count()} results)");
            foreach (var club in result)
            {
                Console.WriteLine(club.ToString());
            }
            return result;
        }

        public IEnumerable<Player> GetSortedPlayers(int maxResultCount)
        {
            var result = Players.OrderBy(players => players.LastName).Take(maxResultCount).Select(players => players);
            Console.WriteLine($@"GetPlayers: ({result.Count()} results)");
            foreach (var player in result)
            {
                Console.WriteLine(player.ToString());
            }
            return result;
        }

        public double GetPlayerAverageAge()
        {

            var result = Players.Select(player => DateTime.Now.Year - player.YearOfBirth).Average();
            Console.WriteLine(result);
            return result;
        }

        public Player GetYoungestPlayer()
        {
            var youngestPlayer = Players.Select(player => player).OrderByDescending(player => player.YearOfBirth).First();
            Console.WriteLine(youngestPlayer.ToString());
            return youngestPlayer;
        }

        public Player GetOldestPlayer()
        {
            var youngestPlayer = Players.Select(player => player).OrderBy(player => player.YearOfBirth).First();
            Console.WriteLine(youngestPlayer.ToString());
            return youngestPlayer;
        }

        public IEnumerable<int> GetClubPlayerAges()
        {
            var ages = Players.Select(player => DateTime.Now.Year - player.YearOfBirth).Distinct();
            Console.WriteLine($@"GetClubPlayerAges: ({ages.Count()} results)");
            foreach (var age in ages)
            {
                Console.WriteLine(age);
            }
            return ages;
        }

        public IEnumerable<Player> GetPlayersByAge(int minAge, int maxAge)
        {
            var players = Players.Select(player => player).Where(player =>
                DateTime.Now.Year - player.YearOfBirth >= minAge &&
                DateTime.Now.Year - player.YearOfBirth <= maxAge);
            Console.WriteLine($@"GetPlayersByAge: ({minAge}, {maxAge}): ({players.Count()} results)");
            foreach (var player in players)
            {
                Console.WriteLine(player.ToString());
            }
            return players;

        }

        public ReportResult GetReportByClub(int clubId)
        {
            var players = Players.Select(player => player).Where(player => player.ClubId == clubId);
            var youngestPlayer = players.Select(player => player)
                .OrderByDescending(player => player.YearOfBirth).First();
            var oldestPlayer = players.Select(player => player).OrderBy(player => player.YearOfBirth).First();
            int totalPlayerCount = players.Count();
            double averagePlayerAge = players.Select(player => DateTime.Now.Year - player.YearOfBirth).Average();
            string youngestPlayerFullName = youngestPlayer.FullName;
            string oldestPlayerFullName = oldestPlayer.FullName;
            int youngestPlayerAge = DateTime.Now.Year - youngestPlayer.YearOfBirth;
            int oldestPlayerAge = DateTime.Now.Year - oldestPlayer.YearOfBirth;
            return new ReportResult(totalPlayerCount, averagePlayerAge, youngestPlayerFullName, oldestPlayerFullName,
                youngestPlayerAge, oldestPlayerAge);
        }

        public IDictionary<AgeCategory, ReportResult> GetReportByAgeCategory()
        {
            var cadetReport = GetReportByCategory(AgeCategory.Cadet);
            var juniorReport = GetReportByCategory(AgeCategory.Junior);
            var midgestReport = GetReportByCategory(AgeCategory.Midgest);
            var seniorReport = GetReportByCategory(AgeCategory.Senior);
            var dictionary = new Dictionary<AgeCategory, ReportResult>
            {
                { AgeCategory.Cadet, cadetReport },
                { AgeCategory.Junior, juniorReport },
                { AgeCategory.Midgest, midgestReport },
                { AgeCategory.Senior, seniorReport }
            };
            return dictionary;
        }

        //SOURCE - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-write-object-data-to-an-xml-file
        public void SaveToXml(string fileName)
        {
            FileStream file = File.Create(fileName);

            XmlSerializer writerClub = new XmlSerializer(typeof(Club));
            foreach (var club in Clubs)
            {
                writerClub.Serialize(file, club);
            }

            XmlSerializer writerPlayer = new XmlSerializer(typeof(Player));
            foreach (var player in Players)
            {
                writerPlayer.Serialize(file, player);
            }

            file.Close();
        }

        private ReportResult GetReportByCategory(AgeCategory category)
        {
            var players = Players.Select(player => player).Where(player => player.AgeCategory == category);
            var youngestPlayer = players.Select(player => player)
                .OrderByDescending(player => player.YearOfBirth).First();
            var oldestPlayer = players.Select(player => player).OrderBy(player => player.YearOfBirth).First();
            int totalPlayerCount = players.Count();
            double averagePlayerAge = players.Select(player => DateTime.Now.Year - player.YearOfBirth).Average();
            string youngestPlayerFullName = youngestPlayer.FullName;
            string oldestPlayerFullName = oldestPlayer.FullName;
            int youngestPlayerAge = DateTime.Now.Year - youngestPlayer.YearOfBirth;
            int oldestPlayerAge = DateTime.Now.Year - oldestPlayer.YearOfBirth;
            return new ReportResult(totalPlayerCount, averagePlayerAge, youngestPlayerFullName, oldestPlayerFullName,
                youngestPlayerAge, oldestPlayerAge);
        }



    }
}
