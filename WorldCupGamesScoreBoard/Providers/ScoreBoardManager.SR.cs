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
            var parsedMatchName = $"{HomeTeamName?.Trim()}-{AwayTeamName?.Trim()}".ToUpper();

            RunStartGameValidations(HomeTeamName, AwayTeamName, parsedMatchName);

            var game = new Game()
            {
                Id = new Guid(),
                MatchName = parsedMatchName,
                Scoring = "0-0",
                StartDate = DateTime.Now.ToUniversalTime()
            };

            _currentGames.Add(parsedMatchName, game);

            return game;
        }


        public bool FinishGame(string MatchName)
        {
            var parsedMatchName = MatchName?.ToUpper();

            RunFinishGameValidations(parsedMatchName);

            return _currentGames.Remove(parsedMatchName);
        }

        public Game UpdateGame(string MatchName, string PairScore)
        {
            (int HomeScore, int AwayScore) scoring;
            scoring = ParseScoring(PairScore);

            var parsedMatchName = MatchName?.ToUpper();

            return UpdateGame(parsedMatchName, scoring.HomeScore, scoring.AwayScore);
        }

        public Game UpdateGame(string MatchName, int HomeScore, int AwayScore)
        {
            var parsedMatchName = MatchName?.ToUpper();

            RunUpdateGameValidations(parsedMatchName, HomeScore, AwayScore);

            _currentGames[parsedMatchName].Scoring = $"{HomeScore}-{AwayScore}";

            return new Game();
        }

        public List<Game> GetSummary()
        {
            return _currentGames.Values.ToList();
        }

        #region region private functions

        private (int HomeScore, int AwayScore) ParseScoring(string pairScore)
        {
            try
            {
                return (Convert.ToInt32(pairScore.Split('-')[0]), Convert.ToInt32(pairScore.Split('-')[1]));
            }
            catch (Exception)
            {
                throw new ArgumentException("ERROR CODE: XXX - requested PairScore has not the correct format '{number}-{number}'");
            }

        }

        private void RunUpdateGameValidations(string matchName, int homeScore, int awayScore)
        {
            // TODO TECH DEBT: Move this to FluentValidation Library or similar centralized validations
            if (string.IsNullOrWhiteSpace(matchName))
                throw new ArgumentException("ERROR CODE: XXX - Requested MatchName is not valid name");

            if (homeScore < 0 || awayScore < 0)
                throw new ArgumentException("ERROR CODE: XXX - Negative Scores are not allowed");

            if (!_currentGames.ContainsKey(matchName))
                throw new ArgumentException("ERROR CODE: XXX - Requested Game was not found in current ongoing matches: Cannot be Finished");
        }

        private void RunStartGameValidations(string homeTeamName, string awayTeamName, string matchName)
        {
            // TODO TECH DEBT: Move this to FluentValidation Library or similar centralized validations
            if (string.IsNullOrWhiteSpace(homeTeamName))
                throw new ArgumentException("ERROR CODE: XXX - Requested HomeTeamName is not valid name");

            if (string.IsNullOrWhiteSpace(awayTeamName))
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
