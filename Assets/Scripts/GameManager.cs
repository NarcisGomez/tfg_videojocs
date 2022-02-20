using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private SongFile currentSong;
    [SerializeField] private float tempoMultiplier;

    public static GameManager getInstance()
    {
        if (instance != null) return instance;
        throw new System.Exception("There is no Game Manager");
    }

    private void Awake()
    {
        tempoMultiplier = 1;
        if (instance != null) Destroy(gameObject);
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void SetSong(SongFile song)
    {
        currentSong = song;
    }

    public void setTempoMultiplier(float value)
    {
        tempoMultiplier = value;
    }

    

    


}

