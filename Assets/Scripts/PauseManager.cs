using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] BPM bpm;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
            bpm.TogglePause();
        }
    }
    private void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
