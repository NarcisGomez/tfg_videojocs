using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MidiPlayerTK;

public class BPM : MonoBehaviour
{
    static BPM instance;
    static bool beatFull;
    static int beatCountFull;
    int prevCount;
    int tickCount;
    float beatInterval;
    float beatTimer;
    AudioManager audioManager;
    GameManager gameManager;
    public float bpm;
    [SerializeField] bool muteClick;
    [SerializeField] TMP_Text tempoText;
    [SerializeField] MidiFilePlayer midiPlayer;
    [SerializeField] SongLoader songLoader;

    public static BPM getInstance()
    {
        if (instance != null) return instance;
        throw new System.Exception("There is no Game Manager");
    }

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioManager = AudioManager.getInstance();
        gameManager = GameManager.getInstance();
        bpm = gameManager.GetSong().tempo;
        muteClick = true;
        prevCount = 4;
        tickCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BeatDetection();
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
            beatCountFull++;
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
                midiPlayer.MPTK_ChannelEnableSet(9, false);
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

    public void toggleMute()
    {
        muteClick = !muteClick;
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
}
