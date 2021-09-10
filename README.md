# sr-world-cup-scoreboard
SR code challenge - imarrero

## Goals

Create a c# library to manage in memory Football World Cup matches data

Some actions must be implemented:
1. Start a game. Our data partners will send us data for the games when they start, and these should capture (Initial score is 0 – 0).
2. Finish game. It will remove a match from the scoreboard.
3. Update score. Receiving the pair score; home team score and away team score updates a game score.
4. Get a summary of games by total score. 

Detailed information is provided by PDF document

## How to run the project
1. Clone repository
2. Open sr-world-cup-scoreboard.sln
3. Build Solution
4. Execute Units tests: [More Info about Unit Tests](WorldCupGamesScoreBoardTests/_Readme.md)
6. Execute BDD tests: [More Info about Behaviour tests](WorldCupGamesScoreBoard.BDD.Tests/_Readme.md)
8. Run all tests, debug if required.

## Project management

Tickets created in Trello:
* https://trello.com/invite/b/wz4z2Hrw/b0f966ada9b722016cf8d60688a60a6e/tickets

* All commits are related to each ticket.

* As this is a test, all commits are in main branch. The correct way to go in a production environment should be more like:
* * Create a *development* and *release* branch
* * Code each ticket on a *feature* branch (from dev)
* * After local tests passed -> create a pull request to merge in *development*
* * If team CR approves the PR, then will be merged into *development*
This way, on each "release date" a new release branch can be create with all dev code. Then, depending on the company policy, (new test are done / staging etc..) finally release is merged into *MASTER* or *MAIN* and deployed to production.

## Future improvements

1. Infrastructure: Separate external parameters from internal use:
* Create a Facade/API/Gateway
* Set parameters validation in a centralized class (FluentValidation / DaataAnnotations.. )
* Use a model as a entry parameter

2.- Create performance tests
* Evaluate current Concurrent dictionary vs Performance (maybe using _locks in the critical methods is enough) 
* Make actions Async to really use Concurrent dictionary features
* * - No different consumers should touch the same MATCH at the same time: Evaluate

3. Persist data

4. Security
* Sanity text parameters
* Configuration for max dictionary items ( to avoid overflow of matches )

5. Implement Logging:
 - System logs 
 - Business data logs: Each CRUD operation should be saved, in order to get a complete trace of what happened in each match. 
 - Send to Elastic or similar
 - Create a Kibana or similar monitoring website: CONNECT TO SLACK or similar!! 

5. Create a WebAPI / Microservice to consume this library
* * User Swagger for documentation / secure access
* * Improve Error handling: Error codes centralized + Friendly Descriptions + Language resolution according request
* * * (Bonus) Create an API gateway to separate public endpoints from internal endpoints
* * * (Bonus) Containerize and deploy on an orchestrator
* * * (Bonus) Monitorize Logs with Kibana, scale pods as necessary, communicate events with other microservices in the cluster/cloud using rabbit/kafka.

Sorry for the overthinking. Just brainstorming surely Im missing important things
