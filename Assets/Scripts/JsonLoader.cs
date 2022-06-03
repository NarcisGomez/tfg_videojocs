using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JsonLoader : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Transform listPanel;
    [SerializeField] Button button;
    [SerializeField] InformationLoader informationLoader;
    [SerializeField] GameObject tempoContainer;
    [SerializeField] Button playButton;
    [SerializeField] Button practiceButton;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        LoadList();
    }

    public void LoadList()
    {
        TextAsset[] list = Resources.LoadAll<TextAsset>("JSONFiles");
        foreach (TextAsset item in list)
        {
            SongFile file = JsonUtility.FromJson<SongFile>(item.ToString());
            Button b = Instantiate<Button>(button);
            b.transform.SetParent(listPanel);
            b.onClick.AddListener(() => { gameManager.SetSong(file); });
            b.onClick.AddListener(() => { informationLoader.LoadInformation(file); });
            b.onClick.AddListener(() => { informationLoader.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { tempoContainer.SetActive(true); });
            b.onClick.AddListener(() => { playButton.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { practiceButton.gameObject.SetActive(true); });
            TMP_Text child = b.GetComponentInChildren<TMP_Text>();
            child.text = file.GetSongName();
            
        }
    }
}
