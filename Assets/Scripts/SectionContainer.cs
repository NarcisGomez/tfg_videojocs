using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionContainer : MonoBehaviour
{
    bool fadeActive;
    bool pumpActive;
    Image background;
    int tempo;
    bool color;

    private void Start()
    {
        fadeActive = false;
        pumpActive = false;
        background = GetComponent<Image>();
        tempo = GameManager.getInstance().GetSong().tempo;
    }

    void Update()
    {
        if (pumpActive)
        {
            ColorPump();
        }
        else if (fadeActive)
        {
            ColorFade();
        }
    }

    public void SetPump(bool pump)
    {
        pumpActive = pump;
    }

    public void SetFade(bool fade)
    {
        SetPump(false);
        fadeActive = fade;
    }

    private void ColorFade()
    {
        background.color = Color.Lerp(background.color, Color.black, Time.deltaTime * tempo * 0.1f);
    }
    private void ToggleColor()
    {
        color = !color;
    }

    private void ColorPump()
    {
        if (color)
        {
            background.color = Color.Lerp(background.color, Color.blue, Time.deltaTime * tempo * 0.1f);
        }
        else
        {
            background.color = Color.Lerp(background.color, Color.red, Time.deltaTime * tempo * 0.1f);
        }
        
    }

    public void Tick()
    {
        ToggleColor();
    }
}
