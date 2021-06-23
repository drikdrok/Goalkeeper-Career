using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Text clubText;
    public Text matchdayText;
    public Text fixtureText;



    void Start()
    {
        TeamList teamList = SaveLoad.loadTeamsData();
        LeagueList leagueList = SaveLoad.loadLeaguesData();


        leagueList.leagues[0].generateFixtures();

        SaveLoad.saveLeaguesData(leagueList);
        SaveLoad.saveTeamsData(teamList);

        PlayerPrefs.SetInt("TeamID", 3);
         
        clubText.text = "Club: " + teamList.teams[PlayerPrefs.GetInt("TeamID")].tag;
        matchdayText.text = "Matchday " + PlayerPrefs.GetInt("Matchday");

        League currentLeague = leagueList.leagues[0];

        foreach (var match in currentLeague.matches)
        {
            if (match[0] == PlayerPrefs.GetInt("Matchday"))
            {
                if (match[1] == PlayerPrefs.GetInt("TeamID"))
                {
                    PlayerPrefs.SetInt("HomeTeam", match[1]);
                    PlayerPrefs.SetInt("AwayTeam", match[2]);
                    break;
                }
                else if (match[2] == PlayerPrefs.GetInt("TeamID"))
                {
                    PlayerPrefs.SetInt("HomeTeam", match[2]);
                    PlayerPrefs.SetInt("AwayTeam", match[1]);
                    break;
                }
            }
        }

        fixtureText.text = teamList.getName(PlayerPrefs.GetInt("HomeTeam")) + " - " + teamList.getName(PlayerPrefs.GetInt("AwayTeam"));

    }

    void Update()
    {
        
    }
}
