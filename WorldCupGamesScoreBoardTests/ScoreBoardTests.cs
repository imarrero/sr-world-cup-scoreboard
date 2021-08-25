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
        [Order(1)]
        public void Setup()
        {
            manager = new ScoreBoardManagerSR();
        }

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
        [Order(2)]
        public void FinishFiveGames()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");
            manager.StartGame("Uruguay", "Italy");
            manager.StartGame("Argentina", "Australia");

            Assert.IsTrue(manager.FinishGame("Mexico-Canada"));
            Assert.IsTrue(manager.FinishGame("Spain-Brazil"));
            Assert.IsTrue(manager.FinishGame("Germany-France"));
            Assert.IsTrue(manager.FinishGame("Mexico-Canada"));
            Assert.IsTrue(manager.FinishGame("Uruguay-Italy"));
            Assert.IsTrue(manager.FinishGame("Argentina-Australia"));

        }
    }
}