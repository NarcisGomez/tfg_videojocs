using UnityEngine;
using UnityEngine.UI;
using System;

public class SectionContainer : MonoBehaviour
{
    bool current;
    bool fadeActive;
    Image background;
    int tempo;

    private void Start()
    {
        current = false;
        fadeActive = false;
        background = GetComponent<Image>();
        tempo = Int32.Parse(GameManager.GetInstance().GetSongInfo().tempo);
    }

    void Update()
    {
        if (current)
        {
            FadePlaying();
        }

        if (fadeActive)
        {
            ColorFade();
        }
    }

    public void SetPlaying(bool playing)
    {
        current = playing;
    }

    public void SetFade(bool fade)
    {
        current = false;
        fadeActive = fade;
    }

    private void ColorFade()
    {
        background.color = Color.Lerp(background.color, Color.black, Time.deltaTime * tempo * 0.1f);
    }

    private void FadePlaying()
    {
        background.color = Color.Lerp(background.color, Color.red, Time.deltaTime * tempo * 0.1f);
    }
}
