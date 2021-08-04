using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixturesMenuScript : MonoBehaviour
{

    public Transform scrollView;
    public GameObject fixtureRow;

    LeagueList leagueList;


    void Start()
    {
        leagueList = SaveLoad.loadLeaguesData();
  

        League currentLeague = leagueList.leagues[0];

        Debug.Log(currentLeague.matches.Count);

        int matchday = 1;

        foreach (List<int> match in currentLeague.matches)
        {

            if (match[0] + 1 != matchday)
            {
                matchday++;
                GameObject mRow = Instantiate(fixtureRow);
                mRow.transform.SetParent(scrollView);
                mRow.transform.Find("Text").GetComponent<Text>().text = "Matchday " + (matchday-1).ToString();
            }
           
            GameObject row = Instantiate(fixtureRow);
            row.transform.SetParent(scrollView);
            row.transform.Find("Text").GetComponent<Text>().text = TeamsManager.Instance.getName(match[1]) + " - " + TeamsManager.Instance.getName(match[2]);
        }
    }

    void Update()
    {
        
    }
}
