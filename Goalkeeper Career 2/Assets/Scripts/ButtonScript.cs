using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    void Start()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToLeagueScreen()
    {
        SceneManager.LoadScene("LeagueScreen");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void reset()
    {
        PlayerPrefs.SetInt("Matchday", 1);

        TeamList teams = SaveLoad.loadTeamsData();

        foreach (Team team in teams.teams)
        {
            team.playedInLeague = 0;
            team.wins = 0;
            team.draws = 0;
            team.losses = 0;
            team.GA = 0;
            team.GF = 0;
            team.points = 0;
        }

        SaveLoad.saveTeamsData(teams);

        
        SceneManager.LoadScene("MainMenu");

    }

}
