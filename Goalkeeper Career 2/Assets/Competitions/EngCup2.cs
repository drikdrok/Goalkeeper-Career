using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngCup2 : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.remainingTeams = new List<int>();
        //competition.generateGenericCup(1, "replays");
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("ENG2").teamIds); // ADD ENG2
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("ENG3").teamIds); // ADD ENG3	
        competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("ENG4").teamIds); // ADD ENG4

        //Remove top 2 relegators from prem
        competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("ENG1").lastSeasonTable[17]);
        competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("ENG1").lastSeasonTable[18]);


        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        competition.remainingTeams = new List<int>();
        foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")])
            competition.remainingTeams.Add(match.winnerId);


        if (competition.matchday == 1)
        {
            //Add remaining from ENG2
            competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("ENG1").lastSeasonTable[17]);
            competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("ENG1").lastSeasonTable[18]);
            //Add ENG1
            competition.remainingTeams.AddRange(CompetitionManager.Instance.getCompetition("ENG1").teamIds);

            //Remove top 7 from ENG1 from last season
            for (int i = 0; i < 7; i++)
            {
                competition.remainingTeams.Remove(CompetitionManager.Instance.getCompetition("ENG1").lastSeasonTable[i]);
            }

        }else if (competition.matchday == 2)
        {
            //Add top 7 ENG1 from last season
            for (int i = 0; i < 7; i++)
            {
                competition.remainingTeams.Add(CompetitionManager.Instance.getCompetition("ENG1").lastSeasonTable[i]);
            }
        }



        if (competition.remainingTeams.Count > 1 && competition.matchday != 7)
            competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, (competition.remainingTeams.Count == 4) ? "2legs" : "");
        
        competition.lastLegWeek = PlayerPrefs.GetInt("Week");
    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("EngCup2", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("EngCup2", handleAfterMatchday);
    }
}
