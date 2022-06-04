using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private string currentSong;
    private SongInfo currentSongInfo;
    private float tempoMultiplier;
    string currentSection;

    public static GameManager GetInstance()
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
    public void SetSong(string song)
    {
        currentSong = song;
    }

    public void SetSongInfo(SongInfo songInfo)
    {
        currentSongInfo = songInfo;
    }

    public string GetSong()
    {
        if (currentSong != null) return currentSong;
        throw new System.Exception("No song name");
    }

    public SongInfo GetSongInfo()
    {
        if(currentSongInfo != null ) return currentSongInfo;
        throw new System.Exception("No song to play");
    }

    public void SetSection(string section)
    {
        currentSection = section;
    }

    public string GetSection()
    {
        if (currentSong != null) return currentSection;
        throw new System.Exception("No section selected");
    }

    public void setTempoMultiplier(float value)
    {
        tempoMultiplier = value;
    }

    public void EndSong()
    {
        SceneManager.LoadScene("SongListScene");
    }
}

