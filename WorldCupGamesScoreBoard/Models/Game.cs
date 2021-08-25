using System;

namespace WorldCupGamesScoreBoard.Models
{
    public class Game
    {
        public Guid Id { get; set; }                
        public string MatchName { get; set; }       // Unique Name / key of the game
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public DateTime StartDate { get; set; }
    }
}