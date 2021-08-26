using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("SCO2").teamIds);

        //Add last season's  nr 9 and 10 from SCO1
        competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("SCO1").lastSeasonTable[8]);
        competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("SCO1").lastSeasonTable[9]);

        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);

        if (competition.matchday == 1) // Add remaning SCO 1 teams after matchday 1
            for (int i = 0; i < 12; i++)
                if (i != 8 && i != 9)
                    competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("SCO1").lastSeasonTable[i]);


        if (competition.remainingTeams.Count > 1)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, "");

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("ScoCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("ScoCup", handleAfterMatchday);
    }
}
