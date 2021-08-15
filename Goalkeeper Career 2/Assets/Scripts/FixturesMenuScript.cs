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
       
            foreach (List<int> match in currentCompetition.matches[i])
            {
                GameObject row = Instantiate(fixtureRow);
                row.transform.SetParent(scrollView);
                row.transform.Find("Text").GetComponent<Text>().text = TeamsManager.Instance.getName(match[0]) + " - " + TeamsManager.Instance.getName(match[1]);
            }
        }

    }

    void Update()
    {
        
    }
}
