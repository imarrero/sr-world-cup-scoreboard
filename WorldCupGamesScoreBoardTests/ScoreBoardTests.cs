using NUnit.Framework;
using System;
using WorldCupGamesScoreBoard.Models;
using WorldCupGamesScoreBoard.Providers;

namespace WorldCupGamesScoreBoardTests
{
    public class Tests
    {
        private IScoreBoardManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new ScoreBoardManagerSR();
        }

        #region StartGames

        [Test]
        public void StartFiveGames()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");
            manager.StartGame("Uruguay", "Italy");
            manager.StartGame("Argentina", "Australia");

            var games = manager.GetSummary();

            Assert.IsTrue(games.Count == 5);
        }

        [Test]
        public void UNSUCCESS_StartGames_DuplicatedMatch()
        {
            manager.StartGame("Germany", "France");
            Assert.Throws<ArgumentException>(() => manager.StartGame("Germany", "France"));
        }

        [Test]
        public void UNSUCCESS_StartGames_EmptyHomeTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame("", "Brazil"));
        }

        [Test]
        public void UNSUCCESS_StartGames_EmptyAwayTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame("Spain", ""));
        }

        [Test]
        public void UNSUCCESS_StartGames_NullHomeTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame(null, "Brazil"));
        }

        [Test]
        public void UNSUCCESS_StartGames_nullAwayTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame("Spain", null));
        }

        #endregion

        #region FinishGames

        [Test]
        public void FinishFiveGames()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");
            manager.StartGame("Uruguay", "Italy");
            manager.StartGame("Argentina", "Australia");

            Assert.IsTrue(manager.FinishGame("Spain-Brazil"));
            Assert.IsTrue(manager.FinishGame("Mexico-Canada"));
            Assert.IsTrue(manager.FinishGame("Uruguay-Italy"));
            Assert.IsTrue(manager.FinishGame("Germany-France"));
            Assert.IsTrue(manager.FinishGame("Argentina-Australia"));

            var games = manager.GetSummary();

            Assert.IsTrue(games.Count == 0);
        }

        [Test]
        public void UNSUCCESS_FinishGames_NotExistsMatchName()
        {
            Assert.Throws<ArgumentException>(() => manager.FinishGame("Spain-DUMMY"));
        }

        [Test]
        public void UNSUCCESS_FinishGames_EmptyMatchName()
        {
            Assert.Throws<ArgumentException>(() => manager.FinishGame(""));
        }

        [Test]
        public void UNSUCCESS_FinishGames_NullMatchName()
        {
            Assert.Throws<ArgumentException>(() => manager.FinishGame(null));
        }

        #endregion

        #region UpdateGames

        [Test]
        public void UpdateGame_Overload1()
        {
            manager.StartGame("Mexico", "Canada");
            manager.UpdateGame("Mexico-Canada", "3-1");

            var games = manager.GetSummary();
            Assert.IsTrue(games.Count == 1);
            Assert.IsTrue(games[0].Scoring == "3-1");
        }

        [Test]
        public void UpdateGame_Overload2()
        {
            manager.StartGame("Mexico", "Canada");
            manager.UpdateGame("Mexico-Canada", 4, 6);

            var games = manager.GetSummary();
            Assert.IsTrue(games.Count == 1);
            Assert.IsTrue(games[0].Scoring == "4-6");
        }

        [Test]
        public void UNSUCCESS_UpdateGame_EmptyScoringV1()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", ""));
        }

        [Test]
        public void UNSUCCESS_UpdateGame_IncorrectScoringValuesV1()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "dummy"));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "dummy-bad"));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "-bad"));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "-b-ad-"));
        }

        [Test]
        public void UNSUCCESS_UpdateGame_NotExistsMatchNameV1()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-CanadaXXX", "0-9"));
        }

        [Test]
        public void UNSUCCESS_UpdateGame_IncorrectScoringValuesV2()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", -1, -6));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", -1, 10));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", 10, -10));
        }

        [Test]
        public void UNSUCCESS_UpdateGame_NotExistsMatchNameV2()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-CanadaXXX", 0, 9));
        }

        #endregion 
    }
}