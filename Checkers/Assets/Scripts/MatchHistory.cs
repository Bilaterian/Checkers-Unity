using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchHistory : MonoBehaviour
{
    [SerializeField]
    private GameObject singleMatchHistory;

    void Start()
    {
        var matches = DataSaver.LoadList<GameStats>("matches");

        matches.Sort(delegate (GameStats x, GameStats y)
        {
            return y.gameEndTime.CompareTo(x.gameEndTime);
        });
        foreach (var match in matches)
        {
            var matchHistoryObject = Instantiate(singleMatchHistory) as GameObject;

            matchHistoryObject.GetComponent<SingleMatchHistory>().SetText(match.wonPlayerName, match.lostPlayerName);

            matchHistoryObject.SetActive(true);
            matchHistoryObject.transform.SetParent(singleMatchHistory.transform.parent, false);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
