using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeagueMenuScript : MonoBehaviour
{

    public GameObject rowPrefab;
    public Transform tablePanel;


    void Start()
    {
        Competition currentCompetition = CompetitionManager.Instance.competitions[TeamsManager.Instance.teams[PlayerPrefs.GetInt("TeamID")].league];

        Debug.Log(currentCompetition.name);



        List<Team> teams = new List<Team>();

        for (int i = 0; i < currentCompetition.teamIds.Count; i++)
        {
            teams.Add(TeamsManager.Instance.teams[currentCompetition.teamIds[i]]);
        }
     
        // Sort by points
        teams = teams.OrderBy(o => currentCompetition.stats[o.id].points).ThenBy(o => (currentCompetition.stats[o.id].GF - currentCompetition.stats[o.id].GA)).ThenBy(o => currentCompetition.stats[o.id].GF).ToList();
        teams.Reverse();
        
        for (int i = 0; i < teams.Count; i++)
        {
            Team team = teams[i];
            GameObject row = Instantiate(rowPrefab);
            row.transform.SetParent(tablePanel);

            TeamStats stats = currentCompetition.stats[team.id];


            row.transform.Find("Position").GetComponent<Text>().text = (i+1).ToString();
           
            row.transform.Find("Club").GetComponent<Text>().text = team.tag;
            row.transform.Find("Played").GetComponent<Text>().text = stats.gamesPlayed.ToString();
            row.transform.Find("Wins").GetComponent<Text>().text = stats.wins.ToString();
            row.transform.Find("Draws").GetComponent<Text>().text = stats.draws.ToString();
            row.transform.Find("Losses").GetComponent<Text>().text = stats.losses.ToString();
            row.transform.Find("GF").GetComponent<Text>().text = stats.GF.ToString();
            row.transform.Find("GA").GetComponent<Text>().text = stats.GA.ToString();
            row.transform.Find("GD").GetComponent<Text>().text = (stats.GF - stats.GA).ToString();
            row.transform.Find("Points").GetComponent<Text>().text = stats.points.ToString();
        }
        
    }

    void Update()
    {
        
    }
}
