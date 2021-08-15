using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CompetitionScreen : MonoBehaviour
{
    public GameObject rowPrefab;
    public GameObject groupPrefab;
    public GameObject leaguePrefab;
    public GameObject tablePanel;


    void Start()
    {
        Debug.Log(CompetitionManager.Instance.currentCompetition.identifier);
        if (CompetitionManager.Instance.currentCompetition.type == "euro")
            initEuroCompetition();
        else if (CompetitionManager.Instance.currentCompetition.type == "league")
        {
            tablePanel.GetComponentInChildren<VerticalLayoutGroup>().spacing = 0;
            initLeague();
        }
        
    }

    void initLeague()
    {
        Competition currentCompetition = CompetitionManager.Instance.currentCompetition;

        GameObject league = Instantiate(leaguePrefab);
        league.transform.SetParent(tablePanel.transform);

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
            row.transform.SetParent(league.transform.Find("Teams"));

            TeamStats stats = currentCompetition.stats[team.id];


            row.transform.Find("Position").GetComponent<Text>().text = (i + 1).ToString();

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

    void initEuroCompetition()
    {
        Competition currentCompetition = CompetitionManager.Instance.currentCompetition;
        if (currentCompetition.matchday <= 6)
        {
            for (int i = 0; i < currentCompetition.groupStage.groups.Count; i++)
            {
                GameObject groupPanel = Instantiate(groupPrefab);
                groupPanel.transform.SetParent(tablePanel.transform);

                groupPanel.transform.Find("GroupName").GetComponent<Text>().text = "Group " + (i+1);

                List<Team> teams = new List<Team>();

                for (int j = 0; j < currentCompetition.groupStage.groups[i].Count; j++)
                    teams.Add(TeamsManager.Instance.teams[currentCompetition.groupStage.groups[i][j]]);

                // Sort by points
                teams = teams.OrderBy(o => currentCompetition.stats[o.id].points).ThenBy(o => (currentCompetition.stats[o.id].GF - currentCompetition.stats[o.id].GA)).ThenBy(o => currentCompetition.stats[o.id].GF).ToList();
                teams.Reverse();

                for (int j = 0; j < teams.Count; j++)
                {
                    GameObject row = Instantiate(rowPrefab);
                    row.transform.SetParent(groupPanel.transform.Find("Teams").GetComponent<Transform>().transform);

                    Team team = teams[j];
                    TeamStats stats = currentCompetition.stats[team.id];


                    row.transform.Find("Position").GetComponent<Text>().text = (j + 1).ToString();

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
        }
    }

}
