using UnityEngine;
using SimpleGraphQL;
using System.Net;
using System;
using System.IO;

public class DownloadHandler : MonoBehaviour
{
    private string song;
    [SerializeField] GameObject blocker;

    public void SetSong(string name)
    {
        song = name;
    }
    public void DownloadSong()
    {
        blocker.SetActive(true);
        getUrlQuery();
    }

    private async void getUrlQuery()
    {
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = $"query {{getSong(title: \"{song}\")}}"
        };
        var response = await client.Send(request);
        GetSong url = processData(response);
        WebClient web = new WebClient();
        web.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
        web.DownloadFileAsync(new Uri(url.getSong), $"{Directory.GetCurrentDirectory()}/Assets/MidiPlayer/Resources/MidiDB/{song}.midi");

    }

    private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            blocker.SetActive(false);
        }
    }

    GetSong processData(string data)
    {
        GetSongData dt = JsonUtility.FromJson<GetSongData>(data);
        return dt.data;
    }
}
