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


    LeagueList leagueList;

    void Start()
    {
        leagueList = SaveLoad.loadLeaguesData();
        League currentLeague = leagueList.leagues[0];

        Debug.Log(currentLeague.name);



        List<Team> teams = new List<Team>();

        for (int i = 0; i <currentLeague.teamIds.Length; i++)
        {
            teams.Add(TeamsManager.Instance.teams[currentLeague.teamIds[i]]);
        }
     
        // Sort by points
        teams = teams.OrderBy(o => o.stats.points).ThenBy(o => (o.stats.GF - o.stats.GA)).ThenBy(o => o.stats.GF).ToList();
        teams.Reverse();

        for (int i = 0; i < teams.Count; i++)
        {
            Team team = teams[i];
            GameObject row = Instantiate(rowPrefab);
            row.transform.SetParent(tablePanel);
            
            row.transform.Find("Position").GetComponent<Text>().text = (i+1).ToString();
           
            row.transform.Find("Club").GetComponent<Text>().text = team.tag;
            row.transform.Find("Played").GetComponent<Text>().text = team.stats.playedInLeague.ToString();
            row.transform.Find("Wins").GetComponent<Text>().text = team.stats.wins.ToString();
            row.transform.Find("Draws").GetComponent<Text>().text = team.stats.draws.ToString();
            row.transform.Find("Losses").GetComponent<Text>().text = team.stats.losses.ToString();
            row.transform.Find("GF").GetComponent<Text>().text = team.stats.GF.ToString();
            row.transform.Find("GA").GetComponent<Text>().text = team.stats.GA.ToString();
            row.transform.Find("GD").GetComponent<Text>().text = (team.stats.GF - team.stats.GA).ToString();
            row.transform.Find("Points").GetComponent<Text>().text = team.stats.points.ToString();
        }
    }

    void Update()
    {
        
    }
}
