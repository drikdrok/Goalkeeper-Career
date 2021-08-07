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

        bool foundMatch = false;
        while (!foundMatch)
        {
            foreach (var competition in CompetitionManager.Instance.competitions) //Find next match player plays in
            {
                if (competition.teamIds.Contains(PlayerPrefs.GetInt("TeamID")) && competition.hasPlayedWeek < PlayerPrefs.GetInt("Week"))
                {
                    foreach(var match in competition.matches[PlayerPrefs.GetInt("Week")])
                    {
                        if (match[0] == PlayerPrefs.GetInt("TeamID")  || match[1] == PlayerPrefs.GetInt("TeamID"))
                        {
                            PlayerPrefs.SetInt("HomeTeam", match[0]);
                            PlayerPrefs.SetInt("AwayTeam", match[1]);

                            CompetitionManager.Instance.currentCompetition = competition;

                            goto FoundMatch;
                        }
                    }
                }
            }
            //No unplayed match was found this week

            foreach (var competition in CompetitionManager.Instance.competitions) // Simulate every competition
                competition.simulateWeek();

            PlayerPrefs.SetInt("Week", PlayerPrefs.GetInt("Week") + 1);
            if (PlayerPrefs.GetInt("Week") >= 52)
            {
                foundMatch = true;
                PlayerPrefs.SetInt("HomeTeam", 1);
                PlayerPrefs.SetInt("AwayTeam", 1);
                CompetitionManager.Instance.currentCompetition = CompetitionManager.Instance.competitions[0];
                Debug.Log("End of year reached!");
            }
        }
        FoundMatch:

        clubText.text = "Club: " + TeamsManager.Instance.getName(PlayerPrefs.GetInt("TeamID"));
        weekText.text = "Week " + PlayerPrefs.GetInt("Week");
        fixtureText.text = TeamsManager.Instance.getName(PlayerPrefs.GetInt("HomeTeam")) + " - " + TeamsManager.Instance.getName(PlayerPrefs.GetInt("AwayTeam"));
        competitionText.text = CompetitionManager.Instance.currentCompetition.name;
        

       // Competition league = CompetitionManager.Instance.GetCompetition("DEN1");
       // Debug.Log(league.teamIds);
    
    }

    void Update()
    {
        
    }
}
