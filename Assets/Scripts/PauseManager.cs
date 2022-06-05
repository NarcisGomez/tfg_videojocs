using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] BPM bpm;
    [SerializeField] PracticeManager practiceManager;
    [SerializeField] MenuManager menuManager;
    [SerializeField] BarDetector barDetector;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if(bpm != null) bpm.TogglePause();
        if(practiceManager != null) practiceManager.TogglePause();
        if (barDetector != null) barDetector.TogglePause();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void FinishPlay()
    {
        menuManager.ChangeScene("EndPlay");
    }
}
