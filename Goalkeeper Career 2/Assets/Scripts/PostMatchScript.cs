using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostMatchScript : MonoBehaviour
{

    LeagueList leagueList;
    TeamList teamList;

    public Transform scrollView;
    public GameObject fixtureRow;

    void Start()
    {
        leagueList = SaveLoad.loadLeaguesData();
        teamList = SaveLoad.loadTeamsData();


        League currentLeague = leagueList.leagues[0];


        int matchday = PlayerPrefs.GetInt("Matchday");
        foreach (var match in currentLeague.matches)
        {
            if (match[0] == matchday && !(PlayerPrefs.GetInt("TeamID") == match[1] || PlayerPrefs.GetInt("TeamID") == match[2]))
            {
                //Simulate match
                Team homeTeam = teamList.teams[match[1]];
                Team awayTeam = teamList.teams[match[2]];

                int homeScore = 0;
                int awayScore = 0;

                //Home goals
                int scoringChances = 5 + Random.Range(-1, 1);
                int scoringProbability = 25;
                if (homeTeam.attackRating > awayTeam.defenseRating)
                {
                    scoringChances += Mathf.FloorToInt((homeTeam.attackRating-awayTeam.defenseRating) / 2);
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

                GameObject row = Instantiate(fixtureRow);
                row.transform.SetParent(scrollView);
                row.transform.Find("Text").GetComponent<Text>().text = teamList.getName(match[1]) + " " + homeScore + " - " + awayScore + " " + teamList.getName(match[2]);

                Debug.Log(teamList.getName(match[1]) + " " + homeScore + " - " + awayScore + " " + teamList.getName(match[2]));



                teamList.teams[match[1]].GF += homeScore;
                teamList.teams[match[1]].GA += awayScore;
                teamList.teams[match[2]].GF += awayScore;
                teamList.teams[match[2]].GA += homeScore;

                teamList.teams[match[1]].playedInLeague++;
                teamList.teams[match[2]].playedInLeague++;

                if (homeScore > awayScore)
                {
                    teamList.teams[match[1]].wins++;
                    teamList.teams[match[1]].points += 3;
                    teamList.teams[match[2]].losses++;

                }
                else if (homeScore < awayScore)
                {
                    teamList.teams[match[1]].losses++;
                    teamList.teams[match[2]].wins++;
                    teamList.teams[match[2]].points += 3;
                }
                else
                {
                    teamList.teams[match[1]].points++;
                    teamList.teams[match[1]].draws++;
                    teamList.teams[match[2]].points++;
                    teamList.teams[match[2]].draws++;
                }


            }
        }

        SaveLoad.saveTeamsData(teamList);
        PlayerPrefs.SetInt("Matchday", PlayerPrefs.GetInt("Matchday") + 1);


    }

}
