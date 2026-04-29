using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public GameObject pausePanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        SoundManager.instance.musicSource.Pause();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        SoundManager.instance.musicSource.UnPause();
        Time.timeScale = 1f;
    }

    public void PauseTime()
    {
        SoundManager.instance.musicSource.Pause();
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        SoundManager.instance.musicSource.UnPause();
        Time.timeScale = 1f;
    }
}