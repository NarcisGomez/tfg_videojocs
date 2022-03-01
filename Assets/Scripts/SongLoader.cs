using UnityEngine;
using TMPro;

public class SongLoader : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject container;
    [SerializeField] Transform listPanel;
    [SerializeField] TMP_Text songTitle;
    private SongFile song;

    void Start()
    {
        gameManager = GameManager.getInstance();
        LoadStructure();
    }

    // Update is called once per frame
    void LoadStructure()
    {
        song = gameManager.GetSong();

        for (int i = 0; i < song.sections.Length; i++)
        {
            GameObject item = Instantiate<GameObject>(container);
            item.transform.SetParent(listPanel);
            TMP_Text[] textArray = item.GetComponentsInChildren<TMP_Text>();
            textArray[0].text = song.sections[i];
            textArray[1].text = song.loops[i].ToString();
        }
    }
}
