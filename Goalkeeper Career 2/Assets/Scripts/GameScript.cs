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

    TeamList teamList;

    void Start()
    {

        teamList = SaveLoad.loadTeamsData();

        minutes = (int)Random.Range(0, 5);
        seconds = Random.Range(0, 58);

        homeTeam = PlayerPrefs.GetInt("HomeTeam");
        awayTeam = PlayerPrefs.GetInt("AwayTeam");
    }

    void Update()
    {

        scoreline.text = teamList.getName(homeTeam) + "   " + homeScore + "-" + awayScore + "    " + teamList.getName(awayTeam);


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
            homeScore++;
        }

    }

    public void newPosition()
    {
        minutes += (int)Random.Range(3, 15);
        seconds = Random.Range(0, 60);

        if (minutes > 95) { //Game finished

            teamList.teams[homeTeam].stats.playedInLeague++;
            teamList.teams[homeTeam].stats.GF += homeScore;
            teamList.teams[homeTeam].stats.GA += awayScore;

            teamList.teams[awayTeam].stats.playedInLeague++;
            teamList.teams[awayTeam].stats.GF += awayScore;
            teamList.teams[awayTeam].stats.GA += homeScore;

            if (homeScore > awayScore) // Home Win
            {
                teamList.teams[homeTeam].stats.points += 3;
                teamList.teams[homeTeam].stats.wins++;
                teamList.teams[awayTeam].stats.losses++;
            }
            else if (homeScore < awayScore) // Away Win
            {
                teamList.teams[awayTeam].stats.points += 3;
                teamList.teams[awayTeam].stats.wins++;
                teamList.teams[homeTeam].stats.losses++;
            }
            else { //Draw
                teamList.teams[homeTeam].stats.points++;
                teamList.teams[awayTeam].stats.points++;
                teamList.teams[homeTeam].stats.draws++;
                teamList.teams[awayTeam].stats.draws++;
            }

            SaveLoad.saveTeamsData(teamList);

            SceneManager.LoadScene("PostMatchScreen");
        }
    }
}
