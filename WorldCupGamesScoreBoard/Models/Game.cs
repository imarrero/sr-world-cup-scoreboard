using System;

namespace WorldCupGamesScoreBoard.Models
{
    public class Game
    {
        public Guid Id { get; set; }                // internal
        public String MatchName { get; set; }       // Unique Name / key of the game
        public String Scoring { get; set; }         // Scoring label
        public DateTime StartDate { get; set; }
    }  
}
