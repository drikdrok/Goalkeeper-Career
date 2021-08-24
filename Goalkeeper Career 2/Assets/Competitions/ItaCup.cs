using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItaCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("ITA1").teamIds);
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("ITA2").teamIds);

        for (int i = 0; i < 8; i++) // Remove top 12 teams from ITA 1
            competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("ITA1").lastSeasonTable[i]);

        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);

        if (competition.matchday == 2)
            for (int i = 0; i < 8; i++) // Add top 12 teams from ITA 1
                competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("ITA1").lastSeasonTable[i]);


        if (competition.remainingTeams.Count > 1 && competition.matchday != 5) // Should not generate match after first leg of semi final
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, (competition.remainingTeams.Count == 4) ? "2legsAway" : "");

        competition.lastLegWeek = PlayerPrefs.GetInt("Week");
    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("ItaCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("ItaCup", handleAfterMatchday);
    }
}
