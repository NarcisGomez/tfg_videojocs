using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private string currentUser;
    private string currentSong;
    private SongInfo currentSongInfo;
    private float tempoMultiplier;
    private string instrument;
    string currentSection;
    SongStats currentStats;

    public static GameManager GetInstance()
    {
        if (instance != null) return instance;
        throw new System.Exception("There is no Game Manager");
    }

    private void Awake()
    {
        tempoMultiplier = 1;
        instrument = "Drums";
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log($"MidiDB exists: {Directory.Exists($"{Application.persistentDataPath}\\MidiDB")} {string.Join(" ", Directory.EnumerateDirectories(Application.persistentDataPath))}");
        if (Directory.Exists($"{Application.persistentDataPath}\\MidiDB") == false)
        {   
            Debug.Log($"Creating folder MidiDB into {Application.persistentDataPath}");
            Directory.CreateDirectory($"{Application.persistentDataPath}\\MidiDB");
            Debug.Log("Copy all the json to MidiDB");
            TextAsset[] midiFiles = Resources.LoadAll<TextAsset>("MidiDB");
            foreach(TextAsset ta in midiFiles)
            {
                File.WriteAllBytes($"{Application.persistentDataPath}/MidiDB/{ta.name}.bytes", ta.bytes);
            }
        }

        Debug.Log($"JSONFiles exists: {Directory.Exists($"{Application.persistentDataPath}\\JSONFiles")} {string.Join(" ", Directory.EnumerateDirectories(Application.persistentDataPath))}");
        if (Directory.Exists($"{Application.persistentDataPath}\\JSONFiles") == false)
        {
            Debug.Log($"Creating folder JSONFiles into {Application.persistentDataPath}");
            Directory.CreateDirectory($"{Application.persistentDataPath}\\JSONFiles");
            Debug.Log("Copy all the json to JSONFiles");
            TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("JSONFiles");
            foreach (TextAsset ta in jsonFiles)
            {
                File.WriteAllBytes($"{Application.persistentDataPath}/JSONFiles/{ta.name}.json", ta.bytes);
            }
        }

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

    public string GetInstrument()
    {
        return instrument;
    }

    public void SetInstrument(string value)
    {
        instrument = value;
    }

    public void setTempoMultiplier(float value)
    {
        tempoMultiplier = value;
    }

    public void EndSong()
    {
        SceneManager.LoadScene("EndPlay");
    }

    public SongStats GetStats()
    {
        return currentStats;
    }

    public void SetStats(int totalNotesPlayed, int hitNotes, int missedNotes)
    {
        SongStats songStats = new SongStats();
        songStats.totalNotesPlayed = totalNotesPlayed;
        songStats.hitNotes = hitNotes;
        songStats.missedNotes = missedNotes;
        currentStats = songStats;

    }

    public string GetUser()
    {
        return currentUser;
    }

    public void SetUSer(string name)
    {
        currentUser = name;
    }


}

