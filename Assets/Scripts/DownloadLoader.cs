using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleGraphQL;
using System.Collections.Generic;
using System.IO;

public class DownloadLoader : MonoBehaviour
{
    private List<string> localSongs;
    [SerializeField] Transform listPanel;
    [SerializeField] Button button;
    [SerializeField] InformationLoader informationLoader;
    [SerializeField] Button downloadButton;
    [SerializeField] DownloadHandler handler;

    void Start()
    {
        localSongs = new List<string>();
        songListQuery();
    }

    void LoadList(GetSongInfoList list)
    {
        string[] songs = Directory.GetFiles($"{Application.persistentDataPath}/MidiDB", "*.bytes");
        foreach (string s in songs)
        {
            string[] path = s.Split('/');
            string name = path[path.Length - 1].Split('.')[0];
            localSongs.Add(name);
        }
        foreach (string item in list.getSongInfoList)
        {
            if (!localSongs.Contains(item))
            {
                Button b = Instantiate(button);
                b.transform.SetParent(listPanel);
                b.onClick.AddListener(() => { downloadButton.gameObject.SetActive(true); });
                b.onClick.AddListener(() => { informationLoader.gameObject.SetActive(true); });
                b.onClick.AddListener(() => { handler.SetSong(item); });
                b.onClick.AddListener(() => { informationLoader.ProcessQuery(item); });
                TMP_Text child = b.GetComponentInChildren<TMP_Text>();
                child.text = item;
            }
        }
    }

    private async void songListQuery()
    {
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = "query {getSongInfoList}"
        };
        var response = await client.Send(request);
        GetSongInfoList sl = processData(response);
        LoadList(sl);
    }

    GetSongInfoList processData(string data)
    {
        GetSongInfoListData dt = JsonUtility.FromJson<GetSongInfoListData>(data);
        return dt.data;
    }

}
