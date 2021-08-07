using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Match : MonoBehaviour
{
    public static Tuple<int, int> simulateMatch(Team homeTeam, Team awayTeam, bool mustFindWinner)
    {
        //Simulate match
        int homeScore = 0;
        int awayScore = 0;

        //Home goals
        int scoringChances = 5 + Random.Range(-1, 1);
        int scoringProbability = 25;
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

        if (mustFindWinner && homeScore == awayScore)
            homeScore++;

        return Tuple.Create(homeScore, awayScore);
    }


    
}
