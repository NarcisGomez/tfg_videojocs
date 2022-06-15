using UnityEngine;
using TMPro;
using MidiPlayerTK;
using System.Collections.Generic;

public class PracticeManager : MonoBehaviour
{
    private GameManager gameManager;
    private int drumChannel;
    private bool paused;
    private List<NoteBehavior> notesOnDisplay;
    [SerializeField] TMP_Text songTitle;
    [SerializeField] Transform finishPoint;
    [SerializeField] GameObject quarter;
    [SerializeField] MidiFilePlayer midiLoader;
    [SerializeField] List<Transform> startingPoint;
    //List order
    //0 -> ride & crash
    //1 -> hihat top
    //2 -> tom 1
    //3 -> tom 2
    //4 -> snare
    //5 -> tom 3
    //6 -> bass drum


    void Start()
    {
        notesOnDisplay = new List<NoteBehavior>();
        gameManager = GameManager.GetInstance();
        drumChannel = int.Parse(gameManager.GetSongInfo().drumChannel);
        songTitle.text = gameManager.GetSong();
        paused = false;
        midiLoader.OnEventNotesMidi.AddListener(NotesToPlay);
        midiLoader.MPTK_MidiName = gameManager.GetSong();
        midiLoader.MPTK_Volume = 0;
        midiLoader.MPTK_StartPlayAtFirstNote = true;
        midiLoader.MPTK_Play();
        NoteBehavior phantom = Instantiate(quarter, startingPoint[6]).GetComponent<NoteBehavior>();
        phantom.SetImage("phantom");
        phantom.SetPosition(new Vector3(finishPoint.position.x, startingPoint[6].position.y, finishPoint.position.z));
        phantom.SetId(0);
    }

    void NotesToPlay(List<MPTKEvent> mptkEvents)
    {
        foreach (MPTKEvent mptkEvent in mptkEvents)
        {
            if (mptkEvent.Command == MPTKCommand.NoteOn && mptkEvent.Channel == drumChannel)
            {
                NoteBehavior note = null;
                switch (mptkEvent.Value)
                {
                    case 35://Bass
                    case 36:
                        note = Instantiate(quarter, startingPoint[6]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[6].position.y, finishPoint.position.z));
                        note.SetId(36);
                        break;
                    case 40:
                    case 39:
                    case 38://Snare
                        note = Instantiate(quarter, startingPoint[4]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[4].position.y, finishPoint.position.z));
                        note.SetId(38);
                        break;
                    case 47:
                    case 50:
                    case 48://Tom1
                        note = Instantiate(quarter, startingPoint[2]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[2].position.y, finishPoint.position.z));
                        note.SetId(48);
                        break;
                    case 45://Tom2
                        note = Instantiate(quarter, startingPoint[3]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[3].position.y, finishPoint.position.z));
                        note.SetId(45);
                        break;
                    case 41:
                    case 43://Tom3
                        note = Instantiate(quarter, startingPoint[5]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[5].position.y, finishPoint.position.z));
                        note.SetId(43);
                        break;
                    case 57:
                    case 49://Crash
                        note = Instantiate(quarter, startingPoint[0]).GetComponent<NoteBehavior>();
                        note.SetImage("star");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[0].position.y, finishPoint.position.z));
                        note.SetId(49);
                        break;
                    case 59:
                    case 51://Ride
                        note = Instantiate(quarter, startingPoint[0]).GetComponent<NoteBehavior>();
                        note.SetImage("cross");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[0].position.y, finishPoint.position.z));
                        note.SetId(51);
                        break;
                    case 44://HiHat Closed
                        note = Instantiate(quarter, startingPoint[1]).GetComponent<NoteBehavior>();
                        note.SetImage("cross");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[1].position.y, finishPoint.position.z));
                        note.SetId(44);
                        break;
                    case 46://HiHat Open
                        note = Instantiate(quarter, startingPoint[1]).GetComponent<NoteBehavior>();
                        note.SetImage("circle");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[1].position.y, finishPoint.position.z));
                        note.SetId(46);
                        break;
                    case 42://HiHat Foot
                        note = Instantiate(quarter, startingPoint[7]).GetComponent<NoteBehavior>();
                        note.SetImage("cross");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[7].position.y, finishPoint.position.z));
                        note.SetId(42);
                        break;
                    default:
                        Debug.LogError($"No note for this number: {mptkEvent.Value}");
                        break;
                }
                if(note != null)
                {
                    notesOnDisplay.Add(note);
                } 
            }                
        }
    }

    public void TogglePause()
    {
        if (!paused)
        {
            midiLoader.MPTK_Pause();
            notesOnDisplay.ForEach(note => note.SetStop(true));
        }
        else
        {
            midiLoader.MPTK_UnPause();
            notesOnDisplay.ForEach(note => note.SetStop(false));
        }
        paused = !paused;
    }
    public void AddNote(NoteBehavior note)
    {
        notesOnDisplay.Add(note);
    }

    public void RemoveNote(NoteBehavior note)
    {
        notesOnDisplay.Remove(note);
    }
}
