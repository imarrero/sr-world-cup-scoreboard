using Moq;
using System;
using WorldCupGamesScoreBoard.Models;
using WorldCupGamesScoreBoard.Providers;
using Xunit;

namespace WorldCupGamesScoreBoard.Unit.Tests
{
    /// <summary>
    /// Unit tests against WorldCupGamesScoreBoard Library
    /// </summary>
    public class ScoreBoardManager_UnitTests
    {
        private Mock<IScoreBoardManager> _mockManager = new Mock<IScoreBoardManager>();
        private IScoreBoardManager manager = new ScoreBoardManagerSR();

        private readonly string _homeTeamDefault;
        private readonly string _awayTeamDefault;
        private readonly string _matchKeyDefault;

        public ScoreBoardManager_UnitTests()
        {
            manager = new ScoreBoardManagerSR();

            _homeTeamDefault = "Spain";
            _awayTeamDefault = "Canada";
            _matchKeyDefault = $"{_homeTeamDefault}-{_awayTeamDefault}";
        }

        #region StartGames

        [Fact]
        public void StartFiveGames()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");
            manager.StartGame("Uruguay", "Italy");
            manager.StartGame("Argentina", "Australia");

            var games = manager.GetSummary();
            Assert.Equal(5, games.Count);
        }

        [Fact]
        public void StartGameMockingTheManager()
        {
            // Real test: Inject _mockManager into the library API / Facade

            // MOCK
            _mockManager.Setup(p => p.StartGame(_homeTeamDefault, _awayTeamDefault))
                        .Returns(new Game()
                        {
                            HomeTeam = _homeTeamDefault,
                            AwayTeam = _awayTeamDefault,
                            HomeScore = 0,
                            AwayScore = 0,
                            MatchName = _matchKeyDefault,
                            StartDate = DateTime.Now
                        });
            // Act
            var game = _mockManager.Object.StartGame(_homeTeamDefault, _awayTeamDefault);

            // Assert
            Assert.NotNull(game);
            Assert.Equal(_homeTeamDefault, game.HomeTeam);
            Assert.Equal(_awayTeamDefault, game.AwayTeam);
            Assert.Equal(_matchKeyDefault, game.MatchName);

        }

        [Fact]
        public void UNSUCCESS_StartGames_DuplicatedMatch()
        {
            manager.StartGame("Germany", "France");
            Assert.Throws<ArgumentException>(() => manager.StartGame("Germany", "France"));
        }

        [Fact]
        public void UNSUCCESS_StartGames_TeamIsStillPlaying()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");

