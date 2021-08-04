using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Text clubText;
    public Text weekText;
    public Text competitionText;
    public Text fixtureText;



    void Start()
    {
        
         PlayerPrefs.SetInt("TeamID", 3);

         clubText.text = "Club: " + TeamsManager.Instance.getName(PlayerPrefs.GetInt("TeamID"));
         weekText.text = "Week " + CompetitionManager.Instance.week;

         Competition currentCompetition = CompetitionManager.Instance.competitions[0];

         foreach (var match in currentCompetition.matches[PlayerPrefs.GetInt("Matchday")])
         {
            if (match[0] == PlayerPrefs.GetInt("TeamID"))
            {
                PlayerPrefs.SetInt("HomeTeam", match[0]);
                PlayerPrefs.SetInt("AwayTeam", match[1]);
                break;
            }
            else if (match[1] == PlayerPrefs.GetInt("TeamID"))
            {
                PlayerPrefs.SetInt("HomeTeam", match[1]);
                PlayerPrefs.SetInt("AwayTeam", match[0]);
                break;
             }
         }

         fixtureText.text = TeamsManager.Instance.getName(PlayerPrefs.GetInt("HomeTeam")) + " - " + TeamsManager.Instance.getName(PlayerPrefs.GetInt("AwayTeam"));
         

       // Competition league = CompetitionManager.Instance.GetCompetition("DEN1");
       // Debug.Log(league.teamIds);
    
    }

    void Update()
    {
        
    }
}
