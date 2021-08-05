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
        PlayerPrefs.SetInt("Week", 1);

        SaveLoad.saveTeamsData(TeamsManager.Instance.teams);


        foreach (var competition in CompetitionManager.Instance.competitions)
        {
            competition.reset();
        }
        
        SceneManager.LoadScene("MainMenu");

    }

}
