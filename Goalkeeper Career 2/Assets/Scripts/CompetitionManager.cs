using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CompetitionManager : MonoBehaviour
{
    public static List<Competition> competitions;
    
    void Start()
    {
        competitions = SaveLoad.loadCompetitionsData();

        Debug.Log(competitions);
        Debug.Log(competitions[0].name);

        SaveLoad.saveCompetitionsData(competitions);
    }
}


[System.Serializable]
public class Competition
{
    public string name;
    public List<int> teamIds;
    //public List<List<int>> matches = new List<List<int>>();
}

[System.Serializable]
public class StandardCup : Competition
{

    public List<List<int>> matches = new List<List<int>>();

    public int[] matchdays;
    public int matchday = 0;

    void generateNextRound()
    {
        teamIds = teamIds.OrderBy(a => Guid.NewGuid()).ToList(); //Shuffle
        for (int i = 0; i < teamIds.Count / 2; i++)
        {

            matches.Add(new List<int>() {
                matchdays[matchday],
                teamIds[0],
                teamIds[1]
            });

            teamIds.RemoveAt(1);
            teamIds.RemoveAt(2);
        }

        matchday++;
    }

  

}
