using System;

namespace WorldCupGamesScoreBoard.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public String MatchName { get; set; }
        public String Scoring { get; set; }
        public DateTime StartDate { get; set; }
    }  
}
