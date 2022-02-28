using System;

[Serializable]
public class SongFile
{
    public string title;
    public int tempo;
    public string[] sections;
    public int[] loops;

    public SongFile(string title, int tempo, string[] sections, int[] loops)
    {
        this.title = title;
        this.tempo = tempo;
        this.sections = sections;
        this.loops = loops;
    }
}