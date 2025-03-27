using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private float timePlayed;
    [SerializeField] private Text timeText;
    [SerializeField] private bool isRunning = true;
    [SerializeField] private Image weapon1Highlight;
    [SerializeField] private Image weapon2Highlight;

    private int selectedWeapon = 1; // Default to weapon 1

    void Start()
    {
        UpdateWeaponUI();
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

        if (Input.GetKeyDown(KeyCode.Escape))
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
            RunGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isRunning = false;
    }

    void RunGame()
    {
        Time.timeScale = 1;
        isRunning = true;
    }
}
