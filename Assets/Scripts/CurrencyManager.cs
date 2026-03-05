using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [Header("Starting Settings")]
    public int startingCurrency = 1000;

    [Header("TMP UI References")]
    public TMP_Text startingCurrencyText;
    public TMP_Text currentCurrencyText;
    public TMP_Text gainedText;
    public TMP_Text AuraText;

    private int currentCurrency;
    private int totalGained;
    private int currentAura;

    public int GetCurrentAura() => currentAura;

    void Start()
    {
        currentCurrency = startingCurrency;
        totalGained = 0;
        currentAura = 0;

        UpdateUI();
    }

    public bool Spend(int amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateUI();
            return true;
        }

        Debug.Log("Not enough currency!");
        return false;
    }

    public void Add(int amount)
    {
        currentCurrency += amount;
        totalGained += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        startingCurrencyText.text = "Starting: $" + startingCurrency;
        currentCurrencyText.text = "Current: $" + currentCurrency;
        gainedText.text = "Gained: $" + totalGained;
        AuraText.text = "Aura: " + currentAura;
    }

    // Optional getters if needed elsewhere
    public int GetCurrentCurrency() => currentCurrency;


    public void LoseCurrency(int amount)
    {
        currentCurrency -= amount;
        if (currentCurrency < 0)
            currentCurrency = 0;

        UpdateUI();
    }

    public void AddAura(int amount)
    {
        currentAura += amount;
        UpdateUI();
    }

    public void LoseAura(int amount)
    {
        currentAura -= amount;
        if (currentAura < 0)
            currentAura = 0;

        UpdateUI();
    }
}