using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene("PlayerDev");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
