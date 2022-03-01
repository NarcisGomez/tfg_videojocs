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

        foreach (string s in file.sections)
        {
            songSections.text += s + "\n";
            songLoops.text += s + "\n";
        }
    }
}
