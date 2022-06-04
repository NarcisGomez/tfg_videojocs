using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongLoader : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] SectionContainer container;
    [SerializeField] Transform listPanel;
    [SerializeField] TMP_Text songTitle;
    [SerializeField] List<SectionContainer> sectionsList;
    SongInfo song;
    int currentIndex;
    SectionContainer currentItem;
    Vector3 initialPosition;
    bool entranceActive;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        songTitle.text = gameManager.GetSong();
        currentIndex = 0;
        LoadStructure();
        initialPosition = listPanel.transform.position;
        entranceActive = true;
    }

    void Update()
    {
        if (currentIndex != 0) {

            listPanel.transform.position = Vector3.MoveTowards(listPanel.transform.position, new Vector3(initialPosition.x, initialPosition.y + (80 * currentIndex), initialPosition.z), 120 * Time.deltaTime);
        }
    }

    void LoadStructure()
    {
        song = gameManager.GetSongInfo();

        for (int i = 0; i < song.sections.Count; i++)
        {
            SectionContainer item = Instantiate(container);
            item.transform.SetParent(listPanel);
            TMP_Text[] textArray = item.GetComponentsInChildren<TMP_Text>();
            textArray[0].text = song.sections[i].name;
            textArray[1].text = song.sections[i].loops;
            textArray[2].text = song.sections[i].loops;
            sectionsList.Add(item);
        }
        currentItem = sectionsList[currentIndex];
        
    }

    public void WholeBar()
    {
        if (!entranceActive)
        {
            TMP_Text[] textArray = currentItem.GetComponentsInChildren<TMP_Text>();
            int loops = int.Parse(textArray[2].text);
            loops = loops - 1;
            textArray[2].text = loops.ToString();
            if (loops == 0)
            {
                currentItem.SetFade(true);
                currentIndex++;
                if (currentIndex < sectionsList.Count)
                {
                    currentItem = sectionsList[currentIndex];
                    currentItem.SetPump(true);
                }
                else
                {
                    gameManager.EndSong();
                }
                
            }
        }
        if (entranceActive)
        {
            entranceActive = false;
            currentItem.SetPump(true);
        }
    }

    public void Tick()
    {
        currentItem.Tick();
    }
}
