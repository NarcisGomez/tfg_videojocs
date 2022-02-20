using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class SongFile
{
    public string title;
    public int tempo;
    public string[] sections;
    public int[] loops;

    public SongFile(string title, int tempo, string[] sections, int[] loops)
    {
        this.title = title;
        this.tempo = tempo;
        this.sections = sections;
        this.loops = loops;
    }
}

public class JsonLoader : MonoBehaviour
{
    [SerializeField] Transform listPanel;
    [SerializeField] Button button;
    [SerializeField] GameManager gameManager;
    [SerializeField] InformationLoader informationLoader;
    [SerializeField] GameObject tempoContainer;

    void Awake()
    {
        LoadList();
    }

    void LoadList()
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
            TMP_Text child = b.GetComponentInChildren<TMP_Text>();
            child.text = file.title;
            
        }
    }
}
