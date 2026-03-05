using UnityEngine;

public class AuraOrCreditsQTE : MonoBehaviour
{
    [Header("Penalty Settings")]
    public int auraLossAmount = 10;
    public int creditLossAmount = 500;

    [Header("References")]
    public CurrencyManager currencyManager;

    private bool choiceMade = false;

    private void OnEnable()
    {
        choiceMade = false;
    }

    // Called by "Lose Aura" button
    public void LoseAuraOption()
    {
        if (choiceMade) return;

        choiceMade = true;
        currencyManager.LoseAura(auraLossAmount);
        CloseQTE();
    }

    // Called by "Lose Credits" button
    public void LoseCreditsOption()
    {
        if (choiceMade) return;

        choiceMade = true;
        currencyManager.LoseCurrency(creditLossAmount);
        CloseQTE();
    }

    private void CloseQTE()
    {
        gameObject.SetActive(false);
    }
}