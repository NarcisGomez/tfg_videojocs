using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class BarDetector : MonoBehaviour
{
    GameManager gameManager;
    PracticeManager practiceManager;
    bool paused;
    List<NoteBehavior> notes = new List<NoteBehavior>();
    [SerializeField] List<Transform> startingPoint;
    [SerializeField] StatisticsManager statsManager;
    [SerializeField] MidiFilePlayer midiPlayer;
    [SerializeField] GameObject quarter;
    [SerializeField] Transform finishPoint;

    private void Start()
    {
        practiceManager = FindObjectOfType<PracticeManager>();
        gameManager = GameManager.GetInstance();
        midiPlayer.MPTK_MidiName = gameManager.GetSong();
    }


    public void HitNote(int note)
    {
            if (notes.Count != 0)
            {
                foreach (NoteBehavior n in notes)
            {
                if(n.GetId() == note) {
                    Debug.Log("HIT" + note);
                    statsManager.AddHitNote();
                    n.SetRight(true);
                    n.SetImage(n.GetNote() + "_right");
                }
                break;
            }
                
            }
    }

    public void TogglePause()
    {
        if (!paused)
        {
            midiPlayer.MPTK_Pause();
        }
        else
        {
            midiPlayer.MPTK_UnPause();
        }
        paused = !paused;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NoteBehavior>().GetId() == 0) {
            midiPlayer.MPTK_StartPlayAtFirstNote = true;
            midiPlayer.MPTK_Play();

        }
        notes.Add(collision.gameObject.GetComponent<NoteBehavior>());
        statsManager.AddPlayedNote();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NoteBehavior note = collision.gameObject.GetComponent<NoteBehavior>();

        if (notes.Contains(note))
        {
            if (!note.GetRight())
            {
                note.SetImage(note.GetNote() + "_wrong");
                statsManager.AddMissedNote();
            }
        }
    }
}
