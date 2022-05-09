using UnityEngine;
using MidiJack;
using MidiPlayerTK;

public class MIDISystem : MonoBehaviour
{
    [SerializeField] BarDetector detector;
    [SerializeField] MidiFilePlayer midiFilePlayer;

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
            case 36://Bass
            case 38://Snare
            case 48://Tom1
            case 45://Tom2
            case 43://Tom3
            case 49://Crash
            case 51://Ride
            case 46://HiHat
                Debug.Log(note);
                detector.HitNote(note);
                break;
        }
    }

    public void StartPlayingMidi()
    {
        midiFilePlayer.MPTK_Play();
    }

    public void StopPlayingMidi()
    {
        midiFilePlayer.MPTK_Stop();
    }
}
