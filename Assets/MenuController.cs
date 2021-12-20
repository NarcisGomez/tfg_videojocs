using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiJack;

public class MenuController : MonoBehaviour
{
    [SerializeField] Text header;
    [SerializeField] Text key;
    [SerializeField] Text vel;
    // Start is called before the first frame update
    void Start()
    {
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

        Debug.Log("NoteOn: " + channel + ", " + note + ", " + velocity);
    }

    private void NoteOff(MidiChannel channel, int note)
    {
        Debug.Log("NoteOff: " + channel + ", " + note);
        header.color = Color.white;
    }

    private void KnobOn(MidiChannel channel, int knob, float value)
    {
        Debug.Log("Knob: " + channel + ", " + knob + ", " + value);
    }
}
