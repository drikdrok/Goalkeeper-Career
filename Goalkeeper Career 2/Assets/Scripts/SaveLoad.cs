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

    public static void saveTeamsData(TeamList teams)
    {
        using (StreamWriter writer = new StreamWriter("Assets/New Data/TeamsData.json", false))
        {
            writer.Write(JsonConvert.SerializeObject(teams, Formatting.Indented));
        }
    }

    public static TeamList loadTeamsData()
    {
        using (StreamReader reader = new StreamReader("Assets/New Data/TeamsData.json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonUtility.FromJson<TeamList>(JSONString);
        }
    }


    public static void saveLeaguesData(LeagueList leagueList)
    {
        using (StreamWriter writer = new StreamWriter("Assets/New Data/LeaguesData.json", false))
        {
            writer.Write(JsonConvert.SerializeObject(leagueList, Formatting.Indented));
        }
    }

    public static LeagueList loadLeaguesData()
    {
        using (StreamReader reader = new StreamReader("Assets/New Data/LeaguesData.json"))
        {
            string JSONString = reader.ReadToEnd();
            return JsonUtility.FromJson<LeagueList>(JSONString);
        }
    }
}
