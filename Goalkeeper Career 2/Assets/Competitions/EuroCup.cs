using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuroCup : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.groupStage = new GroupStage();
        competition.groupStage.init(competition);
    }

    public static void handleAfterMatchday(Competition competition)
    {

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("EuroCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("EuroCup", handleAfterMatchday);
    }
}

