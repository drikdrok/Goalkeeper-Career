using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        using (StreamWriter writer = new StreamWriter("Assets/Data/Competitions/" + name + ".json", false))
        {
            writer.Write(JsonConvert.SerializeObject(matchData, Formatting.Indented));
        }
    }

    public static List<List<Match>> loadMatchesData(string name)
    {
        using (StreamReader reader = new StreamReader("Assets/Data/Competitions/" + name + ".json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<List<Match>>>(JSONString);
        }
    }
}
