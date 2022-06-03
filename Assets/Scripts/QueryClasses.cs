using System.Collections.Generic;
using System;

[Serializable]
public class GetSongData
{
    public GetSong data;
}
[Serializable]
public class GetSong
{
    public string getSong;
}

[Serializable]
public class GetSongInfoListData
{
    public GetSongInfoList data;
}

[Serializable]
public class GetSongInfoList
{
    public List<string> getSongInfoList;

    public override string ToString()
    {

        string message = "[";

        foreach (string s in getSongInfoList)
        {
            message += s + ",";
        }
        return message + "]";
    }
}

[Serializable]
public class GetSongInfoData
{
    public GetSongInfo data;
}

[Serializable]
public class GetSongInfo
{
    public SongInfo getSongInfo;
}

[Serializable]
public class SongInfo
{
    public string band;
    public string title;
    public string tempo;
    public List<Section> sections;
}
[Serializable]
public class Section
{
    public string name;
    public string loops;
}

[Serializable]
public class GetUserStatsData
{
    public GetUserStats data;
}
[Serializable]
public class GetUserStats
{
    public UserStats getUserStats;
}
[Serializable]
public class UserStats
{
    public string id;
    public List<string> songs;
    public List<int> best;
    public List<int> tried;
    public List<bool> completed;
}