using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    


    public static void saveLeaguesData(LeagueList leagueList)
    {
        using (StreamWriter writer = new StreamWriter("Assets/Data/LeaguesData.json", false))
        {
            writer.Write(JsonConvert.SerializeObject(leagueList, Formatting.Indented));
        }
    }

    public static LeagueList loadLeaguesData()
    {
        using (StreamReader reader = new StreamReader("Assets/Data/LeaguesData.json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<LeagueList>(JSONString);
        }
    }


    public static void saveTeamsData(Team[] teams)
    {
        using (StreamWriter writer = new StreamWriter("Assets/Data/TeamsData.json", false))
        {
            writer.Write(JsonConvert.SerializeObject(teams, Formatting.Indented));
        }
    }

    public static Team[] loadTeamsData()
    {
        using (StreamReader reader = new StreamReader("Assets/Data/TeamsData.json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Team[]>(JSONString);
        }
    }


    public static void saveCompitionsData(List<Competition> compitionsData)
    {
        using (StreamWriter writer = new StreamWriter("Assets/Data/CompetitionsData.json", false))
        {
            writer.Write(JsonConvert.SerializeObject(compitionsData, Formatting.Indented));
        }
    }

    public static List<Competition> loadCompitionsData()
    {
        using (StreamReader reader = new StreamReader("Assets/Data/CompetitionsData.json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Competition>>(JSONString);
        }
    }


    public static void saveMatchesData(List<List<Match>> matchData, string name)
    {
        using (StreamWriter writer = new StreamWriter("Assets/Data/Matches/" + name + ".json", false))
        {
            writer.Write(JsonConvert.SerializeObject(matchData, Formatting.Indented));
        }
    }

    public static List<List<Match>> loadMatchesData(string name)
    {
        using (StreamReader reader = new StreamReader("Assets/Data/Matches/" + name + ".json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<List<Match>>>(JSONString);
        }
    }

    public static void saveStatsData(Dictionary<int, TeamStats> statsData, string name)
    {
        using (StreamWriter writer = new StreamWriter("Assets/Data/Stats/" + name + ".json", false))
        {
            writer.Write(JsonConvert.SerializeObject(statsData, Formatting.Indented));
        }
    }

    public static Dictionary<int, TeamStats> loadStatsData(string name)
    {
        using (StreamReader reader = new StreamReader("Assets/Data/Stats/" + name + ".json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<int, TeamStats>>(JSONString);
        }
    }

    public static void saveLastSeasonData(List<int> tableData, string name)
    {
        using (StreamWriter writer = new StreamWriter("Assets/Data/LastSeason/" + name + ".json", false))
        {
            writer.Write(JsonConvert.SerializeObject(tableData, Formatting.Indented));
        }
    }

    public static List<int> loadLastSeasonData(string name)
    {
        using (StreamReader reader = new StreamReader("Assets/Data/LastSeason/" + name + ".json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<int>>(JSONString);
        }
    }
}
