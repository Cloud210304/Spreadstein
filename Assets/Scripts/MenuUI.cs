using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject MainMenu;
    public GameObject Stocks;
    public GameObject ScamEmail;
    public GameObject SpyNetwork;

    void Start()
    {
        // Set default UI state
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {

        MainMenu.SetActive(true);

    }

    public void play()
    {
        SceneManager.LoadScene("GAME");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
