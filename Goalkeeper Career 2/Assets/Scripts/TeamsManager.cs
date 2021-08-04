using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsManager : MonoBehaviour
{
    public static TeamsManager Instance { get; private set; }

    public Team[] teams;

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

        teams = SaveLoad.loadTeamsData();

    }

    public string getName(int id)
    {
        return teams[id].tag;
    }
}
