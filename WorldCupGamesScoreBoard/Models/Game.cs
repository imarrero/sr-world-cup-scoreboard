using System;

namespace WorldCupGamesScoreBoard.Models
{
    /// <summary>
    /// FOOTBALL WORLD CUP MANAGER GAME MODEL
    /// </summary>
    public class Game
    {
        public Game()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }                
        public string MatchName { get; set; }       // Unique Name / key of the game
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public DateTime StartDate { get; set; }
    }
}