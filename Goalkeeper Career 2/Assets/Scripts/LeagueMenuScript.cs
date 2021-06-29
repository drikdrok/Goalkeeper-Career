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


    TeamList teamList;
    LeagueList leagueList;

    void Start()
    {
        leagueList = SaveLoad.loadLeaguesData();
        League currentLeague = leagueList.leagues[0];

        Debug.Log(currentLeague.name);


        teamList = SaveLoad.loadTeamsData();


        List<Team> teams = new List<Team>();

        for (int i = 0; i < teamList.teams.Length; i++)
        {
            teams.Add(teamList.teams[currentLeague.teamIds[i]]);
        }
     
        // Sort by points
        teams = teams.OrderBy(o => o.points).ThenBy(o => (o.GF - o.GA)).ThenBy(o => o.GF).ToList();
        teams.Reverse();

        for (int i = 0; i < teams.Count; i++)
        {
            Team team = teams[i];
            GameObject row = Instantiate(rowPrefab);
            row.transform.SetParent(tablePanel);
            
            row.transform.Find("Position").GetComponent<Text>().text = (i+1).ToString();
           
            row.transform.Find("Club").GetComponent<Text>().text = team.tag;
            row.transform.Find("Played").GetComponent<Text>().text = team.playedInLeague.ToString();
            row.transform.Find("Wins").GetComponent<Text>().text = team.wins.ToString();
            row.transform.Find("Draws").GetComponent<Text>().text = team.draws.ToString();
            row.transform.Find("Losses").GetComponent<Text>().text = team.losses.ToString();
            row.transform.Find("GF").GetComponent<Text>().text = team.GF.ToString();
            row.transform.Find("GA").GetComponent<Text>().text = team.GA.ToString();
            row.transform.Find("GD").GetComponent<Text>().text = (team.GF - team.GA).ToString();
            row.transform.Find("Points").GetComponent<Text>().text = team.points.ToString();
        }
    }

    void Update()
    {
        
    }
}
