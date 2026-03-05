using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChickenFightManager : MonoBehaviour
{
    [Header("Currency")]
    public CurrencyManager currencyManager;
    public int betAmount = 100;

    [Header("UI")]
    public Button[] chickenButtons;   // Assign 4 buttons (Chicken 0–3)
    public Button startFightButton;
    public TMP_Text resultText;

    private int selectedChicken = -1;
    private bool fightInProgress = false;

    void Start()
    {
        // Assign button listeners
        for (int i = 0; i < chickenButtons.Length; i++)
        {
            int index = i;
            chickenButtons[i].onClick.AddListener(() => SelectChicken(index));
        }

        startFightButton.onClick.AddListener(StartFights);

        resultText.text = "Select a chicken to bet on!";
    }

    void SelectChicken(int index)
    {
        if (fightInProgress) return;

        if (currencyManager.Spend(betAmount))
        {
            selectedChicken = index;
            resultText.text = "Bet placed on Chicken " + (index + 1);
        }
        else
        {
            resultText.text = "Not enough money to bet!";
        }
    }

    void StartFights()
    {
        if (fightInProgress) return;
        if (selectedChicken == -1)
        {
            resultText.text = "Select a chicken first!";
            return;
        }

        fightInProgress = true;
        resultText.text = "Fights starting...";

        RunTournament();
    }

    void RunTournament()
    {
        // Semi-final 1: Chicken 0 vs 1
        int winner1 = Fight(0, 1);

        // Semi-final 2: Chicken 2 vs 3
        int winner2 = Fight(2, 3);

        // Final: winners fight
        int champion = Fight(winner1, winner2);

        EvaluateBet(winner1, winner2, champion);

        fightInProgress = false;
        selectedChicken = -1;
    }

    int Fight(int chickenA, int chickenB)
    {
        // 50/50 chance
        bool aWins = Random.value < 0.5f;

        return aWins ? chickenA : chickenB;
    }

    void EvaluateBet(int semiWinner1, int semiWinner2, int champion)
    {
        bool wonFirstFight = false;
        bool wonSecondFight = false;

        // Check if selected chicken won its semi-final
        if (selectedChicken == semiWinner1 || selectedChicken == semiWinner2)
        {
            wonFirstFight = true;
        }

        // Check if selected chicken won final
        if (selectedChicken == champion)
        {
            wonSecondFight = true;
        }

        if (wonSecondFight)
        {
            // Double money (profit = betAmount * 2 total return)
            currencyManager.Add(betAmount * 2);
            resultText.text = "Your chicken WON TWICE! You doubled your bet!";
        }
        else if (wonFirstFight)
        {
            // Money back
            currencyManager.Add(betAmount);
            resultText.text = "Your chicken won once! You got your money back!";
        }
        else
        {
            resultText.text = "Your chicken lost! You lost your bet!";
        }
    }
}