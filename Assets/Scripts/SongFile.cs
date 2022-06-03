using System;

[Serializable]
public class SongFile
{
    public string band;
    public string title;
    public int tempo;
    public int drumChannel;
    public string[] sections;
    public int[] loops;

    public SongFile(string band, string title, int tempo, int drumChannel, string[] sections, int[] loops)
    {
        this.band = band;
        this.title = title;
        this.tempo = tempo;
        this.drumChannel = drumChannel;
        this.sections = sections;
        this.loops = loops;
    }

    public override string ToString()
    {
        return title + tempo;
    }

    public string GetSongName()
    {
        return band + " - " + title;
    } 
}