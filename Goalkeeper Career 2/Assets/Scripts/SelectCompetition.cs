using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCompetition : MonoBehaviour
{
    public Transform leaguesContent;
    public Transform continentalContent;
    public Transform cupsContent;

    public GameObject buttonPrefab;

    void Start()
    {

        List<Competition> competitions = CompetitionManager.Instance.competitions.OrderBy(x => x.name).ToList();

        foreach (var competition in competitions)
        {

            GameObject button = Instantiate(buttonPrefab);
            if (competition.type == "euro")
                button.transform.SetParent(continentalContent);
            else if (competition.type == "cup")
                button.transform.SetParent(cupsContent);
            else
                button.transform.SetParent(leaguesContent);

            button.GetComponentInChildren<Text>().text = competition.name;
            button.GetComponent<Button>().onClick.AddListener(
                delegate {
                    CompetitionManager.Instance.currentCompetition = competition;
                    SceneManager.LoadScene("CompetitionScreen");
                });
        } 
    }


}
