using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text songTitle;
    [SerializeField] private TMP_Text songTempo;
    [SerializeField] private TMP_Text songSections;
    [SerializeField] private TMP_Text songLoops;

    public void LoadInformation(SongFile file)
    {
        songSections.text = "";
        songLoops.text = "";
        songTitle.text = file.title;
        songTempo.text = file.tempo.ToString();

        for (int i = 0; i < file.sections.Length; i++)
        {
            songSections.text += file.sections[i] + "\n";
            songLoops.text += file.loops[i] + "\n";
        }
    }
}
