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
            if ((PlayerPrefs.GetInt("TeamID") == match[0] || PlayerPrefs.GetInt("TeamID") == match[1])) //Should Ignore player's team's match
                continue;
            
            GameObject row = Instantiate(fixtureRow);
            row.transform.SetParent(scrollView);
            row.transform.Find("Text").GetComponent<Text>().text = TeamsManager.Instance.getName(match[0]) + " " + match[2] + " - " + match[3] + " " + TeamsManager.Instance.getName(match[1]);

        }
    }

}


