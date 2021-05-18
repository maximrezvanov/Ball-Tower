using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button onMusic;
    public Button offMusic;
    public Button onFx;
    public Button offFx;
    public Animator clickStart;

    public void MuteMusic()
    {
        SoundController.Instance.OffMusic();
    }

    public void OnMusic()
    {
        SoundController.Instance.OnMusic();
    }

    public void MuteFx()
    {
        SoundController.Instance.OffFx();
    }

    public void OnFx()
    {
        SoundController.Instance.OnFx();
    }

    public void StartGame()
    {
        StartCoroutine(StartFirstLevel());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        StartCoroutine(CheckSoundButtonStatus());
    }

    private IEnumerator CheckSoundButtonStatus()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (SoundController.Instance.isMusicOff)
            {
               offMusic.gameObject.SetActive(false);
            }
            if (SoundController.Instance.isFxOff)
            {
                offFx.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator StartFirstLevel()
    {
        clickStart.SetBool("onClickPlayButton", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);

    }
}






