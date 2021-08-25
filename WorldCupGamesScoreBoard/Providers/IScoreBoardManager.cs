using System;
using System.Collections.Generic;
using WorldCupGamesScoreBoard.Models;


namespace WorldCupGamesScoreBoard.Providers
{
    public interface IScoreBoardManager
    {
        Game StartGame(string HomeTeamName, string AwayTeamName);
        bool FinishGame(string MatchName);
        Game UpdateGame(string MatchName, string PairScore);
        Game UpdateGame(string MatchName, int HomeScore, int AwayScore);
        List<Game> GetSummary();
    }

}