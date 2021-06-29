using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Match : MonoBehaviour
{
    public static Tuple<int, int> simulateMatch(Team homeTeam, Team awayTeam)
    {
        //Simulate match
        int homeScore = 0;
        int awayScore = 0;

        //Home goals
        int scoringChances = 5 + Random.Range(-1, 1);
        int scoringProbability = 25;
        if (homeTeam.stats.attackRating > awayTeam.stats.defenseRating)
        {
            scoringChances += Mathf.FloorToInt((homeTeam.stats.attackRating - awayTeam.stats.defenseRating) / 2);
            scoringProbability += Mathf.FloorToInt(homeTeam.stats.attackRating + 5 - awayTeam.stats.defenseRating) * 2;
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

        if (awayTeam.stats.attackRating > homeTeam.stats.defenseRating)
        {
            scoringChances += Mathf.FloorToInt((awayTeam.stats.attackRating - homeTeam.stats.defenseRating) / 2);
            scoringProbability += Mathf.FloorToInt(awayTeam.stats.attackRating - homeTeam.stats.defenseRating) * 2;
        }

        for (int i = 0; i < scoringChances; i++)
        {
            if (Random.Range(0, 100) < scoringProbability)
            {
                awayScore++;
            }
        }

        return Tuple.Create(homeScore, awayScore);
    }
}
