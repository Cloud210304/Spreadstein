using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SlotSymbol
{
    public string name;
    public Sprite sprite;
    public float multiplier;
    [Range(0f, 1f)]
    public float probability;
}

public class SlotMachine : MonoBehaviour
{
    [Header("Currency")]
    public CurrencyManager currencyManager;

    [Header("Bet Settings")]
    public int currentBet = 50;
    public int betStep = 5;

    [Header("Symbols")]
    public SlotSymbol[] symbols;

    [Header("Slot UI (5 Slots)")]
    public Image[] slots = new Image[5];

    [Header("Spin Settings")]
    public float spinTimePerReel = 1.0f;
    public float reelStopDelay = 0.3f; // delay between each reel stopping
    public float spinSpeed = 0.05f;

    [Header("UI")]
    public Button spinButton;
    public Button increaseBetButton;
    public Button decreaseBetButton;
    public Text resultText;
    public Text betText;
    public Image rewardDisplay;

    private bool isSpinning = false;
    private int totalSpins = 0;

    void Start()
    {
        spinButton.onClick.AddListener(Spin);
        increaseBetButton.onClick.AddListener(IncreaseBet);
        decreaseBetButton.onClick.AddListener(DecreaseBet);

        UpdateBetUI();
        resultText.text = "";
    }

    void IncreaseBet()
    {
        currentBet += betStep;
        UpdateBetUI();
    }

    void DecreaseBet()
    {
        currentBet = Mathf.Max(betStep, currentBet - betStep);
        UpdateBetUI();
    }

    void UpdateBetUI()
    {
        betText.text = "Bet: " + currentBet;
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            if (currencyManager.Spend(currentBet))
            {
                StartCoroutine(SpinCoroutine());
            }
        }
    }

    IEnumerator SpinCoroutine()
    {
        isSpinning = true;
        spinButton.interactable = false;
        resultText.text = "Spinning...";

        int reelCount = slots.Length;

        // ?? STEP 1: Pre-determine final results (important!)
        SlotSymbol[] finalSymbols = new SlotSymbol[reelCount];
        for (int i = 0; i < reelCount; i++)
        {
            finalSymbols[i] = GetRandomSymbol();
        }

        // ?? STEP 2: Spin reels independently
        List<Coroutine> spinningReels = new List<Coroutine>();

        for (int i = 0; i < reelCount; i++)
        {
            int index = i;
            spinningReels.Add(StartCoroutine(SpinReel(index)));
        }

        // ? STEP 3: Stop reels one by one
        for (int i = 0; i < reelCount; i++)
        {
            yield return new WaitForSeconds(reelStopDelay);

            // stop reel i by forcing final sprite
            StopCoroutine(spinningReels[i]);
            slots[i].sprite = finalSymbols[i].sprite;
        }

        // small pause after last reel
        yield return new WaitForSeconds(0.3f);

        // ?? Check win
        Sprite[] results = new Sprite[reelCount];
        for (int i = 0; i < reelCount; i++)
        {
            results[i] = finalSymbols[i].sprite;
        }

        CheckWin(results);

        totalSpins++;

        if (totalSpins % 10 == 0)
        {
            currencyManager.AddAura(1);
            Debug.Log("Bonus Aura +1 for 10 spins!");
        }

        spinButton.interactable = true;
        isSpinning = false;
    }

    IEnumerator SpinReel(int index)
    {
        float timer = 0f;

        while (true)
        {
            slots[index].sprite = GetRandomSymbol().sprite;
            yield return new WaitForSeconds(spinSpeed);
            timer += spinSpeed;
        }
    }

    SlotSymbol GetRandomSymbol()
    {
        float totalWeight = 0f;
        foreach (var s in symbols)
            totalWeight += s.probability;

        float rand = Random.Range(0, totalWeight);
        float current = 0f;

        foreach (var s in symbols)
        {
            current += s.probability;
            if (rand <= current)
                return s;
        }

        return symbols[0];
    }

    void CheckWin(Sprite[] results)
    {
        Dictionary<Sprite, int> counts = new Dictionary<Sprite, int>();

        foreach (Sprite s in results)
        {
            if (!counts.ContainsKey(s))
                counts[s] = 0;

            counts[s]++;
        }

        foreach (var pair in counts)
        {
            if (pair.Value >= 3)
            {
                SlotSymbol symbol = GetSymbolFromSprite(pair.Key);
                int payout = Mathf.RoundToInt(currentBet * symbol.multiplier);

                resultText.text = "WIN! x" + symbol.multiplier + " (" + payout + ")";
                currencyManager.Add(payout);

                rewardDisplay.sprite = symbol.sprite;
                return;
            }
        }

        resultText.text = "Try Again!";
        rewardDisplay.sprite = null;
    }

    SlotSymbol GetSymbolFromSprite(Sprite sprite)
    {
        foreach (var s in symbols)
        {
            if (s.sprite == sprite)
                return s;
        }

        return symbols[0];
    }
}