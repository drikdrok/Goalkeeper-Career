using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EngCup1 : MonoBehaviour
{
    public static void init(Competition competition)
    {
        competition.generateGenericCup(1, "");
    }

    public static void handleAfterMatchday(Competition competition)
    {

        if (competition.matchday % 2 == 0) // Even matchdays are after replay rounds
        {
            competition.remainingTeams = new List<int>();
            foreach (var match in competition.matches[PlayerPrefs.GetInt("Week")].Concat(competition.matches[PlayerPrefs.GetInt("Week")-1]).ToList())
            {
                competition.remainingTeams.Add(match.winnerId);
            }
            
            
            if (competition.matchday == 4) // Teams from ENG 1 & Eng 2 enter the competition
            {
                foreach (int team in CompetitionManager.Instance.competitions[3].teamIds)
                {
                    competition.remainingTeams.Add(team);
                    competition.stats.Add(team, new TeamStats());
                } 

                foreach (int team in CompetitionManager.Instance.competitions[4].teamIds)
                {
                    competition.remainingTeams.Add(team);
                    competition.stats.Add(team, new TeamStats());
                }
                //Todo proper seeding. The top 8 from previous season of ENG1 should start in the next round. Currently I'm hard coding them becuase i'm lazy
                competition.remainingTeams.Remove(29);
                competition.remainingTeams.Remove(34);
                competition.remainingTeams.Remove(35);
                competition.remainingTeams.Remove(36);
                competition.remainingTeams.Remove(24);
                competition.remainingTeams.Remove(40);
                competition.remainingTeams.Remove(33);
                competition.remainingTeams.Remove(31);
            }
            else if (competition.matchday == 6)
            {
                competition.remainingTeams.Add(29);
                competition.remainingTeams.Add(34);
                competition.remainingTeams.Add(35);
                competition.remainingTeams.Add(36);
                competition.remainingTeams.Add(24);
                competition.remainingTeams.Add(40);
                competition.remainingTeams.Add(33);
                competition.remainingTeams.Add(31);
            }


            if (competition.remainingTeams.Count > 1)
                competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, (competition.matchday > 6) ? "" : "replays");

        }
    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("EngCup1", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("EngCup1", handleAfterMatchday);
    }
}
