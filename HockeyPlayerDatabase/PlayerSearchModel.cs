using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase
{
    public class PlayerSearchModel
    {

        public int? Krp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public AgeCategory? AgeCategory { get; set; }
        public string Club { get; set; }
    }
}
