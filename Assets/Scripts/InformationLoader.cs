using UnityEngine;
using TMPro;
using SimpleGraphQL;

public class InformationLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text songTitle;
    [SerializeField] private TMP_Text songTempo;
    [SerializeField] private TMP_Text songSections;
    [SerializeField] private TMP_Text songLoops;

    private void LoadInformation(SongInfo file)
    {
        GameManager.GetInstance().SetSongInfo(file);
        songSections.text = "";
        songLoops.text = "";
        songTitle.text = file.title;
        songTempo.text = file.tempo;

        for (int i = 0; i < file.sections.Count; i++)
        {
            songSections.text += file.sections[i].name + "\n";
            songLoops.text += file.sections[i].loops + "\n";
        }
    }

    public async void ProcessQuery(string name)
    {
        string query = $"query {{getSongInfo(title: \"{name}\"){{\nband\ntitle\ntempo\ndrumChannel\nsections {{\nname\nloops\n}}\n}}\n}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        var response = await client.Send(request);
        GetSongInfo songInfo = processData(response);
        LoadInformation(songInfo.getSongInfo);
     }

    private GetSongInfo processData(string data)
    {
        GetSongInfoData dt = JsonUtility.FromJson<GetSongInfoData>(data);
        return dt.data;
    }

    public void ProcessFile(string name)
    {
        Debug.Log(name);
        TextAsset item = Resources.Load<TextAsset>($"JSONFiles/{name}");
        SongInfo file = JsonUtility.FromJson<SongInfo>(item.ToString());
        LoadInformation(file);
    }

}
