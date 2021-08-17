using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {
        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
        {
            competition.remainingTeams.Add(match.winnerId);
        }


        if (competition.remainingTeams.Count > 1)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, "");

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("GenericCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("GenericCup", handleAfterMatchday);
    }
}
