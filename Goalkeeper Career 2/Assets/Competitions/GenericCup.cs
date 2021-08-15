using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.generateGenericCup(1, false);
    }

    public static void handleAfterMatchday(Competition competition)
    {
        competition.remainingTeams = new List<int>();
       // Debug.Log(competition.matches[PlayerPrefs.GetInt("Week")].Count + " matches were played");
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
        {
           // Debug.Log(match[0] + ", " + match[1] + ", " + match[2] + ", " + match[3]);
            if (match[2] > match[3]) // Winner of match (Todo: allow the game to go to penalties)
                competition.remainingTeams.Add(match[0]);
            else
                competition.remainingTeams.Add(match[1]);
        }

       // Debug.Log("There are " + competition.remainingTeams.Count + "teams remaining!");

        if (competition.remainingTeams.Count > 1)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, false);

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("GenericCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("GenericCup", handleAfterMatchday);
    }
}
