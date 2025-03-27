using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private float timePlayed;
    [SerializeField] private Text timeText;
    [SerializeField] private bool isRunning = true;
    [SerializeField] private Image weapon1Highlight;
    [SerializeField] private Image weapon2Highlight;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound; // Âm thanh khi game over
    private AudioSource audioSource; // AudioSource để phát âm thanh

    private int selectedWeapon = 1; // Default to weapon 1

    void Start()
    {
        UpdateWeaponUI();
        pauseMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void SwitchWeapon(int weaponNumber)
    {
        selectedWeapon = weaponNumber;
        UpdateWeaponUI();
    }

    void UpdateWeaponUI()
    {
        weapon1Highlight.gameObject.SetActive(selectedWeapon == 1);
        weapon2Highlight.gameObject.SetActive(selectedWeapon == 2);
    }

    void Update()
    {
        if (isRunning)
        {
            timePlayed += Time.deltaTime;

            int hours = Mathf.FloorToInt(timePlayed / 3600);
            int minutes = Mathf.FloorToInt((timePlayed % 3600) / 60);
            int seconds = Mathf.FloorToInt(timePlayed % 60);

            timeText.text = string.Format("Time: {0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    PauseButtonHandler();
        //}
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PauseButtonHandler();
        }
    }

    public void PauseButtonHandler()
    {
        if (isRunning)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isRunning = false;
        pauseMenu.SetActive(true); 
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        isRunning = true;
        pauseMenu.SetActive(false); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void BackToMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1); 
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        isRunning = false;
        gameOverScreen.SetActive(true);

       
        foreach (var audio in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            audio.Stop();
        }

        
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }
}