            Assert.Throws<ArgumentException>(() => manager.StartGame("Another", "Spain"));
            Assert.Throws<ArgumentException>(() => manager.StartGame("Spain", "Another"));
        }

        [Fact]
        public void UNSUCCESS_StartGames_EmptyHomeTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame("", "Brazil"));
        }

        [Fact]
        public void UNSUCCESS_StartGames_EmptyAwayTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame("Spain", ""));
        }

        [Fact]
        public void UNSUCCESS_StartGames_NullHomeTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame(null, "Brazil"));
        }

        [Fact]
        public void UNSUCCESS_StartGames_nullAwayTeam()
        {
            Assert.Throws<ArgumentException>(() => manager.StartGame("Spain", null));
        }

        #endregion

        #region FinishGames

        [Fact]
        public void FinishFiveGames()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");
            manager.StartGame("Uruguay", "Italy");
            manager.StartGame("Argentina", "Australia");

            Assert.True(manager.FinishGame("Spain-Brazil"));
            Assert.True(manager.FinishGame("Mexico-Canada"));
            Assert.True(manager.FinishGame("Uruguay-Italy"));
            Assert.True(manager.FinishGame("Germany-France"));
            Assert.True(manager.FinishGame("Argentina-Australia"));

            var games = manager.GetSummary();

            Assert.True(games.Count == 0);
        }

        [Fact]
        public void UNSUCCESS_FinishGames_NotExistsMatchName()
        {
            Assert.Throws<ArgumentException>(() => manager.FinishGame("Spain-DUMMY"));
        }

        [Fact]
        public void UNSUCCESS_FinishGames_EmptyMatchName()
        {
            Assert.Throws<ArgumentException>(() => manager.FinishGame(""));
        }

        [Fact]
        public void UNSUCCESS_FinishGames_NullMatchName()
        {
            Assert.Throws<ArgumentException>(() => manager.FinishGame(null));
        }

        #endregion

        #region UpdateGames

        [Fact]
        public void UpdateGame_Overload1()
        {
            manager.StartGame("Mexico", "Canada");
            manager.UpdateGame("Mexico-Canada", "3-1");

            var games = manager.GetSummary();
            Assert.True(games.Count == 1);
            Assert.True(games[0].HomeScore == 3);
            Assert.True(games[0].AwayScore == 1);
        }

        [Fact]
        public void UpdateGame_Overload2()
        {
            manager.StartGame("Mexico", "Canada");
            manager.UpdateGame("Mexico-Canada", 4, 6);

            var games = manager.GetSummary();
            Assert.True(games.Count == 1);
            Assert.True(games[0].HomeScore == 4);
            Assert.True(games[0].AwayScore == 6);
        }

        [Fact]
        public void UNSUCCESS_UpdateGame_EmptyScoringV1()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", ""));
        }

        [Fact]
        public void UNSUCCESS_UpdateGame_IncorrectScoringValuesV1()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "dummy"));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "dummy-bad"));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "-bad"));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", "-b-ad-"));
        }

        [Fact]
        public void UNSUCCESS_UpdateGame_NotExistsMatchNameV1()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-CanadaXXX", "0-9"));
        }

        [Fact]
        public void UNSUCCESS_UpdateGame_IncorrectScoringValuesV2()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", -1, -6));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", -1, 10));
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-Canada", 10, -10));
        }

        [Fact]
        public void UNSUCCESS_UpdateGame_NotExistsMatchNameV2()
        {
            manager.StartGame("Mexico", "Canada");
            Assert.Throws<ArgumentException>(() => manager.UpdateGame("Mexico-CanadaXXX", 0, 9));
        }

        #endregion

        #region GetSummaryGames

        [Fact]
        public void GetSummary()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");

            var games = manager.GetSummary();

            Assert.True(games.Count == 3);
            Assert.NotNull(games.Find(p => p.HomeTeam == "Mexico" && p.AwayTeam == "Canada"));
            Assert.NotNull(games.Find(p => p.HomeTeam == "Spain" && p.AwayTeam == "Brazil"));
            Assert.NotNull(games.Find(p => p.HomeTeam == "Germany" && p.AwayTeam == "France"));
        }

        [Fact]
        public void GetSummaryWithCorrectOrder()
        {
            manager.StartGame("Mexico", "Canada");
            manager.StartGame("Spain", "Brazil");
            manager.StartGame("Germany", "France");
            manager.StartGame("Uruguay", "Italy");
            manager.StartGame("Argentina", "Australia");

            manager.UpdateGame("Mexico-Canada", "0-5");
            manager.UpdateGame("Spain-Brazil", "10-2");
            manager.UpdateGame("Germany-France", "2-2");
            manager.UpdateGame("Uruguay-Italy", "6-6");
            manager.UpdateGame("Argentina-Australia", "3-1");

            var games = manager.GetSummary();

            Assert.True(games.Count == 5);

            Assert.True(games[0].HomeTeam == "Uruguay");
            Assert.True(games[1].HomeTeam == "Spain");
            Assert.True(games[2].HomeTeam == "Mexico");
            Assert.True(games[3].HomeTeam == "Argentina");
            Assert.True(games[4].HomeTeam == "Germany");

            foreach (var item in games)
            {
                Console.WriteLine($"{item.HomeTeam} {item.HomeScore} - {item.AwayTeam} {item.AwayScore}");
            };
        }

        #endregion
    }
}