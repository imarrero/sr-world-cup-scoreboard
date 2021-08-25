# sr-world-cup-scoreboard
Sport Radar code challenge

## Goals

Create a library to manage in memory Football World Cup matches data

Some actions mus be implemented:
1. Start a game. Our data partners will send us data for the games when they start, and these should capture (Initial score is 0 – 0).
2. Finish game. It will remove a match from the scoreboard.
3. Update score. Receiving the pair score; home team score and away team score updates a game score.
4. Get a summary of games by total score. 

Detailed information is provided by PDF document

## How to run the project:
1. Clone reepository
2. In the IDE set as startup WorldCupGamesScoreBoardTests unit test project
3. Run all tests, debug if required.

## Project management:

Tickets created in Trello:
* https://trello.com/invite/b/wz4z2Hrw/b0f966ada9b722016cf8d60688a60a6e/tickets

* All commits are related to each ticket.

* As this is a test, all commits are in main branch. The correct way to go in a production environment should be more like:
* * Create a *development* and *release* branch
* * code each ticket on a *feature* branch (from dev)
* * After local tests passed -> create a pull request to merge in *development*
* * If team CR approves the PR, theh its merged into *development*
This way on each "release date" a new release branch can be create with all dev code. Then depending on the company policy, new test are done / staging etc and finally merged into *MASTER* or *MAIN* and deployed to production.

## Future improvements:
