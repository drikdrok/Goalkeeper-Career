using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CompetitionManager : MonoBehaviour
{
    public static CompetitionManager Instance { get; private set; }

    public List<Competition> competitions;

    public Competition currentCompetition;

    public Dictionary<string, Action<Competition>> initFunctions;
    public Dictionary<string, Action<Competition>> handleAfterMatchday;

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

        initFunctions = new Dictionary<string, Action<Competition>>();
        handleAfterMatchday = new Dictionary<string, Action<Competition>>();
    }

    private void Start()
    {
        //Initialize competitions
        competitions = SaveLoad.loadCompitionsData();
        Debug.Log("Initializing Competitions...");
        foreach (var competition in competitions)
        {
           // Debug.Log(competition.name);
            if (System.IO.File.Exists("Assets/Data/Matches/" + competition.name + ".json") && System.IO.File.Exists("Assets/Data/Stats/" + competition.name + ".json") && System.IO.File.Exists("Assets/Data/LastSeason/" + competition.name + ".json"))
            {
                competition.matches = SaveLoad.loadMatchesData(competition.name);
                competition.stats = SaveLoad.loadStatsData(competition.name);
                competition.lastSeasonTable = SaveLoad.loadLastSeasonData(competition.name);
            }
            else
            {
                competition.lastSeasonTable = new List<int>(competition.teamIds);
                SaveLoad.saveLastSeasonData(competition.lastSeasonTable, competition.name);
                competition.initialize();
            }

            if (!competition.isInitialized)
                competition.initialize();

        }
    }

    void OnApplicationQuit()
    {
        foreach (var competition in competitions)
        {
            SaveLoad.saveMatchesData(competition.matches, competition.name);
            competition.matches = null;

            SaveLoad.saveStatsData(competition.stats, competition.name);
            competition.stats = null;

            competition.lastSeasonTable = null;
        }

        SaveLoad.saveCompitionsData(competitions);
    }

    public Competition getCompetition(string competition)
    {
        foreach (var comp in competitions)
        {
            if (comp.name == competition)
                return comp;
        }
        throw new Exception("Competition does not exist!: " + competition);
    }

    public static void recordStats(Competition competition, int homeTeam, int awayTeam, int homeScore, int awayScore)
    {
        if (!competition.stats.ContainsKey(homeTeam))
            competition.stats.Add(homeTeam, new TeamStats());
        if (!competition.stats.ContainsKey(awayTeam))
            competition.stats.Add(awayTeam, new TeamStats());

        TeamStats homeStats = competition.stats[homeTeam];
        TeamStats awayStats = competition.stats[awayTeam];

        homeStats.gamesPlayed++;
        homeStats.GF += homeScore;
        homeStats.GA += awayScore;

        awayStats.gamesPlayed++;
        awayStats.GF += awayScore;
        awayStats.GA += homeScore;

        if (homeScore > awayScore) // Home Win
        {
            homeStats.wins++;
            homeStats.points += 3;
            awayStats.losses++;
        }
        else if (homeScore < awayScore) // Away Win
        {
            awayStats.wins++;
            awayStats.points += 3;
            homeStats.losses++;
        }
        else
        { //Draw
            homeStats.draws++;
            homeStats.points++;
            awayStats.draws++;
            awayStats.points++;
        }
    }

}


public class Competition
{
    public string name = "league";
    public List<int> teamIds;
    public List<int> remainingTeams;
    public string type;
    public string identifier;
    public int matchday = 1;
    public int hasPlayedWeek = 0;
    public int lastLegWeek = 0;

    public bool isInitialized = false;

    public Dictionary<int, TeamStats> stats;

    public GroupStage groupStage = null;

    public List<List<Match>> matches;

    public List<int> lastSeasonTable;

    public void initialize()
    {
        isInitialized = true;

        matches = new List<List<Match>>();
        for (int i = 0; i < 52; i++) //52 weeks in a year
            matches.Add(new List<Match>());

        if (stats == null)
        {
           stats = new Dictionary<int, TeamStats>();
           foreach (int teamId in teamIds)
                stats.Add(teamId, new TeamStats());
        }

        if (type == "cup")
            remainingTeams = new List<int>(teamIds);

        if (CompetitionManager.Instance.initFunctions.ContainsKey(identifier))
            CompetitionManager.Instance.initFunctions[identifier](this);

    }

    public void handleAfterMatchday()
    {
        if (CompetitionManager.Instance.handleAfterMatchday.ContainsKey(identifier))
            CompetitionManager.Instance.handleAfterMatchday[identifier](this);

        matchday++;
    }


