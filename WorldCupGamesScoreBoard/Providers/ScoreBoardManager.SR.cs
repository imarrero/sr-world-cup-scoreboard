using System;
using System.Collections.Generic;
using WorldCupGamesScoreBoard.Models;

namespace WorldCupGamesScoreBoard.Providers
{
    public class ScoreBoardManagerSR : IScoreBoardManager
    {
        private Dictionary<string, Game> _currentGames { get; set; }

        public ScoreBoardManagerSR()
        {
            _currentGames = new Dictionary<string, Game>();
        }

        public Game StartGame(string HomeTeamName, string AwayTeamName)
        {
            return new Game();
        }

        public bool FinishGame(string MatchName)
        {
            return true;
        }

        public Game UpdateGame(string MatchName, string PairScore)
        {
            return new Game();
        }

        public Game UpdateGame(string MatchName, int HomeScore, int AwayScore)
        {
            return new Game();
        }

        public Game GetSummary(string HomeTeamName, string AwayTeamName)
        {
            return new Game();
        }
    }
}
