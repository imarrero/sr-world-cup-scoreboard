Feature: ScoreBoard
	Simple ScoreBoard for the World Cup Games

@tag
Scenario: Start matches
	Given the teams
		| HomeTeam	| AwayTeam	|
        | Mexico	| Canada	|
        | Spain		| Brazil	|
		| Germany	| France	|
	When Start matches
	And Get Scoreboard summary
	Then matches count should be 3