using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleGraphQL;

public class DownloadLoader : MonoBehaviour
{
    [SerializeField] Transform listPanel;
    [SerializeField] Button button;
    [SerializeField] InformationLoader informationLoader;
    [SerializeField] Button downloadButton;
    [SerializeField] DownloadHandler handler;

    void Start()
    {
        songListQuery();
    }

    void LoadList(GetSongInfoList list)
    {
        foreach (string item in list.getSongInfoList)
        {
            Button b = Instantiate(button);
            b.transform.SetParent(listPanel);
            b.onClick.AddListener(() => { downloadButton.gameObject.SetActive(true); });
            //b.onClick.AddListener(() => { informationLoader.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { handler.SetSong(item); });
            TMP_Text child = b.GetComponentInChildren<TMP_Text>();
            child.text = item;

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
