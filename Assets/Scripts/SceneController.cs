
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }

    public void LoadLevel()
    {
        StartCoroutine(NextLevel());

    }
    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3f);

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);

    }

}
