using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.Model
{
    public class Player: IPlayer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string TitleBefore { get; set; }
        public int YearOfBirth { get; set; }
        public int KrpId { get; set; }
        public AgeCategory? AgeCategory { get; set; }
        public int? ClubId { get; set; }

        public string ClubName { get; set; }

        public override string ToString()
        {
            return $"{KrpId}. {FullName} ({YearOfBirth}, {AgeCategory.ToString()}), club: {ClubName}";
        }
    }
}
