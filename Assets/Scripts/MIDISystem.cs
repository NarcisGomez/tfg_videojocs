using UnityEngine;
using MidiJack;

public class MIDISystem : MonoBehaviour
{
    [SerializeField] BarDetector detector;
    [SerializeField] BarPlayer player;

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
            case 46://HiHatOpen
            case 42://HiHatClosed
            case 44://HiHatPedal
                if(detector != null)
                {
                    detector.HitNote(note);
                }
                if(player != null)
                {
                    player.HitNote(note);
                }
              break;
        }
    }
}
