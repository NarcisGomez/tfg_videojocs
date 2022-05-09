using UnityEngine;
using TMPro;
using MidiPlayerTK;
using System.Collections.Generic;

public class PracticeManager : MonoBehaviour
{
    private GameManager gameManager;
    private int drumChannel;
    [SerializeField] TMP_Text songTitle;
    [SerializeField] TMP_Text sectionTitle;
    [SerializeField] Transform finishPoint;
    [SerializeField] GameObject quarter;
    [SerializeField] MidiFilePlayer midiOutput;
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
        
        gameManager = GameManager.GetInstance();
        drumChannel = gameManager.GetSong().drumChannel;
        Debug.Log(drumChannel);
        songTitle.text = gameManager.GetSong().title;
        sectionTitle.text = gameManager.GetSection();
        midiOutput.OnEventNotesMidi.AddListener(NotesToPlay);
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
                        note = Instantiate(quarter, startingPoint[6]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[6].position.y, finishPoint.position.z));
                        break;
                    case 38://Snare
                        note = Instantiate(quarter, startingPoint[4]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[4].position.y, finishPoint.position.z));
                        break;
                    case 48://Tom1
                        note = Instantiate(quarter, startingPoint[2]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[2].position.y, finishPoint.position.z));
                        break;
                    case 45://Tom2
                        note = Instantiate(quarter, startingPoint[3]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[3].position.y, finishPoint.position.z));
                        break;
                    case 43://Tom3
                        note = Instantiate(quarter, startingPoint[5]).GetComponent<NoteBehavior>();
                        note.SetImage("quarter");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[5].position.y, finishPoint.position.z));
                        break;
                    case 57:
                    case 49://Crash
                        note = Instantiate(quarter, startingPoint[0]).GetComponent<NoteBehavior>();
                        note.SetImage("star");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[0].position.y, finishPoint.position.z));
                        break;
                    case 51://Ride
                        note = Instantiate(quarter, startingPoint[0]).GetComponent<NoteBehavior>();
                        note.SetImage("cross");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[0].position.y, finishPoint.position.z));
                        break;
                    case 44:
                    case 46://HiHat
                        note = Instantiate(quarter, startingPoint[1]).GetComponent<NoteBehavior>();
                        note.SetImage("cross");
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint[1].position.y, finishPoint.position.z));
                        break;
                    default:
                        Debug.LogError($"No note for this number: {mptkEvent.Value}");
                        break;
                }
                if(note != null)
                {
                    note.SetId(mptkEvent.Value);
                    note.SetDistance(finishPoint.position.x - startingPoint[0].position.x);
                }
                
            }                
        }
    }
}
