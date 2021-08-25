# sr-world-cup-scoreboard
Sport Radar code challenge - imarrero

## Goals

Create a library to manage in memory Football World Cup matches data

Some actions must be implemented:
1. Start a game. Our data partners will send us data for the games when they start, and these should capture (Initial score is 0 – 0).
2. Finish game. It will remove a match from the scoreboard.
3. Update score. Receiving the pair score; home team score and away team score updates a game score.
4. Get a summary of games by total score. 

Detailed information is provided by PDF document

## How to run the project
1. Clone repository
2. Open sr-world-cup-scoreboard.sln
3. In the IDE set as startup WorldCupGamesScoreBoardTests unit test project
4. Run all tests, debug if required.

## Project management

Tickets created in Trello:
* https://trello.com/invite/b/wz4z2Hrw/b0f966ada9b722016cf8d60688a60a6e/tickets

* All commits are related to each ticket.

* As this is a test, all commits are in main branch. The correct way to go in a production environment should be more like:
* * Create a *development* and *release* branch
* * Dode each ticket on a *feature* branch (from dev)
* * After local tests passed -> create a pull request to merge in *development*
* * If team CR approves the PR, then will be merged into *development*
This way, on each "release date" a new release branch can be create with all dev code. Then, depending on the company policy, (new test are done / staging etc..) finally release is merged into *MASTER* or *MAIN* and deployed to production.

## Future improvements

1. Set parameters validation in a centralized class / Use a library like FluentValidation where possible
3. Persist data, currently is in memory
4. Implement Logging:
 - System logs 
 - Business data logs: Each CRUD operation should be saved, in order to get a complete trace of what happened in each match. 
 - Send to Elastic, for example.
5. Create a WebAPI / Microservice to consume this assembly (library) to share data on the internet
6. Create an API gateway to separate public endpoints from internal endpoints
7. Containerize and deploy on an orchestrator
8. Monitorize Logs with Kibana, scale pods as necessary, communicate with other ms in the cluster/cloud using rabbit/kafka.
Many things.. depending of requisites, could be a *never finished work*.
