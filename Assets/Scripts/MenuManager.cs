using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager: MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
