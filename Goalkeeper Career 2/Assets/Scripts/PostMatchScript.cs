using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PostMatchScript : MonoBehaviour
{

    public Transform scrollView;
    public GameObject fixtureRow;

    void Start()
    {
        CompetitionManager.Instance.currentCompetition.simulateWeek();

        foreach (var match in CompetitionManager.Instance.currentCompetition.matches[PlayerPrefs.GetInt("Week")])
        {
            //if ((PlayerPrefs.GetInt("TeamID") == match.homeTeamId || PlayerPrefs.GetInt("TeamID") == match.awayTeamId)) //Should Ignore player's team's match
            //    continue;
            
            GameObject row = Instantiate(fixtureRow);
            row.transform.SetParent(scrollView);
            row.transform.Find("Text").GetComponent<Text>().text = TeamsManager.Instance.getName(match.homeTeamId) + " " + match.homeScore + " - " + match.awayScore + " " + TeamsManager.Instance.getName(match.awayTeamId);

            if (match.homePens != 0 || match.awayPens != 0)
                row.transform.Find("Pens").GetComponent<Text>().text = "Penalties: " + match.homePens + " - " + match.awayPens;
        }
    }

}


