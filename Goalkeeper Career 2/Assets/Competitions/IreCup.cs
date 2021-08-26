using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IreCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("IRE2").teamIds);

        //Remove 3rd and 4th from IRE2
        competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("IRE2").lastSeasonTable[2]);
        competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("IRE2").lastSeasonTable[3]);

        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);

        if (competition.matchday == 1) // Add remaining teams
        {
            competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("IRE1").teamIds);
            competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("IRE2").lastSeasonTable[2]);
            competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("IRE2").lastSeasonTable[3]);
        }


        if (competition.remainingTeams.Count > 1)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, "");

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("IreCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("IreCup", handleAfterMatchday);
    }
}
