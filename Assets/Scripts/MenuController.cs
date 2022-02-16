//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using MidiJack;
//using MidiPlayerTK;

//public class MenuController : MonoBehaviour
//{
//    [SerializeField] Text instrument;
//    [SerializeField] Text midi;
//    [SerializeField] Text instKey;
//    [SerializeField] Text instVel;
//    [SerializeField] Text midiKey;
//    [SerializeField] Text midiVel;
//    [SerializeField] Image BassDrum;
//    [SerializeField] Image Snare;
//    [SerializeField] Image Tom1;
//    [SerializeField] Image Tom2;
//    [SerializeField] Image Tom3;
//    [SerializeField] Image Crash;
//    [SerializeField] Image Ride;
//    [SerializeField] Image HiHat;
//    [SerializeField] MidiFilePlayer midiFilePlayer;
//    // Start is called before the first frame update
//    void Start()
//    {
//        midiFilePlayer.OnEventNotesMidi.AddListener(NotesToPlay);
//    }

//    private void OnEnable()
//    {
//        MidiMaster.noteOnDelegate += NoteOn;
//        MidiMaster.noteOffDelegate += NoteOff;
//        //MidiMaster.knobDelegate += KnobOn;
//    }

//    private void OnDisable()
//    {
//        MidiMaster.noteOnDelegate -= NoteOn;
//        MidiMaster.noteOffDelegate -= NoteOff;
//        //MidiMaster.knobDelegate -= KnobOn;
//    }

//    private void NoteOn(MidiChannel channel, int note, float velocity)
//    {
//        instKey.text = note.ToString();
//        instVel.text = velocity.ToString();
//        instrument.color = Color.green;

//        switch (note) { 
//            case 36:
//                BassDrum.color = Color.green;
//                instKey.text = "BOMBO";
//                break;
//            case 38:
//                Snare.color = Color.green;
//                instKey.text = "CAIXA";

//                break;
//            case 48:
//                Tom1.color = Color.green;
//                instKey.text = "TOM1";

//                break;
//            case 45:
//                Tom2.color = Color.green;
//                instKey.text = "TOM2";

//                break;
//            case 43:
//                Tom3.color = Color.green;
//                instKey.text = "TOM3";

//                break;
//            case 49:
//                Crash.color = Color.green;
//                instKey.text = "CRASH";

//                break;
//            case 51:
//                Ride.color = Color.green;
//                instKey.text = "RIDE";

//                break;
//            case 46:
//                HiHat.color = Color.green;
//                instKey.text = "HIHAT";

//                break;
//        }
//        Debug.Log("NoteOn: " + channel + ", " + note + ", " + velocity);
//    }

//    private void NoteOff(MidiChannel channel, int note)
//    {
//        Debug.Log("NoteOff: " + channel + ", " + note);
//        instrument.color = Color.white;

//        switch (note)
//        {
//            case 36:
//                BassDrum.color = Color.grey;
//                break;
//            case 38:
//                Snare.color = Color.grey;
//                break;
//            case 48:
//                Tom1.color = Color.grey;
//                break;
//            case 45:
//                Tom2.color = Color.grey;
//                break;
//            case 43:
//                Tom3.color = Color.grey;
//                break;
//            case 49:
//                Crash.color = Color.grey;
//                break;
//            case 51:
//                Ride.color = Color.grey;
//                break;
//            case 46:
//                HiHat.color = Color.grey;
//                break;
//        }
//    }

//    //private void KnobOn(MidiChannel channel, int knob, float value)
//    //{
//    //    Debug.Log("Knob: " + channel + ", " + knob + ", " + value);
//    //}

//    public void NotesToPlay(List<MPTKEvent> mptkEvents)
//    {
//        Debug.Log("Received " + mptkEvents.Count + " MIDI Events");
//        foreach (MPTKEvent mptkEvent in mptkEvents)
//        {
//            // Log if event is a note on
//            if (mptkEvent.Command == MPTKCommand.NoteOn)
//                midi.color = Color.green;
//                midiKey.text = mptkEvent.Value.ToString();
//                midiVel.text = mptkEvent.Velocity.ToString();
//                Debug.Log($"Note on Time:{mptkEvent.RealTime} millisecond  Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration} millisecond  Velocity:{mptkEvent.Velocity}");

//            // Uncomment to display all MIDI events
//            // Debug.Log(mptkEvent.ToString());
//        }
//        midi.color = Color.black;
//    }
//}
