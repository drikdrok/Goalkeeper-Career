using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("FRA2").teamIds);

        for (int i = 0; i < 4; i++) // Remove top 4 teams from FRA2
            competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("FRA2").lastSeasonTable[i]);

        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);

        if (competition.matchday == 1)
        {
            competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("FRA1").teamIds);
            for (int i = 0; i < 4; i++) // Add top 4 teams from FRA2
                competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("FRA2").lastSeasonTable[i]);
        }


        if (competition.remainingTeams.Count > 1 ) 
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1,  "");

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("FraCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("FraCup", handleAfterMatchday);
    }
}
