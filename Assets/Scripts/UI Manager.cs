using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject Slots;
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

        Slots.SetActive(true);
        Stocks.SetActive(false);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(false);
    }

    public void ShowStocks()
    {
        Slots.SetActive(false);
        Stocks.SetActive(true);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(false);

    }

    public void ShowEmails()
    {
        Slots.SetActive(false);
        Stocks.SetActive(false);
        ScamEmail.SetActive(true);
        SpyNetwork.SetActive(false);

    }

    public void ShowSpyNetwork()
    {

        Slots.SetActive(false);
        Stocks.SetActive(false);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}