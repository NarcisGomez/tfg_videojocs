using UnityEngine;
using SimpleGraphQL;

public class StatisticsManager : MonoBehaviour
{
    private string user;

    void Start()
    {
        user = UserManager.GetInstance().GetUser();
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
}
