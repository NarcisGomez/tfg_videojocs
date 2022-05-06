using UnityEngine;
using MidiJack;

public class MIDISystem : MonoBehaviour
{
    [SerializeField] BarDetector detector;

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
    }

    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
    }

    private void NoteOn(MidiChannel channel, int note, float velocity)
    {
        switch (note)
        {
            case 36:
            case 38:
            case 48:
            case 45:
            case 43:
            case 49:
            case 51:
            case 46:
                Debug.Log(note);
                detector.HitNote(note);
                break;
        }
    }
}
