using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject Slots;
    public GameObject Stocks;
    public GameObject ScamEmail;
    public GameObject SpyNetwork;
    public GameObject Reboot;
    public GameObject ChickenFights;

    [Header("UI Player")]
    public GameObject PlayerUI;

    [Header("Reboot Animation")]
    public Animator RebootAnimator;     // Assign in Inspector
    public string rebootAnimationName = "RebootAnim"; // Animation state name
    public float rebootAnimationLength = 3f; // Fallback duration

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        Slots.SetActive(true);
        Stocks.SetActive(false);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(false);
        Reboot.SetActive(false);
        ChickenFights.SetActive(false);
    }

    public void ShowStocks()
    {
        Slots.SetActive(false);
        Stocks.SetActive(true);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(false);
        Reboot.SetActive(false);
        ChickenFights.SetActive(false);
    }

    public void ShowEmails()
    {
        Slots.SetActive(false);
        Stocks.SetActive(false);
        ScamEmail.SetActive(true);
        SpyNetwork.SetActive(false);
        Reboot.SetActive(false);
        ChickenFights.SetActive(false);
    }

    public void ShowSpyNetwork()
    {
        Slots.SetActive(false);
        Stocks.SetActive(false);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(true);
        Reboot.SetActive(false);
        ChickenFights.SetActive(false);
    }

    public void ShowChickenFights()
    {
        Slots.SetActive(false);
        Stocks.SetActive(false);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(false);
        Reboot.SetActive(false);
        ChickenFights.SetActive(true);
    }

    public void ShowReboot()
    {
        Slots.SetActive(false);
        Stocks.SetActive(false);
        ScamEmail.SetActive(false);
        SpyNetwork.SetActive(false);
        Reboot.SetActive(true);
        ChickenFights.SetActive(false);

        PlayerUI.SetActive(false);

        // Start reboot sequence
        StartCoroutine(PlayRebootSequence());
    }

    private IEnumerator PlayRebootSequence()
    {
        if (RebootAnimator != null)
        {
            RebootAnimator.Play(rebootAnimationName);

            // Wait until animation finishes
            yield return new WaitForSeconds(rebootAnimationLength);
        }
        else
        {
            Debug.LogWarning("Reboot Animator not assigned.");
            yield return new WaitForSeconds(2f);
        }

        // After animation completes
        PlayerUI.SetActive(true);
        ShowMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}