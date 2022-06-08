using UnityEngine;
using SimpleGraphQL;

public class StatisticsManager : MonoBehaviour
{
    private string user;
    private int totalNotesPlayed;
    private int hitNotes;
    private int missedNotes;
    [SerializeField] BarPerformance barPerformance;

    void Start()
    {
        user = UserManager.GetInstance().GetUser();
        totalNotesPlayed = 0;
        hitNotes = 0;
        missedNotes = 0;
    }

    public async void SaveStats(int best, string song, int tried, bool completed)
    {
        string query = $"mutation {{ saveUserStats(stats: {{id: \"{user}\", best: {best}, song: \"{song}\", tried: {tried}, completed: {completed}}})}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        await client.Send(request);
    }

    public void AddHitNote()
    {
        hitNotes++;
        barPerformance.Plus();
    }

    public void AddMissedNote()
    {
        missedNotes++;
        barPerformance.Minus();
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
