using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BPM : MonoBehaviour
{
    static BPM instance;
    static bool beatFull;
    static int beatCountFull;
    int prevCount;
    int tickCount;
    float beatInterval;
    float beatTimer;
    [SerializeField] AudioManager audioManager;
    [SerializeField] float bpm;
    [SerializeField] bool muteClick;
    [SerializeField] TMP_Text tempoText;

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
        if(beatFull)
        {
            updateTick();
            if (prevCount > 0)
            {
                audioManager.Play("tick");
                prevCount--;
            }
            else if (!muteClick)
            {
                audioManager.Play("tick");
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
        if (tickCount > 4) tickCount = 1;
        tempoText.text = tickCount.ToString();

    }

}
