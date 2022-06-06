using UnityEngine;
using TMPro;
using MidiPlayerTK;

public class BPM : MonoBehaviour
{
    bool beatFull;
    int prevCount;
    int tickCount;
    float beatInterval;
    float beatTimer;
    AudioManager audioManager;
    GameManager gameManager;
    public bool paused;
    public float bpm;
    [SerializeField] bool muteClick;
    [SerializeField] TMP_Text tempoText;
    [SerializeField] MidiFilePlayer midiPlayer;
    [SerializeField] SongLoader songLoader;

    void Start()
    {
        audioManager = AudioManager.getInstance();
        gameManager = GameManager.GetInstance();
        bpm = int.Parse(gameManager.GetSongInfo().tempo);
        midiPlayer.MPTK_MidiName = gameManager.GetSong();
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
            else
            {
                songLoader.Tick();
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
        tempoText.text = tickCount.ToString();

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
        }
        else
        {
            midiPlayer.MPTK_Pause();
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
}
