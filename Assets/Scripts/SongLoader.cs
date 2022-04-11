using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class SongLoader : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] SectionContainer container;
    [SerializeField] Transform listPanel;
    [SerializeField] TMP_Text songTitle;
    [SerializeField] List<SectionContainer> sectionsList;
    SongFile song;
    int currentIndex;
    SectionContainer currentItem;

    void Start()
    {
        gameManager = GameManager.getInstance();
        currentIndex = 0;
        LoadStructure();
    }

    // Update is called once per frame
    void LoadStructure()
    {
        song = gameManager.GetSong();

        for (int i = 0; i < song.sections.Length; i++)
        {
            SectionContainer item = Instantiate<SectionContainer>(container);
            item.transform.SetParent(listPanel);
            TMP_Text[] textArray = item.GetComponentsInChildren<TMP_Text>();
            textArray[0].text = song.sections[i];
            textArray[1].text = song.loops[i].ToString();
            textArray[2].text = song.loops[i].ToString();
            sectionsList.Add(item);
        }
        currentItem = sectionsList[currentIndex];
        currentItem.SetPump(true);
    }
    public void WholeBar()
    {
        TMP_Text[] textArray = currentItem.GetComponentsInChildren<TMP_Text>();
        int loops = int.Parse(textArray[2].text);
        loops = loops - 1;
        textArray[2].text = loops.ToString();
        if (loops == 0)
        {
            currentItem.SetFade(true);
            currentIndex++;
            currentItem = sectionsList[currentIndex];
            currentItem.SetPump(true);
        }
    }
}
