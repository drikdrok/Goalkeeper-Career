using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("SPA2").teamIds);

        //TODO: Remove top 2 non promoted teams 
        competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("SPA2").lastSeasonTable[6]);
        competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("SPA2").lastSeasonTable[7]);

        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);

        if (competition.matchday == 1)
        {
            competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("SPA1").teamIds);
            competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("SPA2").lastSeasonTable[6]);
            competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("SPA2").lastSeasonTable[7]);
        }


        if (competition.remainingTeams.Count > 1 && competition.matchday != 5) // Should not generate match after first leg of semi final
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, (competition.remainingTeams.Count == 4) ? "2legsAway" : "");

        competition.lastLegWeek = PlayerPrefs.GetInt("Week");
    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("SpaCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("SpaCup", handleAfterMatchday);
    }
}
