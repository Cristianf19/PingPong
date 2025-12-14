using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public WindowHandler windowHandler;
    public GameObject mainMenu;
    public GameObject menuMultiplayer;
    public void Start()
    {
        windowHandler.enabledWindow(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void MultiplayerGame()
    {
        windowHandler.enabledWindow(1);
    }
    public void QuitGame()
    {
       Application.Quit();
    }
}
