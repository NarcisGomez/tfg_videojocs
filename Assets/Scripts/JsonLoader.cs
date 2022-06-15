using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class JsonLoader : MonoBehaviour
{
    GameManager gameManager;
    int songAmmount;
    [SerializeField] Transform listPanel;
    [SerializeField] Button button;
    [SerializeField] InformationLoader informationLoader;
    [SerializeField] GameObject tempoContainer;
    [SerializeField] Button playButton;
    [SerializeField] Button practiceButton;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        if(UserManager.GetInstance().GetUserStats() != null)
        {
            songAmmount = UserManager.GetInstance().GetUserStats().songs.Count;
        }
        else
        {
            songAmmount = 99;
        }
        LoadList();
    }

    public void LoadList()
    {
        string[] list = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Assets/MidiPlayer/Resources/MidiDB", "*.bytes");
        foreach (Transform c in listPanel.transform)
        {
            Destroy(c.gameObject);
        }
        for(int i = 0; i < list.Length; i++)
        {
            string[] path = list[i].Split('/');
            string file = path[path.Length - 1].Split('.')[0];
            Button b = Instantiate<Button>(button);
            
            b.transform.SetParent(listPanel);
            b.onClick.AddListener(() => { gameManager.SetSong(file); });
            b.onClick.AddListener(() => { informationLoader.ProcessFile(file); });
            b.onClick.AddListener(() => { informationLoader.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { tempoContainer.SetActive(true); });
            b.onClick.AddListener(() => { playButton.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { practiceButton.gameObject.SetActive(true); });
            TMP_Text child = b.GetComponentInChildren<TMP_Text>();
            child.text = file;
            if(i >= songAmmount + 1)
            {
                b.enabled = false;
                TMP_Text buttonText = b.GetComponentInChildren<TMP_Text>();
                Debug.Log(buttonText);
                buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 0.2f);
            }
        }
        //Do additional content here
    }
}
