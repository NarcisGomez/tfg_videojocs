using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    private int totalNotesPlayed;
    private int hitNotes;
    private int missedNotes;

    void Start()
    {
        totalNotesPlayed = 0;
        hitNotes = 0;
        missedNotes = 0;
    }

    public void AddHitNote()
    {
        hitNotes++;
    }

    public void AddMissedNote()
    {
        missedNotes++;
    }

    public void AddPlayedNote()
    {
        totalNotesPlayed++;
    }

    public void EndGame()
    {
        GameManager gameManager = GameManager.GetInstance();
        gameManager.SetStats(totalNotesPlayed, hitNotes, missedNotes);
        gameManager.EndSong();
    }
}
