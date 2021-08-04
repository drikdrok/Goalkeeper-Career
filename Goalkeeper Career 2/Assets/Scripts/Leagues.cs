using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Leagues : MonoBehaviour
{

    public LeagueList leagueList;


    public void loadData()
    {
      
        leagueList = SaveLoad.loadLeaguesData();
    }


    public string getTeamName(int id)
    {
        return TeamsManager.Instance.teams[id].tag;
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

    public void generateFixtures()
    {
        matches = new List<List<int>>();
        var numberList = Enumerable.Range(0, teamIds.Length).ToList();

        var topList = numberList.GetRange(0, teamIds.Length / 2);
        var bottomList = numberList.GetRange(teamIds.Length / 2, teamIds.Length / 2).ToList();
        bottomList.Reverse();

      


        string message = "";
        foreach (var i in topList)
            message += i.ToString() + ", ";

       // Debug.Log("Toplist: " + message);

        message = "";
        foreach (var i in bottomList)
            message += i.ToString() + ", ";

       // Debug.Log("Bottomlist: " + message);



        for (int matchday = 1; matchday < teamIds.Length*2; matchday++)
        {
            for (int i = 0; i < topList.Count; i++)
            {
                List<int> match = new List<int>();
                match.Add(matchday);
                
                if (matchday % 2 == 0)
                {
                    match.Add(topList[i]);
                    match.Add(bottomList[i]);
                }
                else
                {
                    match.Add(bottomList[i]);
                    match.Add(topList[i]);
                }
                //Debug.Log("Match: " + match[0] + ": " + match[1] + " - " + match[2]);

                matches.Add(match);
            }

            rotateList(numberList);
            topList = numberList.GetRange(0, teamIds.Length / 2);
            bottomList = numberList.GetRange(teamIds.Length / 2, teamIds.Length / 2).ToList();
            bottomList.Reverse();
        }

        void rotateList(List<int> list)
        {
            int j, last;
            last = list[list.Count - 1];

            for (j = list.Count - 1; j > 0; j--)
            {
                list[j] = list[j - 1];
            }
            list[0] = last;

              int start = list[0];
              list[0] = list[1];
              list[1] = start;
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

    public TeamStats stats = new TeamStats();

}

[System.Serializable]
public class TeamStats
{
    public int wins = 0;
    public int draws = 0;
    public int losses = 0;
    public int GF = 0;
    public int GA = 0;
    public int points = 0;
    public int playedInLeague = 0;

    public int attackRating = 50;
    public int midfieldRating = 50;
    public int defenseRating = 50;
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
