using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CareerScreenScript : MonoBehaviour
{
    public Text stats;
    void Start()
    {
        stats.text = 
            "Games Played: " + PlayerPrefs.GetInt("GamesPlayed", 0) + "\n" +
            "Wins: " + PlayerPrefs.GetInt("TotalWins", 0) + "\n" +
            "Draws: " + PlayerPrefs.GetInt("TotalDraws", 0) + "\n" +
            "Losses: " + PlayerPrefs.GetInt("TotalLosses", 0) + "\n" + "\n" +
            "Saves: " + PlayerPrefs.GetInt("TotalSaves", 0) + "\n" +
            "Catches: " + PlayerPrefs.GetInt("TotalCatches", 0) + "\n" + "\n" +
            "Conceeded: " + PlayerPrefs.GetInt("TotalConceeded", 0) + "\n" 
            ;
    }

}