    public void simulateWeek()
    {

        //Add stats for teams that may have been added 
        foreach (var team in remainingTeams)
            if (!stats.ContainsKey(team))
                stats.Add(team, new TeamStats());


        if (hasPlayedWeek < PlayerPrefs.GetInt("Week"))
        {
            Debug.Log(name + ": Simulating week " + PlayerPrefs.GetInt("Week") +"...");
            foreach (var match in matches[PlayerPrefs.GetInt("Week")])
            {
                if (!(PlayerPrefs.GetInt("TeamID") == match.homeTeamId || PlayerPrefs.GetInt("TeamID") == match.awayTeamId)) //Should not simulate match with player's team
                {
                    Team homeTeam = TeamsManager.Instance.teams[match.homeTeamId];
                    Team awayTeam = TeamsManager.Instance.teams[match.awayTeamId];

                    if (match.twoLegged) // 2-Legged match
                    {
                        foreach (var prevLeg in matches[lastLegWeek])
                        {
                            if (prevLeg.homeTeamId == awayTeam.id) // Previous leg found
                            {
                                match.homeAggregate = prevLeg.awayScore;
                                match.awayAggregate = prevLeg.homeScore;
                                break;
                            } else if (prevLeg.awayTeamId == homeTeam.id)
                            {
                                match.homeAggregate = prevLeg.homeScore;
                                match.awayAggregate = prevLeg.awayScore;
                                break;
                            }
                        }
                    }

                    match.simulate(); // Simulate match

                    CompetitionManager.recordStats(this, homeTeam.id, awayTeam.id, match.homeScore, match.awayScore); // Record stats

                    if (match.replayIfDraw && match.homeScore == match.awayScore)
                    {
                        Match replay = new Match(awayTeam.id, homeTeam.id);
                        replay.mustFindWinner = true;
                        matches[PlayerPrefs.GetInt("Week") + 1].Add(replay);
                    }


                    // Debug.Log(TeamsManager.Instance.getName(match[0]) + " " + homeScore + " - " + awayScore + " " + TeamsManager.Instance.getName(match[1]));
                }
                else
                {
                    match.homeScore = PlayerPrefs.GetInt("HomeScore");
                    match.awayScore = PlayerPrefs.GetInt("AwayScore");
                    match.winnerId = match.homeTeamId;
                    
                }
            }

            hasPlayedWeek = PlayerPrefs.GetInt("Week");
            handleAfterMatchday();
        }
     }


    public void reset()
    {
        stats = null;

        initialize();
        hasPlayedWeek = 0;
        matchday = 1;

        if (type == "cup")
            remainingTeams = new List<int>(teamIds);
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

        for (int matchday = 1; matchday < teamIds.Count * 2 - 1; matchday++)
        {
            for (int i = 0; i < topList.Count; i++)
            {
                Match match = new Match();
                //match.Add(matchday);

                if (matchday % 2 == 0)
                {
                    match.homeTeamId = teamIds[topList[i]];
                    match.awayTeamId = teamIds[bottomList[i]];
                }
                else
                {
                    match.homeTeamId = teamIds[bottomList[i]];
                    match.awayTeamId = teamIds[topList[i]];
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

    public void generateLeagueFrom(List<int> teams)
    {
        var numberList = Enumerable.Range(0, teams.Count).ToList();

        var topList = numberList.GetRange(0, teams.Count / 2);
        var bottomList = numberList.GetRange(teams.Count / 2, teams.Count / 2).ToList();
        bottomList.Reverse();

        for (int matchday = 1; matchday < teams.Count * 2 - 1; matchday++)
        {
            for (int i = 0; i < topList.Count; i++)
            {
                Match match = new Match();

                if (matchday % 2 == 0)
                {
                    match.homeTeamId = teams[topList[i]];
                    match.awayTeamId = teams[bottomList[i]];
                }
                else
                {
                    match.homeTeamId = teams[bottomList[i]];
                    match.awayTeamId = teams[topList[i]];
                }

                matches[matchday].Add(match);
            }

            rotateList(numberList);
            topList = numberList.GetRange(0, teams.Count / 2);
            bottomList = numberList.GetRange(teams.Count / 2, teams.Count / 2).ToList();
            bottomList.Reverse();
        }
    }

    public void generateGenericCup(int week, string matchSpecifics)
    {
        remainingTeams = remainingTeams.OrderBy(x => Random.value).ToList(); //Shuffle
        if (remainingTeams.Count % 2 == 1)
            Debug.LogError("Number of remaining teams is uneven!  " + remainingTeams.Count);

        int remaining = remainingTeams.Count;


        for (int i = 0; i < remaining / 2; i++)
        {
            Match match = new Match(remainingTeams[0], remainingTeams[1]);


            if (matchSpecifics == "replays")
                match.replayIfDraw = true;
            else
                match.mustFindWinner = true;

            if (matchSpecifics == "2legs" || matchSpecifics == "2legsAway")
            {
                match.mustFindWinner = false;
            }

            //Debug.Log("New Match: " + match[0] + ", " + match[1]);

            matches[week].Add(match);

            if (matchSpecifics == "2legs" || matchSpecifics == "2legsAway")
            {
                match = new Match(remainingTeams[1], remainingTeams[0]);
                match.twoLegged = true;
                if (matchSpecifics == "2legsAway")
                    match.awayGoalRule = true;

                matches[week+1].Add(match);
            }

            remainingTeams.RemoveAt(0);
            remainingTeams.RemoveAt(0);
        }
    }

}