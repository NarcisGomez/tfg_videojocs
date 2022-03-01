using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiPlayerTK;
using TMPro;

public class DirectoryLoader : MonoBehaviour
{
    [SerializeField] MidiFilePlayer midiPlayer;
    [SerializeField] Transform listPanel;
    [SerializeField] Button button;

    void Awake()
    {
        LoadMidi();
    }

    void LoadMidi()
    {
        List<MPTKListItem> list = MidiPlayerGlobal.MPTK_ListMidi;
        foreach(MPTKListItem item in list)
        {
            Button b = Instantiate<Button>(button);
            b.transform.SetParent(listPanel);
            TMP_Text child = b.GetComponentInChildren<TMP_Text>();
            child.text = item.Label;
            b.onClick.AddListener(() =>
            {
                midiPlayer.MPTK_Stop();
                midiPlayer.MPTK_MidiName = item.Label;

            });
        }
    }
}
