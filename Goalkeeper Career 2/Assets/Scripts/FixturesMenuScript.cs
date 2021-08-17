using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixturesMenuScript : MonoBehaviour
{

    public Transform scrollView;
    public GameObject fixtureRow;

    void Start()
    {
        Competition currentCompetition = CompetitionManager.Instance.currentCompetition;

        for (int i = 1; i < 52; i++)
        {
            GameObject mRow = Instantiate(fixtureRow);
            mRow.transform.SetParent(scrollView);
            mRow.transform.Find("Text").GetComponent<Text>().text = "Week" + i;
       
            foreach (var match in currentCompetition.matches[i])
            {
                GameObject row = Instantiate(fixtureRow);
                row.transform.SetParent(scrollView);

                Debug.Log(match.homeTeamId + ", " + match.awayTeamId);
                string text = TeamsManager.Instance.getName(match.homeTeamId) + " - " + TeamsManager.Instance.getName(match.awayTeamId);

                text = match.homeScore + " " + text + " " + match.awayScore;

                row.transform.Find("Text").GetComponent<Text>().text = text;

                if (match.homePens != 0 || match.awayPens != 0)
                    row.transform.Find("Pens").GetComponent<Text>().text = "Penalties: " + match.homePens + " - " + match.awayPens;
            }
        }

    }
}
