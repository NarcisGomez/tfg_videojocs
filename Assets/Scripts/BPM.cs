using UnityEngine;
using System.Collections.Generic;
using MidiPlayerTK;

public class BPM : MonoBehaviour
{
    int drumChannel;
    bool beatFull;
    int prevCount;
    int tickCount;
    float beatInterval;
    float beatTimer;
    AudioManager audioManager;
    GameManager gameManager;
    public bool paused;
    public float bpm;
    private List<NoteBehavior> notesOnDisplay;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform finishPoint;
    [SerializeField] GameObject quarter;
    [SerializeField] bool muteClick;
    [SerializeField] MidiFilePlayer midiPlayer;
    [SerializeField] SongLoader songLoader;
    [SerializeField] Animator circleAnimation;

    void Start()
    {
        notesOnDisplay = new List<NoteBehavior>();
        audioManager = AudioManager.getInstance();
        gameManager = GameManager.GetInstance();
        bpm = int.Parse(gameManager.GetSongInfo().tempo);
        circleAnimation.speed = circleAnimation.speed / 60 * bpm;
        drumChannel = int.Parse(gameManager.GetSongInfo().drumChannel);
        midiPlayer.MPTK_MidiName = gameManager.GetSong();
        midiPlayer.OnEventNotesMidi.AddListener(NotesToPlay);
        muteClick = true;
        prevCount = 4;
        tickCount = 0;
        paused = false;
    }

    void Update()
    {
        if (!paused)
        {
            BeatDetection();
        }
    }

    void BeatDetection()
    {
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
        }
        if (beatFull)
        {
            updateTick();
            if (prevCount > 0)
            { 
                prevCount--;
                audioManager.Play("tick");

            }
            else if(prevCount == 0)
            {
                midiPlayer.MPTK_Play();
                midiPlayer.MPTK_TickCurrent = midiPlayer.MPTK_TickFirstNote;
                prevCount--;
            }
            else if (!muteClick)
            {
                audioManager.Play("tick");
            }
        }
    }

    void updateTick()
    {
        tickCount++;
        if (tickCount > 4)
        {
            songLoader.WholeBar();
            tickCount = 1;
        }
    }

    public void MuteDrums()
    {
        midiPlayer.MPTK_ChannelEnableSet(9, !midiPlayer.MPTK_ChannelEnableGet(9));
    }

    public void TogglePause()
    {
        if (paused)
        {
            midiPlayer.MPTK_UnPause();
            notesOnDisplay.ForEach(note => note.SetStop(false));
        }
        else
        {
            midiPlayer.MPTK_Pause();
            notesOnDisplay.ForEach(note => note.SetStop(true));
        }
        paused = !paused;
    }

    public void toggleMute()
    {
        muteClick = !muteClick;
    }

    public void SetPause(bool value)
    {
        paused = value;
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
                    case 40:
                    case 39:
                    case 38://Snare
                    case 48://Tom1
                    case 45://Tom2
                    case 41:
                    case 43://Tom3
                    case 57:
                    case 49://Crash
                    case 51://Ride
                    case 42:
                    case 44:
                    case 46://HiHat
                        note = Instantiate(quarter, startingPoint).GetComponent<NoteBehavior>();
                        note.SetPosition(new Vector3(finishPoint.position.x, startingPoint.position.y, finishPoint.position.z));
                        break;
                    default:
                        Debug.LogError($"No note for this number: {mptkEvent.Value}");
                        break;
                }
                if (note != null)
                {
                    note.SetId(mptkEvent.Value);
                    notesOnDisplay.Add(note);
                }
            }
        }
    }

    public void RemoveNote(NoteBehavior note)
    {
        notesOnDisplay.Remove(note);
    }
}
