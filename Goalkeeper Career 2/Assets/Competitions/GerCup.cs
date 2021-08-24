using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("GER1").teamIds);
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("GER2").teamIds);
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("GER3").teamIds);

        for (int i = 0; i < 8; i++) // Remove previous top 8 from GER1
            competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("GER1").lastSeasonTable[i]);


        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);
        

        if (competition.matchday == 1)
            for (int i = 0; i < 8; i++) // Add previous top 8 from GER1
                competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("GER1").lastSeasonTable[i]);


        if (competition.remainingTeams.Count > 1)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, "");
    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("GerCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("GerCup", handleAfterMatchday);
    }
}
