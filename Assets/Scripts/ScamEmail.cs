using System.Collections;
using UnityEngine;

public class ScamEmail : MonoBehaviour
{
    [Header("Game Objects to Show")]
    public GameObject winObject;     // Show this if player wins
    public GameObject loseObject;    // Show this if player loses

    [Header("Settings")]
    public float waitTime = 30f;     // Time to wait before deciding win/loss
    public float showTime = 5f;      // Time to show result object
    [Range(0, 1)]
    public float winChance = 0.3f;   // 30% chance to win
    public int rewardAmount = 500;   // Amount to add if player wins

    [Header("References")]
    public CurrencyManager currencyManager;

    private bool gameInProgress = false;

    void Start()
    {
        // Ensure result objects are hidden at start
        if (winObject) winObject.SetActive(false);
        if (loseObject) loseObject.SetActive(false);
    }

    public void StartChanceGame()
    {
        if (!gameInProgress)
        {
            StartCoroutine(ChanceRoutine());
        }
        else
        {
            Debug.Log("Game already in progress!");
        }
    }

    private IEnumerator ChanceRoutine()
    {
        gameInProgress = true;
        Debug.Log("Waiting for " + waitTime + " seconds...");

        // Wait for the defined time
        yield return new WaitForSeconds(waitTime);

        // Decide win or lose
        bool won = Random.value < winChance;

        if (won)
        {
            Debug.Log("Player WON!");
            currencyManager.Add(rewardAmount);

            if (winObject) winObject.SetActive(true);
        }
        else
        {
            Debug.Log("Player LOST!");
            if (loseObject) loseObject.SetActive(true);
        }

        // Wait for showTime before hiding objects
        yield return new WaitForSeconds(showTime);

        if (winObject) winObject.SetActive(false);
        if (loseObject) loseObject.SetActive(false);

        gameInProgress = false;
        Debug.Log("Game finished.");

    }

    public void Hide()
    {
        if (winObject) winObject.SetActive(false);
        if (loseObject) loseObject.SetActive(false);
    }
}