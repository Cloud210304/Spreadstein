using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [Header("Currency")]
    public CurrencyManager currencyManager;

    [Header("Win Settings")]
    public int winReward = 200;
    public int superWinReward = 1000;
    public int spinCost = 50;

    [Header("Super Win Sprite")]
    public Sprite superWinSprite; 

    [Header("Slot UI Images (Assign 3 UI Images)")]
    public Image slot1;
    public Image slot2;
    public Image slot3;

    [Header("Possible Sprites (Assign in Inspector)")]
    public Sprite[] possibleSprites;

    [Header("Spin Settings")]
    public float spinDuration = 1.5f;
    public float spinSpeed = 0.1f;

    [Header("UI")]
    public Button spinButton;
    public Text resultText;

    private bool isSpinning = false;

    void Start()
    {
        spinButton.onClick.AddListener(Spin);
        resultText.text = "";
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            if (currencyManager.Spend(spinCost))
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

        float timer = 0f;

        while (timer < spinDuration)
        {
            slot1.sprite = GetRandomSprite();
            slot2.sprite = GetRandomSprite();
            slot3.sprite = GetRandomSprite();

            timer += spinSpeed;
            yield return new WaitForSeconds(spinSpeed);
        }

        // Final result
        Sprite final1 = GetRandomSprite();
        Sprite final2 = GetRandomSprite();
        Sprite final3 = GetRandomSprite();

        slot1.sprite = final1;
        slot2.sprite = final2;
        slot3.sprite = final3;

        CheckWin(final1, final2, final3);

        spinButton.interactable = true;
        isSpinning = false;
    }

    Sprite GetRandomSprite()
    {
        return possibleSprites[Random.Range(0, possibleSprites.Length)];
    }

    void CheckWin(Sprite s1, Sprite s2, Sprite s3)
    {
        if (s1 == s2 && s2 == s3)
        {
            // SUPER WIN condition
            if (s1 == superWinSprite)
            {
                resultText.text = "SUPER WIN!!!";
                currencyManager.Add(superWinReward);
                Debug.Log("SUPER WIN!");
            }
            else
            {
                resultText.text = "YOU WIN!";
                currencyManager.Add(winReward);
                Debug.Log("Normal Win!");
            }
        }
        else
        {
            resultText.text = "Try Again!";
        }
    }
}