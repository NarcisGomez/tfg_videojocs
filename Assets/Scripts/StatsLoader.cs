using UnityEngine;
using TMPro;

public class StatsLoader : MonoBehaviour
{
    UserStats userStats;
    [SerializeField] UserManager userManager;
    [SerializeField] TMP_Text username;
    [SerializeField] GameObject statsContainer;
    [SerializeField] Transform listPanel;

    public void LoadStats()
    {
        userStats = userManager.GetUserStats();
        username.text = userStats.id;

        foreach (Transform c in listPanel.transform)
        {
            Destroy(c.gameObject);
        }
        for (int i = 0; i < userStats.songs.Count; i++)
        {
            GameObject item = Instantiate(statsContainer);
            item.transform.SetParent(listPanel);
            TMP_Text[] textArray = item.GetComponentsInChildren<TMP_Text>();
            textArray[0].text = userStats.songs[i];
            textArray[1].text = userStats.best[i].ToString();
            textArray[3].text = userStats.tried[i].ToString();
            textArray[4].text = userStats.completed[i].ToString();
        }
    }
}
