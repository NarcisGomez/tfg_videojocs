using UnityEngine;
using TMPro;

public class FinalStats : MonoBehaviour
{
    SongStats songStats;
    [SerializeField] TMP_Text username;
    [SerializeField] TMP_Text songTitle;
    [SerializeField] TMP_Text totalPlayed;
    [SerializeField] TMP_Text accuracy;
    [SerializeField] TMP_Text failed;

    // Start is called before the first frame update
    void Start()
    {
        songStats = GameManager.GetInstance().GetStats();
        LoadView();
    }

    private void LoadView()
    {
        totalPlayed.text = (songStats.totalNotesPlayed).ToString() + " %";
        accuracy.text = (songStats.hitNotes / songStats.totalNotesPlayed * 100).ToString() + " %";
        failed.text = (songStats.missedNotes / songStats.totalNotesPlayed * 100).ToString() + " %";
        songTitle.text = GameManager.GetInstance().GetSong();
        username.text = GameManager.GetInstance().GetUser();
    }
}
