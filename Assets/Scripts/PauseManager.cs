using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }
    private void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
