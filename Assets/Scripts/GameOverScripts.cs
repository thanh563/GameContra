using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScripts : MonoBehaviour
{
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    private void Start()
    {
        
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PlayerDev");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuUI");
    }
}
