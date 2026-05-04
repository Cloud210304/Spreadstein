using UnityEngine;

public class MarketCrashQTE : MonoBehaviour
{
    [Header("References")]
    public SimpleStockMarket stockMarket;

    private bool triggered = false;

    private void OnEnable()
    {
        triggered = false;
    }

    // Call this from a button like "OH NO!"
    public void TriggerCrash()
    {
        if (triggered) return;

        triggered = true;

        if (stockMarket != null)
        {
            stockMarket.TriggerMarketCrash();
        }

        CloseQTE();
    }

    private void CloseQTE()
    {
        gameObject.SetActive(false);
    }
}