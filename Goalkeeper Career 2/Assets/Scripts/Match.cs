using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Match
{
    public int homeTeamId, awayTeamId;
    public int homeScore = 0, awayScore = 0, homePens = 0, awayPens = 0, homeAggregate = 0, awayAggregate = 0; 
    public int winnerId = -1;
    public bool mustFindWinner, twoLegged, replayIfDraw, awayGoalRule;

    public Match()
    {

    }

    public Match(int homeTeam, int awayTeam)
    {
        homeTeamId = homeTeam;
        awayTeamId = awayTeam;

    }




    public void simulate()
    {
        //Home goals
        int scoringChances = 5 + Random.Range(-1, 1);
        int scoringProbability = 25;

        Team homeTeam = TeamsManager.Instance.teams[homeTeamId];
        Team awayTeam = TeamsManager.Instance.teams[awayTeamId];

        if (homeTeam.attackRating > awayTeam.defenseRating)
        {
            scoringChances += Mathf.FloorToInt((homeTeam.attackRating - awayTeam.defenseRating) / 2);
            scoringProbability += Mathf.FloorToInt(homeTeam.attackRating + 5 - awayTeam.defenseRating) * 2;
        }

        for (int i = 0; i < scoringChances; i++)
        {
            if (Random.Range(0, 100) < scoringProbability)
            {
                homeScore++;
            }
        }

        //Away goals
        scoringChances = 5 + Random.Range(-1, 1);
        scoringProbability = 25;

        if (awayTeam.attackRating > homeTeam.defenseRating)
        {
            scoringChances += Mathf.FloorToInt((awayTeam.attackRating - homeTeam.defenseRating) / 2);
            scoringProbability += Mathf.FloorToInt(awayTeam.attackRating - homeTeam.defenseRating) * 2;
        }

        for (int i = 0; i < scoringChances; i++)
        {
            if (Random.Range(0, 100) < scoringProbability)
            {
                awayScore++;
            }
        }


        if (twoLegged)
        {

            if (homeScore + homeAggregate > awayScore + awayAggregate)
            {
                winnerId = homeTeamId;
                return;
            } else if (homeScore + homeAggregate < awayScore + awayAggregate)
            {
                winnerId = awayTeamId;
                return;
            }

            if (awayGoalRule && homeScore + homeAggregate == awayScore + awayAggregate)
            {
                if (homeAggregate > awayScore)
                    winnerId = homeTeamId;
                else if (homeAggregate < awayScore)
                    winnerId = awayTeamId;
                else
                    penalties();

                return;
            }

            if (homeScore + homeAggregate == awayScore + awayAggregate)
                penalties();
        } 
        
        if (mustFindWinner && homeScore == awayScore)
        {
            penalties();
        }

        if (homeScore > awayScore)
            winnerId = homeTeamId;
        else if (homeScore < awayScore)
            winnerId = awayTeamId;

    }


    void penalties()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Random.value < 0.8f)
                homePens++;
            if (Random.value < 0.8f)
                awayPens++;
        }

        while (homePens == awayPens)
        {
            if (Random.value < 0.8f)
                homePens++;
            if (Random.value < 0.8f)
                awayPens++;
        }

        if (homePens > awayPens)
            winnerId = homeTeamId;
        else
            winnerId = awayTeamId;
    }
}
