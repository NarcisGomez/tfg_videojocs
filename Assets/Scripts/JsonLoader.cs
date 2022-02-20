using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class JsonLoader : MonoBehaviour
{
    [Serializable]
    private class SongFile
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

    [SerializeField] Transform listPanel;
    [SerializeField] Button button;

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
            Debug.Log(file.title);
            Button b = Instantiate<Button>(button);
            b.transform.SetParent(listPanel);
            TMP_Text child = b.GetComponentInChildren<TMP_Text>();
            child.text = file.title;
        }
    }
}
