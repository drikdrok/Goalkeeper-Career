using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("POR2").teamIds);

        for (int i = 0; i < 10; i++) // Remove top 10 teams from POR2
            competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("POR2").lastSeasonTable[i]);

        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);

        if (competition.matchday == 1)
        {
            competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("POR1").teamIds);
            for (int i = 0; i < 10; i++) // Add top 10 teams from POR2
                competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("POR2").lastSeasonTable[i]);
        }


        if (competition.remainingTeams.Count > 1 && competition.matchday != 5)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, (competition.remainingTeams.Count == 4) ? "2legsAway" : "");

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("PorCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("PorCup", handleAfterMatchday);
    }
}
