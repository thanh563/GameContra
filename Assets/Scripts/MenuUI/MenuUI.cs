using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
