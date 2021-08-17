using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public int homeScore = 0;
    public int awayScore = 0;

    public int saveStreak = 0;

    public int minutes = 0;
    public float seconds = 0;

    public RawImage StreakIcon1;
    public RawImage StreakIcon2;
    public RawImage StreakIcon3;

    public Text scoreline;
    public Text timeText;

    int homeTeam;
    int awayTeam;

    public bool playerHome;


    void Start()
    {
        minutes = (int)Random.Range(0, 5);
        seconds = Random.Range(0, 58);

        homeTeam = PlayerPrefs.GetInt("HomeTeam");
        awayTeam = PlayerPrefs.GetInt("AwayTeam");

        playerHome = (homeTeam == PlayerPrefs.GetInt("TeamID")) ? true : false;
    }

    void Update()
    {

        scoreline.text = TeamsManager.Instance.getName(homeTeam) + "   " + homeScore + "-" + awayScore + "    " + TeamsManager.Instance.getName(awayTeam);


        seconds += Time.deltaTime;
        if (seconds > 60)
        {
            seconds = 0;
            minutes++;
        }
        string minuteString = (minutes < 10) ? "0" + minutes : minutes.ToString();
        string secondString = (seconds < 10) ? "0" + Mathf.Floor(seconds) : Mathf.Floor(seconds).ToString();

        timeText.text = minuteString + ":" + secondString;

        StreakIcon1.enabled = false;
        StreakIcon2.enabled = false;
        StreakIcon3.enabled = false;
        if (saveStreak > 0)
            StreakIcon1.enabled = true;
        if (saveStreak > 1)
            StreakIcon2.enabled = true;
        if (saveStreak > 2)
            StreakIcon3.enabled = true;
        if (saveStreak > 3)
        {
            saveStreak -= 4;
            if (playerHome)
                homeScore++;
            else
                awayScore++;
        }

    }

    public void newPosition()
    {
        minutes += (int)Random.Range(3, 15);
        seconds = Random.Range(0, 60);

        if (minutes > 95) { //Game finished

            CompetitionManager.recordStats(CompetitionManager.Instance.currentCompetition, homeTeam, awayTeam, homeScore, awayScore);

            PlayerPrefs.SetInt("HomeScore", homeScore);
            PlayerPrefs.SetInt("AwayScore", awayScore);


            if (playerHome)
            {
                if (homeScore > awayScore)
                    PlayerPrefs.SetInt("TotalWins", PlayerPrefs.GetInt("TotalWins", 0) + 1);
                else if (homeScore < awayScore)
                    PlayerPrefs.SetInt("TotalLosses", PlayerPrefs.GetInt("TotalLosses", 0) + 1);
                else
                    PlayerPrefs.SetInt("TotalDraws", PlayerPrefs.GetInt("TotalDraws", 0) + 1);
            }
            else
            {
                if (homeScore < awayScore)
                    PlayerPrefs.SetInt("TotalWins", PlayerPrefs.GetInt("TotalWins", 0) + 1);
                else if (homeScore > awayScore)
                    PlayerPrefs.SetInt("TotalLosses", PlayerPrefs.GetInt("TotalLosses", 0) + 1);
                else
                    PlayerPrefs.SetInt("TotalDraws", PlayerPrefs.GetInt("TotalDraws", 0) + 1);
            }

            PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed", 0) + 1);

            SceneManager.LoadScene("PostMatchScreen");
        }
    }
}
