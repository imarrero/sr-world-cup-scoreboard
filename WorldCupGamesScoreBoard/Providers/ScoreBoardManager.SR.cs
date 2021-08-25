using System;
using System.Collections.Generic;
using System.Linq;
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
            var matchName = $"{HomeTeamName?.Trim()}-{AwayTeamName?.Trim()}".ToUpper();

            RunStartGameValidations(HomeTeamName, AwayTeamName, matchName);

            var game = new Game()
            {
                Id = new Guid(),
                MatchName = matchName,
                Scoring = "0-0",
                StartDate = DateTime.Now.ToUniversalTime()
            };

            _currentGames.Add(matchName, game);

            return game;
        }


        public bool FinishGame(string MatchName)
        {
            var matchName = MatchName?.ToUpper();

            RunFinishGameValidations(matchName);

            return _currentGames.Remove(matchName);
        }

        public Game UpdateGame(string MatchName, string PairScore)
        {
            return new Game();
        }

        public Game UpdateGame(string MatchName, int HomeScore, int AwayScore)
        {
            return new Game();
        }

        public List<Game> GetSummary()
        {
            return _currentGames.Values.ToList();
        }

        #region region private functions

        private void RunStartGameValidations(string HomeTeamName, string AwayTeamName, string matchName)
        {
            // TODO TECH DEBT: Move this to FluentValidation Library or similar centralized validations
            if (string.IsNullOrWhiteSpace(HomeTeamName))
                throw new ArgumentException("ERROR CODE: XXX - Requested HomeTeamName is not valid name");

            if (string.IsNullOrWhiteSpace(AwayTeamName))
                throw new ArgumentException("ERROR CODE: XXX - Requested HomeTeamName is not valid name");

            if (_currentGames.ContainsKey(matchName))
                throw new ArgumentException("ERROR CODE: XXX - Requested Game is already on going: Cannot be recreated");
        }

        private void RunFinishGameValidations(string matchName)
        {
            // TODO TECH DEBT: Move this to FluentValidation Library or similar centralized validations
            if (string.IsNullOrWhiteSpace(matchName))
                throw new ArgumentException("ERROR CODE: XXX - Requested MatchName is not valid");

            if (!_currentGames.ContainsKey(matchName))
                throw new ArgumentException("ERROR CODE: XXX - Requested Game was not found in current ongoing matches: Cannot be Finished");
        }

        #endregion
    }
}
