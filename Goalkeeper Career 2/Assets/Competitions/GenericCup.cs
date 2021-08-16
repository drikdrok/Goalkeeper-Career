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
            if (match[2] > match[3]) // Winner of match (Todo: allow the game to go to penalties)
                competition.remainingTeams.Add(match[0]);
            else
                competition.remainingTeams.Add(match[1]);
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
