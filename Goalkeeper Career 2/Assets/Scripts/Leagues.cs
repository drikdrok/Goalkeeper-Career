using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Leagues : MonoBehaviour
{

    public LeagueList leagueList;
    public TeamList teamList;

    public void loadData()
    {
        teamList = SaveLoad.loadTeamsData();
        leagueList = SaveLoad.loadLeaguesData();
    }


    public string getTeamName(int id)
    {
        return teamList.teams[id].tag;
    }

    public void saveTeams()
    {

    }
}


[System.Serializable]
public class League
{
    public string name;
    public int[] teamIds;
    public List<List<int>> matches = new List<List<int>>();
    //Problem: Unity cant parse 2d lists into json...

    public void generateFixtures()
    {

        List<int> teams = teamIds.ToList();

        if (teams == null || teams.Count < 2)
        {
            Debug.LogError("Not enough teams in league!");
        }

        var restTeams = new List<int>(teams.Skip(1));
        var teamsCount = teams.Count;
        if (teams.Count % 2 != 0)
        {
            restTeams.Add(default);
            teamsCount++;
        }

        for (var day = 0; day < teamsCount - 1; day++)
        {
            if (restTeams[day % restTeams.Count].Equals(default) == false)
            {
                matches.Add( new List<int>() { day, teams[0], restTeams[day % restTeams.Count] });
            }

            for (var index = 1; index < teamsCount / 2; index++)
            {
                var firstTeam = restTeams[(day + index) % restTeams.Count];
                var secondTeam = restTeams[(day + restTeams.Count - index) % restTeams.Count];
                if (firstTeam.Equals(default) == false && secondTeam.Equals(default) == false)
                {
                    matches.Add(new List<int>() { day, firstTeam, secondTeam});
                }
            }
        }

        foreach (var match in matches)
        {
            Debug.Log(match[0] + "=>" + match[1]+ " - " + match[2]);
        }
    }
}

[System.Serializable]
public class LeagueList
{
    public League[] leagues;
}

[System.Serializable]
public class Team
{
    public int id;
    public string tag;
    public int league;

    public int wins = 10;
    public int draws = 0;
    public int losses = 0;
    public int GF = 0;
    public int GA = 0;
    public int points = 0;
    public int playedInLeague = 0;
}
[System.Serializable]
public class TeamList
{
    public Team[] teams;

    public string getName(int id)
    {
        return teams[id].tag;
    }
}
