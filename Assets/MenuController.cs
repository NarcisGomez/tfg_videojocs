using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiJack;
using MidiPlayerTK;

public class MenuController : MonoBehaviour
{
    [SerializeField] Text header;
    [SerializeField] Text key;
    [SerializeField] Text vel;
    [SerializeField] Image BassDrum;
    [SerializeField] Image Snare;
    [SerializeField] Image Tom1;
    [SerializeField] Image Tom2;
    [SerializeField] Image Tom3;
    [SerializeField] Image Crash;
    [SerializeField] Image Ride;
    [SerializeField] Image HiHat;
    [SerializeField] MidiFilePlayer midiFilePlayer;
    // Start is called before the first frame update
    void Start()
    {
        midiFilePlayer.OnEventNotesMidi.AddListener(NotesToPlay);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += KnobOn;
    }

    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
        MidiMaster.knobDelegate -= KnobOn;
    }

    private void NoteOn(MidiChannel channel, int note, float velocity)
    {
        key.text = note.ToString();
        vel.text = velocity.ToString();
        header.color = Color.green;

        switch (note) { 
            case 36:
                BassDrum.color = Color.green;
                key.text = "BOMBO";
                break;
            case 38:
                Snare.color = Color.green;
                key.text = "CAIXA";

                break;
            case 48:
                Tom1.color = Color.green;
                key.text = "TOM1";

                break;
            case 45:
                Tom2.color = Color.green;
                key.text = "TOM2";

                break;
            case 43:
                Tom3.color = Color.green;
                key.text = "TOM3";

                break;
            case 49:
                Crash.color = Color.green;
                key.text = "CRASH";

                break;
            case 51:
                Ride.color = Color.green;
                key.text = "RIDE";

                break;
            case 46:
                HiHat.color = Color.green;
                key.text = "HIHAT";

                break;
        }
        Debug.Log("NoteOn: " + channel + ", " + note + ", " + velocity);
    }

    private void NoteOff(MidiChannel channel, int note)
    {
        Debug.Log("NoteOff: " + channel + ", " + note);
        header.color = Color.white;

        switch (note)
        {
            case 36:
                BassDrum.color = Color.grey;
                break;
            case 38:
                Snare.color = Color.grey;
                break;
            case 48:
                Tom1.color = Color.grey;
                break;
            case 45:
                Tom2.color = Color.grey;
                break;
            case 43:
                Tom3.color = Color.grey;
                break;
            case 49:
                Crash.color = Color.grey;
                break;
            case 51:
                Ride.color = Color.grey;
                break;
            case 46:
                HiHat.color = Color.grey;
                break;
        }
    }

    private void KnobOn(MidiChannel channel, int knob, float value)
    {
        Debug.Log("Knob: " + channel + ", " + knob + ", " + value);
    }

    public void pene() { Debug.Log("HOLA"); }

    public void NotesToPlay(List<MPTKEvent> mptkEvents)
    {
        Debug.Log("Received " + mptkEvents.Count + " MIDI Events");
        // Loop on each MIDI events
        foreach (MPTKEvent mptkEvent in mptkEvents)
        {
            // Log if event is a note on
            if (mptkEvent.Command == MPTKCommand.NoteOn)
                Debug.Log($"Note on Time:{mptkEvent.RealTime} millisecond  Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration} millisecond  Velocity:{mptkEvent.Velocity}");

            // Uncomment to display all MIDI events
            // Debug.Log(mptkEvent.ToString());
        }
    }
}
