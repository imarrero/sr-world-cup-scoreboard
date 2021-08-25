using System.Collections.Generic;
using WorldCupGamesScoreBoard.Models;

namespace WorldCupGamesScoreBoard.Providers
{
    /// <summary>
    /// Interface for all games that support:
    /// - Two teams + scoring as integer numbers
    /// </summary>
    public interface IScoreBoardManager
    {
        /// <summary>
        /// Starts a new game: Creates an entry in the Scoreboard system
        /// </summary>
        /// <param name="HomeTeamName"></param>
        /// <param name="AwayTeamName"></param>
        /// <returns></returns>
        Game StartGame(string HomeTeamName, string AwayTeamName);

        /// <summary>
        /// Remove an existing game: Deletes entry from the Scoreboard system
        /// </summary>
        /// <param name="MatchName">Match name in {HomeTeam}-{AwayTeam} format</param>
        /// <returns></returns>
        bool FinishGame(string MatchName);

        /// <summary>
        /// Update existing game Scoring: 
        /// </summary>
        /// <param name="MatchName">Match name in {HomeTeam}-{AwayTeam} format</param>
        /// <param name="PairScore">Must be in {number}-{number} format</param>
        /// <returns></returns>
        Game UpdateGame(string MatchName, string PairScore);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatchName">Match name in {HomeTeam}-{AwayTeam} format</param>
        /// <param name="HomeScore">Home Team score value: integer > 0</param>
        /// <param name="AwayScore">Away Team score value: integer > 0</param>
        /// <returns></returns>
        Game UpdateGame(string MatchName, int HomeScore, int AwayScore);

        /// <summary>
        /// Return a list of current ongoing games
        /// </summary>
        /// <returns></returns>
        List<Game> GetSummary();
    }

}