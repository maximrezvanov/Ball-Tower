using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject settingsMenu;

    private void Awake()
    {
        GameObject settingsMenu = GameObject.Find("SettingsMenuPanel");
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}






