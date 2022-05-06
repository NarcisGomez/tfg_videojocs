using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SectionLoader : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Transform loopPanel;
    [SerializeField] Button button;
    [SerializeField] Button playButton;
    SongFile song;

    void Start()
    {
        gameManager = GameManager.getInstance();
        LoadList();
    }

    void LoadList()
    {
        song = gameManager.GetSong();

        for (int i = 0; i < song.sections.Length; i++)
        {
            Button b = Instantiate<Button>(button);
            b.transform.SetParent(loopPanel);
            TMP_Text[] textArray = b.GetComponentsInChildren<TMP_Text>();
            textArray[0].text = song.sections[i];
            b.onClick.AddListener(() => { playButton.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { gameManager.SetSection(textArray[0].text); });
        }
    }
}
