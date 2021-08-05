using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PostMatchScript : MonoBehaviour
{

    public Transform scrollView;
    public GameObject fixtureRow;

    void Start()
    {
        CompetitionManager.Instance.currentCompetition.simulateWeek();   

    }

}


