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

    public void GoToFixturesScreen()
    {
        SceneManager.LoadScene("Fixtures");
    }

    public void GoToCompetitonScreen()
    {
        CompetitionManager.Instance.currentCompetition = CompetitionManager.Instance.competitions[TeamsManager.Instance.teams[PlayerPrefs.GetInt("TeamID")].league];
        SceneManager.LoadScene("CompetitionScreen");
    }

    public void GoToSelectCompetition()
    {
        SceneManager.LoadScene("SelectCompetition");
    }

    public void GoToCareerScreen()
    {
        SceneManager.LoadScene("Career");
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

        PlayerPrefs.SetInt("GamesPlayed", 0);
        PlayerPrefs.SetInt("TotalWins", 0);
        PlayerPrefs.SetInt("TotalDraws", 0);
        PlayerPrefs.SetInt("TotalLosses", 0);
        PlayerPrefs.SetInt("TotalSaves", 0);
        PlayerPrefs.SetInt("TotalCatches", 0);
        PlayerPrefs.SetInt("TotalConceeded", 0);


    }

}
