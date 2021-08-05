using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CompetitionManager : MonoBehaviour
{
    public static CompetitionManager Instance { get; private set; }

    public List<Competition> competitions;

    public Competition currentCompetition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Initialize competitions

        competitions = SaveLoad.loadCompitionsData();
        Debug.Log("Initializing Competitions...");
        foreach (var competition in competitions)
        {
            Debug.Log(competition.name);
            competition.initialize();
        }
    }

    void OnApplicationQuit()
    {
        SaveLoad.saveCompitionsData(competitions);
    }

    public Competition GetCompetition(string competition)
    {
        foreach (var comp in competitions)
        {
            if (comp.name == competition)
                return comp;
        }
        throw new Exception("Competition does not exist!: " + competition);
    }

}


public class Competition
{
    public string name = "league";
    public List<int> teamIds;
    public List<int> remainingTeams;
    public string type;
    public int matchday = 1;
    public int hasPlayedWeek = 0;

    public List<List<List<int>>> matches;

    public void initialize()
    {
        matches = new List<List<List<int>>>();
        for (int i = 0; i < 52; i++) //52 weeks in a year
            matches.Add(new List<List<int>>());

        if (type == "league")
        {
            generateGenericLeague();
        }
        else if (type == "cup")
        {
            remainingTeams = new List<int>(teamIds);
            generateGenericCup();
        }
    }

    public void handleAfterMatchday()
    {


        matchday++;
    }


    public void simulateWeek()
    {
        if (hasPlayedWeek < PlayerPrefs.GetInt("Week"))
        {
            Debug.Log(name + ": Simulating week " + PlayerPrefs.GetInt("Week") +"...");
            foreach (var match in matches[PlayerPrefs.GetInt("Week")])
            {
                if (!(PlayerPrefs.GetInt("TeamID") == match[0] || PlayerPrefs.GetInt("TeamID") == match[1])) //Should not simulate match with player's team
                {
                    Team homeTeam = TeamsManager.Instance.teams[match[0]];
                    Team awayTeam = TeamsManager.Instance.teams[match[1]];

                    Tuple<int, int> scoreline = Match.simulateMatch(homeTeam, awayTeam); // Simulate match
                    int homeScore = scoreline.Item1;
                    int awayScore = scoreline.Item2;


                    Debug.Log(TeamsManager.Instance.getName(match[0]) + " " + homeScore + " - " + awayScore + " " + TeamsManager.Instance.getName(match[1]));
                }
            }

            hasPlayedWeek = PlayerPrefs.GetInt("Week");
        }
     }


    public void reset()
    {
        hasPlayedWeek = 0;
    }

    public void rotateList(List<int> list)
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
    public void generateGenericLeague()
    {

        var numberList = Enumerable.Range(0, teamIds.Count).ToList();

        var topList = numberList.GetRange(0, teamIds.Count / 2);
        var bottomList = numberList.GetRange(teamIds.Count / 2, teamIds.Count / 2).ToList();
        bottomList.Reverse();


        string message = "";
        foreach (var i in topList)
            message += i.ToString() + ", ";

        //Debug.Log("Toplist: " + message);

        message = "";
        foreach (var i in bottomList)
            message += i.ToString() + ", ";

        //Debug.Log("Bottomlist: " + message);

        for (int matchday = 1; matchday < teamIds.Count * 2; matchday++)
        {
            for (int i = 0; i < topList.Count; i++)
            {
                List<int> match = new List<int>();
                //match.Add(matchday);

                if (matchday % 2 == 0)
                {
                    match.Add(teamIds[topList[i]]);
                    match.Add(teamIds[bottomList[i]]);
                }
                else
                {
                    match.Add(teamIds[bottomList[i]]);
                    match.Add(teamIds[topList[i]]);
                }
                //  Debug.Log("Matchday: " + matchday + ": " + match[0] + " - " + match[1]);

                matches[matchday].Add(match);
            }

            rotateList(numberList);
            topList = numberList.GetRange(0, teamIds.Count / 2);
            bottomList = numberList.GetRange(teamIds.Count / 2, teamIds.Count / 2).ToList();
            bottomList.Reverse();
        }
    }

    public void generateGenericCup()
    {
        remainingTeams = teamIds.OrderBy(x => Random.value).ToList(); //Shuffle
        for (int i = 0; i < teamIds.Count / 2; i++)
        {
            List<int> match = new List<int>();
            match.Add(remainingTeams[0]);
            match.Add(remainingTeams[1]);

            matches[matchday].Add(match);

            remainingTeams.RemoveAt(0);
            remainingTeams.RemoveAt(0);
        }


    }
}