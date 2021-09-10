using System.Collections.Generic;
using Xunit;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WorldCupGamesScoreBoard.BDD.Tests.Models;
using WorldCupGamesScoreBoard.Models;
using WorldCupGamesScoreBoard.Providers;

namespace WorldCupGamesScoreBoard.BDD.Tests.Steps
{
    [Binding]
    public class ScoreBoardSteps
    {
        private ScenarioContext _scenarioContext;
        private IScoreBoardManager _manager;

        public ScoreBoardSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetupBeforeScenario()
        {
            _manager = new ScoreBoardManagerSR();
        }

        [StepArgumentTransformation]
        public Team[] ConvertTeams(Table teams)
        {
            return teams.CreateSet<Team>().ToArray();
        }

        [Given(@"the teams")]
        public void GivenTheTeams(Team[] teams)
        {
            _scenarioContext.Add("currentTeams", teams);
        }

        [When(@"Start matches")]
        [Given(@"Start matches")]
        public void WhenStartMatches()
        {
            foreach (Team item in _scenarioContext.Get<Team[]>("currentTeams"))
            {
                _manager.StartGame(item.HomeTeam, item.AwayTeam);
            }
        }

        [When(@"Get Scoreboard summary")]
        [Given(@"Get Scoreboard summary")]
        public void WhenGetScoreboardSummary()
        {
            _scenarioContext.Add("currentSummary", _manager.GetSummary());
        }

        [Then(@"matches count should be (.*)")]
        public void ThenMatchesCountShouldBe(int count)
        {
            var games = _scenarioContext.Get<List<Game>>("currentSummary");
            Assert.Equal(games.Count, count);
        }
    }
}