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
    public bool mustFindWinner, twoLegged, replayIfDraw;

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

        if ((mustFindWinner || twoLegged) && homeScore + homeAggregate == awayScore + awayAggregate) // Penalty shoot out
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
        }

        if (homeScore > awayScore || homePens > awayPens)
            winnerId = homeTeamId;
        else if (homeScore < awayScore || homePens < awayPens)
            winnerId = awayTeamId;

    }
}
