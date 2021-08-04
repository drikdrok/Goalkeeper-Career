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
    public void GoToFixturesScreen()
    {
        SceneManager.LoadScene("Fixtures");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void reset()
    {
        PlayerPrefs.SetInt("Matchday", 1);

        foreach (Team team in TeamsManager.Instance.teams)
        {
            team.stats.playedInLeague = 0;
            team.stats.wins = 0;
            team.stats.draws = 0;
            team.stats.losses = 0;
            team.stats.GA = 0;
            team.stats.GF = 0;
            team.stats.points = 0;
        }

        SaveLoad.saveTeamsData(TeamsManager.Instance.teams);

        
        SceneManager.LoadScene("MainMenu");

    }

}
