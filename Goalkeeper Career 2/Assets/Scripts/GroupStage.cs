using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupStage
{
    public List<List<int>> groups = new List<List<int>>();

    public void init(Competition competition)
    {
        //Todo proper group generation
        for (int i = 0; i < competition.teamIds.Count; i += 4)
        {
            groups.Add(
                new List<int>(){
                    competition.teamIds[i],
                    competition.teamIds[i+1],
                    competition.teamIds[i+2],
                    competition.teamIds[i+3],
            });
        }

        foreach(var group in groups)
        {
            competition.generateLeagueFrom(group);
        }
    }

}
