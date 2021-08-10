using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLeague : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.generateGenericLeague();
    }

    public static void handleAfterMatchday(Competition competition)
    {

    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("GenericLeague", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("GenericLeague", handleAfterMatchday);
    }
}
