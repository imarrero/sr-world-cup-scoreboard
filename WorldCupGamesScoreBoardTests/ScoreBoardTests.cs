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

        #region FinishGame

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
    }
}