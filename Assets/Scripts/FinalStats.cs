using UnityEngine;
using TMPro;
using SimpleGraphQL;

public class FinalStats : MonoBehaviour
{
    GameManager gameManager;
    SongStats songStats;
    string user;
    [SerializeField] TMP_Text username;
    [SerializeField] TMP_Text songTitle;
    [SerializeField] TMP_Text totalPlayed;
    [SerializeField] TMP_Text accuracy;
    [SerializeField] TMP_Text failed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        songStats = gameManager.GetStats();
        LoadView();
    }

    private void LoadView()
    {
        int finalAccuracy = songStats.hitNotes / songStats.totalNotesPlayed * 100;
        totalPlayed.text = (songStats.totalNotesPlayed).ToString() + " %";
        accuracy.text = finalAccuracy.ToString() + " %";
        failed.text = (songStats.missedNotes / songStats.totalNotesPlayed * 100).ToString() + " %";
        songTitle.text = gameManager.GetSong();
        if (gameManager.GetUser() != null)
        {
            user = gameManager.GetUser();
            username.text = user;
            SaveStats(finalAccuracy, gameManager.GetSong(), 1, finalAccuracy == 100);
        }
        else
        {
            user = "Guest";
            username.text = user;
        }

    }

    private async void SaveStats(int best, string song, int tried, bool completed)
    {
        string query = $"mutation {{ saveUserStats(stats: {{id: \"{user}\", best: {best}, song: \"{song}\", tried: {tried}, completed: {completed}}})}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        await client.Send(request);
    }
}
