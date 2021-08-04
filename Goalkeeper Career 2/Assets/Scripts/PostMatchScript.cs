using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PostMatchScript : MonoBehaviour
{

    LeagueList leagueList;


    public Transform scrollView;
    public GameObject fixtureRow;

    void Start()
    {
        leagueList = SaveLoad.loadLeaguesData();
 


        League currentLeague = leagueList.leagues[0];


        int matchday = PlayerPrefs.GetInt("Matchday");
        foreach (var match in currentLeague.matches)
        {
            if (match[0] == matchday && !(PlayerPrefs.GetInt("TeamID") == match[1] || PlayerPrefs.GetInt("TeamID") == match[2]))
            {
                Team homeTeam = TeamsManager.Instance.teams[match[1]];
                Team awayTeam = TeamsManager.Instance.teams[match[2]];

                Tuple<int, int> scoreline = Match.simulateMatch(homeTeam, awayTeam);
                int homeScore = scoreline.Item1;
                int awayScore = scoreline.Item2;


                GameObject row = Instantiate(fixtureRow);
                row.transform.SetParent(scrollView);
                row.transform.Find("Text").GetComponent<Text>().text = TeamsManager.Instance.getName(match[1]) + " " + homeScore + " - " + awayScore + " " + TeamsManager.Instance.getName(match[2]);

                Debug.Log(TeamsManager.Instance.getName(match[1]) + " " + homeScore + " - " + awayScore + " " + TeamsManager.Instance.getName(match[2]));



                TeamsManager.Instance.teams[match[1]].stats.GF += homeScore;
                TeamsManager.Instance.teams[match[1]].stats.GA += awayScore;
                TeamsManager.Instance.teams[match[2]].stats.GF += awayScore;
                TeamsManager.Instance.teams[match[2]].stats.GA += homeScore;

                TeamsManager.Instance.teams[match[1]].stats.playedInLeague++;
                TeamsManager.Instance.teams[match[2]].stats.playedInLeague++;

                if (homeScore > awayScore)
                {
                    TeamsManager.Instance.teams[match[1]].stats.wins++;
                    TeamsManager.Instance.teams[match[1]].stats.points += 3;
                    TeamsManager.Instance.teams[match[2]].stats.losses++;

                }
                else if (homeScore < awayScore)
                {
                    TeamsManager.Instance.teams[match[1]].stats.losses++;
                    TeamsManager.Instance.teams[match[2]].stats.wins++;
                    TeamsManager.Instance.teams[match[2]].stats.points += 3;
                }
                else
                {
                    TeamsManager.Instance.teams[match[1]].stats.points++;
                    TeamsManager.Instance.teams[match[1]].stats.draws++;
                    TeamsManager.Instance.teams[match[2]].stats.points++;
                    TeamsManager.Instance.teams[match[2]].stats.draws++;
                }


            }
        }

        SaveLoad.saveTeamsData(TeamsManager.Instance.teams);
        PlayerPrefs.SetInt("Matchday", PlayerPrefs.GetInt("Matchday") + 1);


    }

}


