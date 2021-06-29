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
                if (homeTeam.stats.attackRating > awayTeam.stats.defenseRating)
                {
                    scoringChances += Mathf.FloorToInt((homeTeam.stats.attackRating-awayTeam.stats.defenseRating) / 2);
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

                GameObject row = Instantiate(fixtureRow);
                row.transform.SetParent(scrollView);
                row.transform.Find("Text").GetComponent<Text>().text = teamList.getName(match[1]) + " " + homeScore + " - " + awayScore + " " + teamList.getName(match[2]);

                Debug.Log(teamList.getName(match[1]) + " " + homeScore + " - " + awayScore + " " + teamList.getName(match[2]));



                teamList.teams[match[1]].stats.GF += homeScore;
                teamList.teams[match[1]].stats.GA += awayScore;
                teamList.teams[match[2]].stats.GF += awayScore;
                teamList.teams[match[2]].stats.GA += homeScore;

                teamList.teams[match[1]].stats.playedInLeague++;
                teamList.teams[match[2]].stats.playedInLeague++;

                if (homeScore > awayScore)
                {
                    teamList.teams[match[1]].stats.wins++;
                    teamList.teams[match[1]].stats.points += 3;
                    teamList.teams[match[2]].stats.losses++;

                }
                else if (homeScore < awayScore)
                {
                    teamList.teams[match[1]].stats.losses++;
                    teamList.teams[match[2]].stats.wins++;
                    teamList.teams[match[2]].stats.points += 3;
                }
                else
                {
                    teamList.teams[match[1]].stats.points++;
                    teamList.teams[match[1]].stats.draws++;
                    teamList.teams[match[2]].stats.points++;
                    teamList.teams[match[2]].stats.draws++;
                }


            }
        }

        SaveLoad.saveTeamsData(teamList);
        PlayerPrefs.SetInt("Matchday", PlayerPrefs.GetInt("Matchday") + 1);


    }

}
