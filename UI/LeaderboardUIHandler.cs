using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;

    SetLeaderboardItem[] setLeaderboardItems;

    private void Awake()
    {
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        setLeaderboardItems = new SetLeaderboardItem[carLapCounterArray.Length];

        for(int i = 0; i < carLapCounterArray.Length; i++)
        {
            GameObject leaderboardInfoGameObject = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);

            setLeaderboardItems[i] = leaderboardInfoGameObject.GetComponent<SetLeaderboardItem>();

            setLeaderboardItems[i].SetPositionText($"{i + 1}.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateList(List<CarLapCounter> lapCounterList)
    {
        for(int i = 0; i < lapCounterList.Count;i++)
        {
            setLeaderboardItems[i].SetDriverName(lapCounterList[i].gameObject.name);
        }
    }
}
