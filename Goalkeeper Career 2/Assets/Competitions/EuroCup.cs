using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (competition.matchday == 6) //Group stage finished
        {
            List<int> winners = new List<int>();
            List<int> seconds = new List<int>();
            foreach (var group in competition.groupStage.groups)
            {
                List<int> g = group.OrderBy(o => competition.stats[o].points).ThenBy(o => (competition.stats[o].GF - competition.stats[o].GA)).ThenBy(o => competition.stats[o].GF).ToList();
                seconds.Add(g[2]);
                winners.Add(g[3]);
            }

            winners = winners.OrderBy(x => Random.value).ToList(); //Shuffle
            seconds = seconds.OrderBy(x => Random.value).ToList();
            int count = winners.Count;
            for (int i = 0; i < count; i++) // Generate matches
            {
                Match match1 = new Match(winners[0], seconds[0]);

                Match match2 = new Match(seconds[0], winners[0]);
                match2.twoLegged = true;

                competition.matches[PlayerPrefs.GetInt("Week") + 1].Add(match1);
                competition.matches[PlayerPrefs.GetInt("Week") + 2].Add(match2);

               // Debug.Log("CL Match: " + match1[0] + ", " + match1[1]);

                winners.RemoveAt(0);
                seconds.RemoveAt(0);
            }
        
        } else if (competition.matchday > 6)
        {
            if (competition.matchday % 2 == 0) //Even matchdays are after second leg
            {
                competition.remainingTeams = new List<int>();
                foreach(var leg2 in competition.matches[PlayerPrefs.GetInt("Week")])
                {
                    foreach(var leg1 in competition.matches[competition.lastLegWeek])
                    {
                        if (leg1.homeTeamId == leg2.awayTeamId || leg1.awayTeamId == leg2.homeTeamId) // Found first leg
                        {
                            if (leg1.homeScore + leg2.awayScore > leg1.awayScore + leg2.homeScore) // Winner over 2 legs
                            { 
                                competition.remainingTeams.Add(leg1.homeTeamId);
                                Debug.Log("Leg Winner: " + leg1.homeTeamId);
                            }
                            else
                            {
                                competition.remainingTeams.Add(leg1.awayTeamId);
                                Debug.Log("Leg Winner: " + leg1.awayTeamId);
                            }
                            break;
                        }
                    }
                }

                competition.generateGenericCup(PlayerPrefs.GetInt("Week") + 1, (competition.remainingTeams.Count == 2) ? "" : "2legs");

            }
            else
            {
                competition.lastLegWeek = PlayerPrefs.GetInt("Week");
            }
        }
    }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        CompetitionManager.Instance.initFunctions.Add("EuroCup", init);
        CompetitionManager.Instance.handleAfterMatchday.Add("EuroCup", handleAfterMatchday);
    }
}

