using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 300f;
    private float currentTime;
    private bool gameEnded = false;

    [Header("References")]
    public CurrencyManager currencyManager;

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text currencyScoreText;
    public TMP_Text auraScoreText;
    public TMP_Text totalScoreText;
    public GameObject endGamePanel; // Optional panel to show results

    void Start()
    {
        currentTime = startTime;

        if (endGamePanel != null)
            endGamePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        gameEnded = true;

        // Freeze the game
        Time.timeScale = 0f;

        // Get stats
        int currency = currencyManager.GetCurrentCurrency();
        int aura = currencyManager.GetCurrentAura();

        // Calculate scores (customize formula if needed)
        int currencyScore = currency;
        int auraScore = aura * 10; // Example multiplier
        int totalScore = currencyScore + auraScore;

        // Display scores separately
        currencyScoreText.text = "Currency Score: " + currencyScore;
        auraScoreText.text = "Aura Score: " + auraScore;
        totalScoreText.text = "Total Score: " + totalScore;

        if (endGamePanel != null)
            endGamePanel.SetActive(true);
    }
}